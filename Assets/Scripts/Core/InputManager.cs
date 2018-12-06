using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager getInstance()
    {
        return instance;
    }

    private ConversationManager inst_conv;
    private CharacterManager inst_char;

    private void Awake()
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

    private void Start()
    {
        inst_conv = ConversationManager.getInstance();
        inst_char = CharacterManager.getInstance();
        EnableInput = true;
    }

    public bool EnableInput { get; private set; }
    public event Action OnEscMenu = () => { };
    public event Action OnStand = () => { };

    public event Action<float, float> OnTranslate = (_, __) => { };

    public event Action OnJump = () => { };

    public event Action mouseLeftClickDown = () => { };

    public event Action mouseLeftClickUp = () => { };

    public event Action<float> mouseWheel = (_) => { };

    public event Action mouseRightDragStart = () => { };

    public event Action<Vector3, Vector3> mouseRightDragging = (_, __) => { };

    public event Action mouseRightDragEnd = () => { };

    public event Action firstSkill = () => { };

    public event Action secondSkill = () => { };

    public event Action combinationSkill = () => { };

    public List<InteractionObject.InteractionEventBundle> InteractionBundles = new List<InteractionObject.InteractionEventBundle>();
    public InteractionObject.InteractionEventBundle currentInteraction;

    private Vector3 mouseDragOriginPos;
    private Vector3 mouseClickOriginPos;
    private const float eventUpdateInterval = 0.3333f;
    private float leftMouseTimeBucket = 0.0f;
    private bool leftMouseWasDown = false;
    private float rightMouseTimeBucket = 0.0f;
    private bool rightMouseWasDown = false;

    // Update is called once per frame
    public void DisableInput()
    {
        EnableInput = false;
    }

    public void AllowInput()
    {
        EnableInput = true;
    }

    private void FixedUpdate()
    {
        CheckFixedCharacterInputs();
    }

    private void Update()
    {
        CheckUIInputs();
        if (EnableInput)
        {
            CheckInteractions();
            CheckCharacterInputs();
            CheckMouseInputs();
        }
    }

    private void CheckUIInputs()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inst_conv.isConversationBoxOpen)
            {
                inst_conv.NextConversation();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) //1124, OnEscMenu때문에 고침, Interaction에서 오류나면 여기 조심할것
        {
            if (currentInteraction != null)
            {    
                currentInteraction.cancelAction();
            }
            else
            {
                OnEscMenu();
            }
        }
    }

    private void CheckInteractions()
    {
        if (Input.GetKey(KeyCode.Escape) && currentInteraction != null)
        {
            currentInteraction.cancelAction();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentInteraction == null && InteractionBundles.Count != 0)
            {
                currentInteraction = InteractionBundles[0];
                InteractionBundles[0].startAction();
            }
        }
    }

    private void CheckFixedCharacterInputs()
    {
        float horizonWeight = Input.GetAxis("Horizontal");
        float verticalWeight = Input.GetAxis("Vertical");
        if (Mathf.Abs(horizonWeight) <= float.Epsilon && Mathf.Abs(verticalWeight) <= float.Epsilon)
        {
            if (!inst_char.IsNavigationStarted && !inst_char.isComboStarted)
            {
                OnStand();
            }
        }
        else
        {
            if (EnableInput)
            {
                OnTranslate(horizonWeight, verticalWeight);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }
    }

    private void CheckCharacterInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            firstSkill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            secondSkill();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            combinationSkill();
        }
    }

    private void CheckMouseInputs()
    {
        mouseWheel(Input.mouseScrollDelta.y);
        if (Input.GetMouseButtonDown(0))
        {
            mouseClickOriginPos = Input.mousePosition;
            mouseLeftClickDown();
            leftMouseWasDown = true;
            leftMouseTimeBucket = Time.time;
        }
        if (Input.GetMouseButton(0))
        {
            if (leftMouseTimeBucket + eventUpdateInterval < Time.time)
            {
                leftMouseWasDown = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (leftMouseWasDown)
            {
                mouseLeftClickUp();
                leftMouseWasDown = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightMouseWasDown = true;
            rightMouseTimeBucket = Time.time;
        }
        if (Input.GetMouseButton(1))
        {
            if (rightMouseTimeBucket + eventUpdateInterval < Time.time)
            {
                if (rightMouseWasDown)
                {
                    mouseDragOriginPos = Input.mousePosition;
                    mouseRightDragStart();
                }
                else
                {
                    mouseRightDragging(mouseDragOriginPos, Input.mousePosition - mouseDragOriginPos);
                }
                rightMouseWasDown = false;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (rightMouseWasDown)
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
