using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyGenerator : MonoBehaviour {

	private CameraManager inst_cam;
	private InputManager inst_input;
	private CameraEffect_Cinema cameraEffect;
	public GameObject Fairy;
	public GameObject CameraSocket;
	public SpawnEnemy Stage;
	public float CameraAnimationDuration;
	public float PauseDuration;
	
	private Vector3 originCameraPos;
	private Quaternion originCameraRot;
	private float startedTime;

	void Start()
	{
		inst_input = InputManager.getInstance();
		inst_cam = CameraManager.getInstance();
		Stage.ConsumeEvents += InstantiateFairy;
		cameraEffect = FindObjectOfType<CameraEffect_Cinema>();

		Fairy.SetActive(false);
	}

	void InstantiateFairy()
	{
		originCameraPos = inst_cam.transform.position;
		originCameraRot = inst_cam.transform.rotation;

		inst_input.DisableInput();

		Fairy.SetActive(true);
		cameraEffect.StartAnimation();

		startedTime = Time.time;
		StartCoroutine("CameraAnimation");
	}

	IEnumerator CameraAnimation()
	{
		while(true)
		{
			if(Time.time - startedTime < CameraAnimationDuration + PauseDuration)
			{
				inst_cam.transform.position = Vector3.Lerp(originCameraPos, CameraSocket.transform.position, ((Time.time - startedTime)/CameraAnimationDuration) );
				inst_cam.transform.rotation = Quaternion.Lerp(originCameraRot, CameraSocket.transform.rotation, ((Time.time - startedTime)/CameraAnimationDuration) );

				if(Time.time - startedTime > CameraAnimationDuration)
				{
					cameraEffect.CancelAnimation();
				}
				yield return new WaitForEndOfFrame();
			}
			else if (Time.time - startedTime < CameraAnimationDuration * 2 + PauseDuration)
			{
				inst_cam.transform.position = Vector3.Lerp(CameraSocket.transform.position, originCameraPos, ((Time.time - startedTime - PauseDuration - CameraAnimationDuration)/CameraAnimationDuration) );
				inst_cam.transform.rotation = Quaternion.Lerp(CameraSocket.transform.rotation, originCameraRot, ((Time.time - startedTime - PauseDuration - CameraAnimationDuration)/CameraAnimationDuration) );

				yield return new WaitForEndOfFrame();
			}
			else
			{
				inst_input.AllowInput();
				yield return null;
			}
		}
		

	}

}
