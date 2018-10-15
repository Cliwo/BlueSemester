using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinimapRect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	private bool IsExpandEnable;
	private bool IsExpanding;

	private Vector2 screenSpaceAnchorPosition;
	private RectTransform rect;
	private CursorManager inst_cursor;

	void Start() {
		inst_cursor = CursorManager.getInstance();
		rect = GetComponent<RectTransform>(); 
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

	void Update() {
		if(IsExpanding)
		{
			Vector2 diff = screenSpaceAnchorPosition - new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			rect.sizeDelta = diff;
		}	
	}
   
}
