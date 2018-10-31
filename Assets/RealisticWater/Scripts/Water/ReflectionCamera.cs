using System.Collections;
using UnityEngine;

public class ReflectionCamera : MonoBehaviour
{
    public float m_ClipPlaneOffset = 0.07f;
    public LayerMask CullingMask = ~(1 << 4);
    public bool HDR;
    public bool OcclusionCulling = true;
    public float TextureScale = 1f;
    public RenderTextureFormat RenderTextureFormat;
    public FilterMode FilterMode = FilterMode.Point;
    public RenderingPath RenderingPath;
    public bool UseRealtimeUpdate;
    public int FPSWhenMoveCamera = 40;
    public int FPSWhenStaticCamera = 20;
    

    private RenderTexture renderTexture;
    private GameObject go;
    private Camera reflectionCamera;
    private Vector3 oldPosition;
    private Quaternion oldRotation;
    private Transform instanceCameraTransform;
    private int frameCountWhenCameraIsStatic;
    private bool canUpdateCamera, isStaticUpdate;
    private WaitForSeconds fpsMove, fpsStatic;
    private const int DropedFrames = 50;
    private Camera currentCamera;

    private void OnEnable()
    {
        Shader.EnableKeyword("editor_off");
        Shader.EnableKeyword("cubeMap_off");
        currentCamera = Camera.main;
        fpsMove = new WaitForSeconds(1.0f / FPSWhenMoveCamera);
        fpsStatic = new WaitForSeconds(1.0f / FPSWhenStaticCamera);
        if (!UseRealtimeUpdate) {
            StartCoroutine(RepeatCameraMove());
            StartCoroutine(RepeatCameraStatic());
        }
        else canUpdateCamera = true;
    }

    private IEnumerator RepeatCameraMove()
    {
        while (true) {
            if (!isStaticUpdate)
                canUpdateCamera = true;
            yield return fpsMove;
        }
    }

    private IEnumerator RepeatCameraStatic()
    {
        while (true) {
            if (isStaticUpdate)
                canUpdateCamera = true;
            yield return fpsStatic;
        }
    }

    private void OnBecameVisible()
    {
        if (go!=null)
            go.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        //if (go != null) go.SetActive(false);
    }

    // Use this for initialization
    private void Update()
    {
        Vector3 pos = transform.position;
        Vector3 normal = transform.up;
        float d = -Vector3.Dot(normal, pos) - m_ClipPlaneOffset;
        Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

        Matrix4x4 reflection = Matrix4x4.zero;
        CalculateReflectionMatrix(ref reflection, reflectionPlane);
        Vector3 oldpos = currentCamera.transform.position;
        Vector3 newpos = reflection.MultiplyPoint(oldpos);

        if (go==null) {
            renderTexture = new RenderTexture((int) (Screen.width * TextureScale), (int) (Screen.height * TextureScale), 16, RenderTextureFormat);
            renderTexture.DiscardContents();
            go = new GameObject("Water Refl Camera");
            reflectionCamera = go.AddComponent<Camera>();
            reflectionCamera.depth = currentCamera.depth - 1;
            reflectionCamera.renderingPath = RenderingPath;
            reflectionCamera.depthTextureMode = DepthTextureMode.None;
            go.transform.position = transform.position;
            go.transform.rotation = transform.rotation;
            reflectionCamera.cullingMask = CullingMask; 
            reflectionCamera.targetTexture = renderTexture;
            reflectionCamera.allowHDR = HDR;
            reflectionCamera.useOcclusionCulling = OcclusionCulling;
            Shader.SetGlobalTexture("_ReflectionTex", renderTexture);
            instanceCameraTransform = reflectionCamera.transform;
        }
        reflectionCamera.worldToCameraMatrix = currentCamera.worldToCameraMatrix * reflection;
        Vector4 clipPlane = CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);
#if UNITY_4_3 || UNITY_4_5 
		reflectionCamera.projectionMatrix = CalculateObliqueMatrix(reflectionCamera.projectionMatrix, clipPlane);
#else
        reflectionCamera.projectionMatrix = currentCamera.CalculateObliqueMatrix(clipPlane);
#endif

#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6
        GL.SetRevertBackfacing(true);
#else
        GL.invertCulling = true;
#endif
        go.transform.position = newpos;
        Vector3 euler = currentCamera.transform.eulerAngles;
        go.transform.eulerAngles = new Vector3(-euler.x, euler.y, euler.z);

        UpdateCameraPosition();

#if UNITY_4_3 || UNITY_4_5 || UNITY_4_6
        GL.SetRevertBackfacing(false);
#else
        GL.invertCulling = false;
#endif
    }

    private void UpdateCameraPosition()
    {
        if (reflectionCamera==null)
            return;
        if (Vector3.SqrMagnitude(instanceCameraTransform.position - oldPosition) <= 0.00001f && instanceCameraTransform.rotation==oldRotation) {
            ++frameCountWhenCameraIsStatic;
            if (frameCountWhenCameraIsStatic >= DropedFrames)
                isStaticUpdate = true;
        }
        else {
            frameCountWhenCameraIsStatic = 0;
            isStaticUpdate = false;
        }
        oldPosition = instanceCameraTransform.position;
        oldRotation = instanceCameraTransform.rotation;
        if (canUpdateCamera) {
            reflectionCamera.enabled = true;
            if(!UseRealtimeUpdate) canUpdateCamera = false;
        }
        else if (reflectionCamera.enabled)
            reflectionCamera.enabled = false;
    }

	private static float sgn(float a)
	{
		if (a > 0.0f) return 1.0f;
		if (a < 0.0f) return -1.0f;
		return 0.0f;
	}

	private Matrix4x4 CalculateObliqueMatrix (Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 q = projection.inverse * new Vector4(
			sgn(clipPlane.x),
			sgn(clipPlane.y),
			1.0f,
			1.0f
			);
		Vector4 c = clipPlane * (2.0F / (Vector4.Dot (clipPlane, q)));
		// third row = clip plane - fourth row
		projection[2] = c.x - projection[3];
		projection[6] = c.y - projection[7];
		projection[10] = c.z - projection[11];
		projection[14] = c.w - projection[15];
		return projection;
	}

    private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
    {
        reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMat.m01 = (-2F * plane[0] * plane[1]);
        reflectionMat.m02 = (-2F * plane[0] * plane[2]);
        reflectionMat.m03 = (-2F * plane[3] * plane[0]);

        reflectionMat.m10 = (-2F * plane[1] * plane[0]);
        reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMat.m12 = (-2F * plane[1] * plane[2]);
        reflectionMat.m13 = (-2F * plane[3] * plane[1]);

        reflectionMat.m20 = (-2F * plane[2] * plane[0]);
        reflectionMat.m21 = (-2F * plane[2] * plane[1]);
        reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMat.m23 = (-2F * plane[3] * plane[2]);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;
    }

    private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        Vector3 offsetPos = pos + normal * m_ClipPlaneOffset;
        Matrix4x4 m = cam.worldToCameraMatrix;
        Vector3 cpos = m.MultiplyPoint(offsetPos);
        Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal));
    }

    private void OnDisable()
    {
        ClearCamera();
        Shader.DisableKeyword("editor_off");
        Shader.DisableKeyword("cubeMap_off");
    }

    private void ClearCamera()
    {
        if (go) {
            DestroyImmediate(go);
            go = null;
        }
        if (renderTexture) {
            DestroyImmediate(renderTexture);
            renderTexture = null;
        }
    }

    // Update is called once per frame
}