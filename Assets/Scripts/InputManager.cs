using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour {
	private static InputManager instance;
	public static InputManager getInstance()
	{
		return instance;
	}

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		if(instance != this)
		{
			DestroyImmediate(this);
		}
		DontDestroyOnLoad(this);
	}
	
	
	public event Action Button_W = ()=> { };
    public event Action Button_A = () => { };
	public event Action Button_S = () => { };
    public event Action Button_D = () => { };

    public event Action mouseLeftClick = () => { };
    public event Action<float> mouseWheel = (_) => { };

    public event Action mouseRightDragStart = () => { };
    public event Action<Vector3> mouseRightDragging = (_) => { };
    public event Action mouseRightDragEnd = () => { };
    
	Vector3 mouseDragOriginPos;
	const float eventUpdateInterval = 0.3333f;
	float leftMouseTimeBucket = 0.0f;
	bool leftMouseWasDown = false;
	float rightMouseTimeBucket = 0.0f;
	bool rightMouseWasDown = false;


    // Update is called once per frame
    void Update () {
		if(Input.GetKey(KeyCode.W))
		{
			Button_W();
		}
		if(Input.GetKey(KeyCode.A))
		{
			Button_A();
		}
		if(Input.GetKey(KeyCode.S))
		{
			Button_S();
		}
		if(Input.GetKey(KeyCode.D))
		{
			Button_D();
		}
		if(Input.GetMouseButtonDown(0))
		{
			leftMouseWasDown = true;
			leftMouseTimeBucket = Time.time;
		}
		if(Input.GetMouseButton(0))
		{
			if(leftMouseTimeBucket + eventUpdateInterval < Time.time)
			{
				leftMouseWasDown = false;
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			if(leftMouseWasDown)
			{
				mouseLeftClick();
				leftMouseWasDown = false;
			}
		}
		if(Input.GetMouseButtonDown(2))
		{
			rightMouseWasDown = true;
			rightMouseTimeBucket = Time.time;
		}
		if(Input.GetMouseButton(2))
		{
			if(rightMouseTimeBucket + eventUpdateInterval < Time.time)
			{
				if(rightMouseWasDown)
				{
					mouseRightDragStart();
					mouseDragOriginPos = Input.mousePosition;
				}
				else
				{
					mouseRightDragging(mouseDragOriginPos - Input.mousePosition);
				}
				rightMouseWasDown = false;
			}
		}
		if(Input.GetMouseButtonUp(2))
		{
			if(rightMouseWasDown)
			{
				rightMouseWasDown = false;
			}
			else
			{
				mouseRightDragEnd();
			}
		}
        mouseWheel(Input.mouseScrollDelta.y);
        
	}

}
