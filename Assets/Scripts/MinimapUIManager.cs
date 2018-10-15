using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapUIManager : MonoBehaviour, IPointerClickHandler
{
    public int MaxMinimapSize;
    public int MinMinimapSize;

    public GameObject MinimapCamera_g;
    public Canvas Canvas;

    Camera MinimapCamera;
    RectTransform MinimapRect;

    MinimapManager inst_Minimap;

    float verticalMinimapScale;
    float horizontalMinimapScale;

    void Awake()
    {
        MinimapRect = GetComponent<RectTransform>();
        MinimapCamera = MinimapCamera_g.GetComponent<Camera>();
        
        UpdateMinimapScale();
    }
    void Start()
    {
        inst_Minimap = MinimapManager.getInstance();
    }

    public void Minify()
    {
        if(MaxMinimapSize <= MinimapCamera.orthographicSize)
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
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(MinimapRect,
            eventData.position, eventData.pressEventCamera, out localPos))
        {
            Vector2 clamp = ClampClickPosition(localPos);
            Vector3 rayCastStart = new Vector3(clamp.x * horizontalMinimapScale, MinimapCamera.transform.position.y, 
                clamp.y * verticalMinimapScale);
            inst_Minimap.PinPointNavigate(rayCastStart);
        }
    }

    Vector2 ClampClickPosition(Vector2 pos)
    {
        float vert = (pos.y + MinimapRect.rect.height) / MinimapRect.rect.height;
        float horiz = (pos.x + MinimapRect.rect.width) / MinimapRect.rect.width;

        return new Vector2(horiz * 2 - 1, vert * 2 - 1); //[-1,+1] , [-1,+1]
    }

    void UpdateMinimapScale()
    {
        verticalMinimapScale = MinimapCamera.orthographicSize;
        horizontalMinimapScale = MinimapCamera.aspect * verticalMinimapScale;
    }
}
