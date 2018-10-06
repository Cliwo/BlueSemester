using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;
public class InputManager : MonoBehaviour {
	private static InputManager instance;
	public static InputManager getInstance()
	{
		return instance;
	}
	
	private ConversationManager inst_conv;

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
	
	void Start() {
		inst_conv = ConversationManager.getInstance();	
		EnableInput = true;
	}
	public bool EnableInput{ get; private set; }
	public event Action OnTranslate = ()=> { };
	public event Action OnJump = ()=> { };
    public event Action mouseLeftClick = () => { };
    public event Action<float> mouseWheel = (_) => { };

    public event Action mouseRightDragStart = () => { };
    public event Action<Vector3, Vector3> mouseRightDragging = (_, __) => { };
    public event Action mouseRightDragEnd = () => { };

	public event Action firstSkill = () => { };
	public event Action secondSkill = () => { };
	public event Action combinationSkill = () => { };

	public List<InteractionObject.InteractionEventBundle> InteractionBundles = new List<InteractionObject.InteractionEventBundle>();
	public InteractionObject.InteractionEventBundle currentInteraction;

	Vector3 mouseDragOriginPos;
	const float eventUpdateInterval = 0.3333f;
	float leftMouseTimeBucket = 0.0f;
	bool leftMouseWasDown = false;
	float rightMouseTimeBucket = 0.0f;
	bool rightMouseWasDown = false;

    // Update is called once per frame
	public void DisableInput()
	{
		EnableInput = false;
	}

	public void AllowInput()
	{
		EnableInput = true;
	}

    void Update () {
		CheckUIInputs();
		if(EnableInput)
		{
			CheckInteractions();
			CheckCharacterInputs();
        	CheckCameraInputs();
		}
	}

	private void CheckUIInputs()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			if(inst_conv.isConversationBoxOpen)
			{
				inst_conv.NextConversation();
			}
		}
		if(Input.GetKey(KeyCode.Escape))
		{
			if(currentInteraction != null)
				currentInteraction.cancelAction();
		}
	}
	private void CheckInteractions()
	{
		if(Input.GetKey(KeyCode.Escape) && currentInteraction != null)
		{
			//행동 취소
			currentInteraction.cancelAction();
		}
		if(Input.GetKeyDown(KeyCode.F))
		{
			//행동 시작
			if(currentInteraction == null && InteractionBundles.Count != 0)
			{
				currentInteraction = InteractionBundles[0];
				InteractionBundles[0].startAction();
			}
		}
	}
	private void CheckCharacterInputs()
	{
		OnTranslate();
		if(Input.GetKeyDown(KeyCode.Space))
		{
			OnJump();
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
	}
	private void CheckCameraInputs()
	{
		mouseWheel(Input.mouseScrollDelta.y);
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
					mouseDragOriginPos = Input.mousePosition;
					mouseRightDragStart();
				}
				else
				{
					mouseRightDragging(mouseDragOriginPos , mouseDragOriginPos - Input.mousePosition);
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
	}
}
