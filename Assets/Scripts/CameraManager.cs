using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager instance;

    public GameObject character;
    public float minDistance;
    public float maxDistance;
    public float currentDistance;

    [SerializeField]
    private CameraEffect_Cinema cinemaEffect;

    private Vector3 dragStartRotation;

    public static CameraManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    // Use this for initialization
    private void Start()
    {
        InputManager inst_Input = InputManager.getInstance();
        inst_Input.mouseWheel += OnScroll;
        inst_Input.mouseRightDragging += OnDragging;
        inst_Input.mouseRightDragStart += OnDragStart;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void StartCinema(float waitTime = float.PositiveInfinity)
    {
        cinemaEffect.StartAnimation(waitTime);
    }

    public void CancelCinema()
    {
        cinemaEffect.CancelAnimation();
    }

    public void StartShake(float duration, float magnitude = 0.08f)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private void OnScroll(float delta)
    {
        Vector3 dis = character.transform.position - transform.position;
        float currentSqrMag = dis.sqrMagnitude;
        Debug.Log(currentSqrMag); // 스크롤이 안먹는다.18.11.05
        if (currentSqrMag < minDistance * minDistance && delta > 0 || currentSqrMag > maxDistance * maxDistance && delta < 0)
        {
            return;
        }
        if ((transform.position + dis.normalized * delta).sqrMagnitude < minDistance * minDistance)
        {
            transform.position = character.transform.position + -dis.normalized * minDistance;
        }
        else if ((transform.position + dis.normalized * delta).sqrMagnitude > maxDistance * maxDistance)
        {
            transform.position = character.transform.position + -dis.normalized * maxDistance;
        }
        else
        {
            transform.Translate(dis.normalized * delta, Space.World);
        }
        currentDistance = currentSqrMag;
    }

    private void OnDragStart()
    {
        dragStartRotation = transform.rotation.eulerAngles;
    }

    private void OnDragging(Vector3 origin, Vector3 delta)
    {
        float y_axis_delta = delta.x / Screen.width;

        float destinationAngle = y_axis_delta * (180.0f) + dragStartRotation.y;
        float angle = destinationAngle - transform.rotation.eulerAngles.y;

        transform.RotateAround(character.transform.position, Vector3.up, angle);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(character.transform.position, transform.position);

        Vector3 discCenter = new Vector3(character.transform.position.x, transform.position.y, character.transform.position.z);
        float radius = Vector3.Magnitude(discCenter - transform.position);

        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(discCenter, Vector3.up, radius);
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}