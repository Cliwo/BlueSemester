using UnityEngine;
using System.Collections;

public class UnderwaterPostEffects : MonoBehaviour
{
	public Color FogColor = new Color(87f/255f, 190f/255f, 219f/255f, 1);
    public float FogDensity = 0.05f;
    public bool UseSunShafts = true;
    public float SunShuftsIntensity = 1.5f;
    public ShaftsScreenBlendMode SunShuftsScreenBlendMode = ShaftsScreenBlendMode.Screen;


    private Vector3 SunShaftTargetPosition = new Vector3(0, 7, 10);
    private Camera cam;
    private SunShafts SunShafts;

    void OnEnable()
    {
        cam = Camera.main;
        SunShafts = cam.gameObject.AddComponent<SunShafts>();
        SunShafts.sunShaftIntensity = SunShuftsIntensity;
        var target = new GameObject("SunShaftTarget");
        SunShafts.sunTransform = target.transform;
        target.transform.parent = cam.transform;
        target.transform.localPosition = SunShaftTargetPosition;
        SunShafts.screenBlendMode = SunShuftsScreenBlendMode;
        SunShafts.sunShaftsShader = Shader.Find("Hidden/SunShaftsComposite");
        SunShafts.simpleClearShader = Shader.Find("Hidden/SimpleClear");

        var underwater = cam.gameObject.AddComponent<Underwater>();
        underwater.UnderwaterLevel = transform.position.y;
        underwater.FogColor = FogColor;
        underwater.FogDensity = FogDensity;
    }


	// Update is called once per frame
	void Update ()
	{
	    if (cam==null)
	        return;
	    if (cam.transform.position.y < transform.position.y) {
	        if (!SunShafts.enabled)
	            SunShafts.enabled = true;
	    }
        else if (SunShafts.enabled)
            SunShafts.enabled = false;
	}
}