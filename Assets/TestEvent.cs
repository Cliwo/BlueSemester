using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour {

    bool previouseFrameMouseDown = false;
    bool currentFrameMouseDown = false;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentFrameMouseDown = true;
            Debug.Log("MouseDown");
        }
        if(Input.GetMouseButton(0))
        {
            currentFrameMouseDown = false;
            Debug.Log("OnHold");
        }
        if(Input.GetMouseButtonUp(0))
        {
            previouseFrameMouseDown = currentFrameMouseDown;
            currentFrameMouseDown = false;

            if(previouseFrameMouseDown ^ currentFrameMouseDown)
            {
                Debug.Log("Clicked");
            }
            else
            {
                Debug.Log("MouseUp");
            }
        }
    }
}
