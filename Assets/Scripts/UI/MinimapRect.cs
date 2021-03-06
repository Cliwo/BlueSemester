﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapRect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	private bool IsExpandEnable;
	private bool IsExpanding;

	public RectTransform realMinimap;
	public int MaximumRectSize;
	public int MinimumRectSize;

	private Image image; 
	private RectTransform rect;

	private Vector2 screenSpaceAnchorPosition;
	private CursorManager inst_cursor;

	void Start() {
		inst_cursor = CursorManager.getInstance();
		
		rect = GetComponent<RectTransform>();
		image = GetComponent<Image>();
		image.alphaHitTestMinimumThreshold = 0.6f;
	}
    public void OnPointerEnter(PointerEventData eventData)
    {
        inst_cursor.SetCursor(CursorManager.CursorConstant.EXPAND);
		IsExpandEnable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inst_cursor.SetCursor(CursorManager.CursorConstant.NORMAL);
		IsExpandEnable = false;
    }

	public void OnPointerDown(PointerEventData eventData)
    {
		if(IsExpandEnable)
		{
			IsExpanding = true;
			float x = rect.pivot.x * Screen.width;
			float y = rect.pivot.y * Screen.height;

			screenSpaceAnchorPosition = new Vector2(x, y) - rect.anchoredPosition;
		}
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        IsExpanding = false;
    }

	void Update() 
	{
		if(IsExpanding)
		{	
			Vector2 diff = screenSpaceAnchorPosition - new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			diff.x = Mathf.Clamp(diff.x, MinimumRectSize, MaximumRectSize);
			diff.y = Mathf.Clamp(diff.y, MinimumRectSize, MaximumRectSize);

			Vector2 minimapDiff = new Vector2(diff.x - 100f, diff.y - 100f); //100f 는 처음 미니맵 width height와 border 의 width height의 차이
			
			realMinimap.sizeDelta = minimapDiff;
			rect.sizeDelta = diff;
		}	
	}
   
}
