using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinimapUIManager : MonoBehaviour, IPointerClickHandler
{
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

        verticalMinimapScale = MinimapCamera.orthographicSize;
        horizontalMinimapScale = MinimapCamera.aspect * verticalMinimapScale;

    }
    void Start()
    {
        inst_Minimap = MinimapManager.getInstance();
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

    /*
     * 1. 카메라의 Size의 단위는 unit (1m), 카메라의 size는 수직길이의 절반을 뜻한다
     * 2. 만약 16:9설정이고 size가 8이면 수직 길이는 16unit, 수평 길이는 28.444...unit이다.
     * 3. Click position은 [-1,+1] 사이로 Clamp 된다.
     * 4. 만약 click Position이 0.5 -0.5 로 주어진다면 캐릭터는 자신의 위치에서 +4unit, -7.111...unit 
     *    떨어진 곳으로 도착해야한다. 
     * 
     */

}
