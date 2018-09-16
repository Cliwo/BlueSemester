using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour {

    CharacterManager inst_character;

    private static MinimapManager instance;
    public static MinimapManager getInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            DestroyImmediate(this);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        inst_character = CharacterManager.getInstance();
    }

    public void PinPointNavigate(Vector3 raycastStartPos)
    {
        //인자로 받는 raycastStartPos는 Chracter Local 좌표계 기준으로 계산된 Position이다.
        Vector3 raycastStartWorldPos = raycastStartPos + inst_character.transform.position;
        //3. 2에서 구한 position 에서 바닥을 향해 Raycast한다.
        RaycastHit hit;
        if(Physics.Raycast(raycastStartPos, Vector3.down, out hit))
        {
            Debug.DrawRay(raycastStartWorldPos, Vector3.down * 100, Color.red, 4.0f);
            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.transform.position = hit.point;
        }
        //4. Raycast hit 지점으로 Navigation Mesh를 돌려서 길찾기한다.
    }
}
