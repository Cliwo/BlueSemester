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
        //인자인 raycastStartPos는 chracter 의 position을 기준으로하는 로컬좌표계에서 방향을 나타낸다. World 기준으로 바꿔주어야한다.
        Vector3 raycastStartWorldPos = raycastStartPos + inst_character.transform.position;
        RaycastHit hit;
        if(Physics.Raycast(raycastStartWorldPos, Vector3.down, out hit))
        {
            //4. Raycast hit 지점으로 Navigation Mesh를 돌려서 길찾기한다.
            inst_character.NavigationStart(hit.point);
            DebugNavigation(hit.point);
        }
       
    }

    private void DebugNavigation(Vector3 pos)
    {
        //후에 삭제할 것 
        GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        point.transform.position = pos;
        point.GetComponent<SphereCollider>().enabled = false;
    }
}
