using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect_Cinema : MonoBehaviour {

	public Animator TopImage;
	public Animator BottomImage;
	
	bool isStarted = false;
	
	public void StartAnimation(float waitTime = float.PositiveInfinity)
	{
		StartCoroutine(AnimationCoroutine(waitTime));
	}
	public void CancelAnimation()
	{
		if(isStarted)
		{
			StopCoroutine("StartAnimation");
			CloseAnim();
			isStarted = false;
		}
	}
	IEnumerator AnimationCoroutine(float waitTime = float.PositiveInfinity)
	{
		if(!isStarted)
		{
			StartAnim();
			isStarted = true;
		}
		yield return new WaitForSeconds(waitTime);
		if(isStarted)
		{
			CloseAnim();
			isStarted = false;
		}
	}
	void StartAnim()
	{
		TopImage.SetTrigger("Appear");
		BottomImage.SetTrigger("Appear");
	}

	void CloseAnim()
	{
		TopImage.SetTrigger("Disappear");
		BottomImage.SetTrigger("Disappear");
	}


}
