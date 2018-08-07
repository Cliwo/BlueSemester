using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinimapUI : MonoBehaviour, IPointerClickHandler{

    public RectTransform pinPointImage;
    [HideInInspector]
    public RawImage pinImage;

    private void Awake()
    {
        pinImage = pinPointImage.GetComponent<RawImage>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        pinPointImage.localPosition = new Vector3(eventData.position.x - Screen.width / 2, eventData.position.y - Screen.height / 2, 0);
        ShowPin();

        RectTransform rectTrans = GetComponent<RectTransform>();
        //https://answers.unity.com/questions/1013011/convert-recttransform-rect-to-screen-space.html
        Vector2 size = Vector2.Scale(rectTrans.rect.size, rectTrans.lossyScale);
        Vector2 normalizedDir = (new Vector2(pinPointImage.position.x, pinPointImage.position.y) - new Vector2(rectTrans.position.x, rectTrans.position.y)) / size;
        Debug.Log(normalizedDir);
        MinimapManager.instance.OnMinimapPinEneabled(normalizedDir);
    }

    public void ShowPin()
    {
        pinImage.enabled = true;
    }

    public void HidePin()
    {
        pinImage.enabled = false;
    }
}
