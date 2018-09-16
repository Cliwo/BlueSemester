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
        Quaternion characterYRotation = Quaternion.Euler(0, inst_character.transform.rotation.eulerAngles.y, 0);
        Vector3 raycastStartWorldPos = characterYRotation * raycastStartPos + inst_character.transform.position;

        RaycastHit hit;
        if(Physics.Raycast(raycastStartWorldPos, Vector3.down, out hit))
        {
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
