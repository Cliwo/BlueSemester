using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapUIManager : MonoBehaviour, IPointerClickHandler
{
    public int MaxMinimapSize;
    public int MinMinimapSize;
    public Camera MinimapCamera;
    private Canvas canvas;
    private RectTransform MinimapRect;

    private MinimapManager inst_Minimap;
    private CharacterManager inst_Character;

    private float verticalMinimapScale;
    private float horizontalMinimapScale;

    private void Awake()
    {
        MinimapRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        inst_Character = CharacterManager.getInstance();
        MinimapCamera = inst_Character.GetComponent<Transform>().Find("MinimapCamera").GetComponent<Camera>();
        UpdateMinimapScale();
        inst_Minimap = MinimapManager.getInstance();
    }

    public void Minify()
    {
        if (MaxMinimapSize <= MinimapCamera.orthographicSize)
        {
            return;
        }
        MinimapCamera.orthographicSize += 2;
        UpdateMinimapScale();
    }

    public void Magnify()
    {
        if (MinMinimapSize >= MinimapCamera.orthographicSize)
        {
            return;
        }
        MinimapCamera.orthographicSize -= 2;
        UpdateMinimapScale();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(MinimapRect,
            eventData.position, eventData.pressEventCamera, out localPos))
        {
            Vector2 clamp = ClampClickPosition(localPos);
            Vector3 rayCastStart = new Vector3(clamp.x * horizontalMinimapScale, MinimapCamera.transform.position.y,
                clamp.y * verticalMinimapScale);
            inst_Minimap.PinPointNavigate(rayCastStart);
        }
    }

    private Vector2 ClampClickPosition(Vector2 pos)
    {
        float vert = (pos.y + MinimapRect.rect.height) / MinimapRect.rect.height;
        float horiz = (pos.x + MinimapRect.rect.width) / MinimapRect.rect.width;

        return new Vector2(horiz * 2 - 1, vert * 2 - 1); //[-1,+1] , [-1,+1]
    }

    private void UpdateMinimapScale()
    {
        verticalMinimapScale = MinimapCamera.orthographicSize;
        horizontalMinimapScale = MinimapCamera.aspect * verticalMinimapScale;
    }
}