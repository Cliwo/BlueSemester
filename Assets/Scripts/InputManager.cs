﻿using System.Collections;
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
	
	public event Action OnTranslate = ()=> { };
	public event Action OnJump = ()=> { };
    public event Action mouseLeftClick = () => { };
    public event Action<float> mouseWheel = (_) => { };

    public event Action mouseRightDragStart = () => { };
    public event Action<Vector3> mouseRightDragging = (_) => { };
    public event Action mouseRightDragEnd = () => { };

	public event Action firstSkill = () => { };
	public event Action secondSkill = () => { };
	public event Action combinationSkill = () => { };
    
	Vector3 mouseDragOriginPos;
	const float eventUpdateInterval = 0.3333f;
	float leftMouseTimeBucket = 0.0f;
	bool leftMouseWasDown = false;
	float rightMouseTimeBucket = 0.0f;
	bool rightMouseWasDown = false;


    // Update is called once per frame
    void Update () {
		OnTranslate();	
		if(Input.GetKeyDown(KeyCode.Space))
		{
			OnJump();
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
		if(Input.GetMouseButtonDown(1))
		{
			rightMouseWasDown = true;
			rightMouseTimeBucket = Time.time;
		}
		if(Input.GetMouseButton(1))
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
		if(Input.GetMouseButtonUp(1))
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
		if(Input.GetKey(KeyCode.Alpha1))
		{
			firstSkill();
		}
		if(Input.GetKey(KeyCode.Alpha2))
		{
			secondSkill();
		}
		if(Input.GetKey(KeyCode.Alpha3))
		{
			combinationSkill();
		}
        mouseWheel(Input.mouseScrollDelta.y);
        
	}

}
