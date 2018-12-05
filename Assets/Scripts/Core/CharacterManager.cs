using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManager : Pawn
{
    public GameObject characterModel;

    [HideInInspector]
    public NavMeshAgent s_navAgent;

    public float jumpWeightConst = 2.6f;

    private CharacterController s_characterController;
    private CameraManager inst_Camera;
    private InputManager inst_Input;
    private CharacterAnimationManager inst_Anim;

    private Vector3 navigationDestination;
    private Vector3 moveDirection = Vector3.zero;
    private bool navigationStarted = false;
    public bool IsNavigationStarted { get { return navigationStarted; } }
    private bool isJumped = false;
    private const float gravity = -0.03f;

    private Hit wand;

    [SerializeField]
    private GameObject fireSkillForDebug;

    [SerializeField]
    private GameObject secondSkillForDebug;

    public Transform targetForSkillDebug;

    private static CharacterManager instance;

    public static CharacterManager getInstance()
    {
        return instance;
    }

    public void NavigationStart(Vector3 worldPos)
    {
        navigationStarted = true;
        s_navAgent.updatePosition = true;
        navigationDestination = worldPos;
        s_navAgent.destination = worldPos;

        s_characterController.enabled = false;
        inst_Anim.animator.SetFloat(CharacterAnimationManager.AnimatorTrigger.Translation, 0.9f);
        inst_Anim.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, false);

        Vector3 forward = worldPos - transform.position;
        characterModel.transform.rotation = Quaternion.LookRotation(forward);
    }

    public void NavigationCancel()
    {
        if (navigationStarted)
        {
            navigationStarted = false;
            s_navAgent.updatePosition = false;

            s_characterController.enabled = true;
            inst_Anim.animator.SetFloat(CharacterAnimationManager.AnimatorTrigger.Translation, 0.0f);
            inst_Anim.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, true);
        }
    }

    override protected void InitStatus()
    {
        hp = 500f;
        horizontalSpeed = 0.2f;
    }

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

        // effectManager = GetComponentInChildren<EffectManager>();
    }

    override protected void Start()
    {
        base.Start();
        inst_Camera = CameraManager.getInstance();
        inst_Input = InputManager.getInstance();
        inst_Anim = CharacterAnimationManager.getInstance();

        s_navAgent = GetComponent<NavMeshAgent>();
        s_characterController = GetComponent<CharacterController>();

        inst_Input.OnStand += OnIdle;
        inst_Input.OnTranslate += OnTranslate;
        inst_Input.OnTranslate += (_, __) => NavigationCancel();
        inst_Input.OnJump += OnJump;
        inst_Input.mouseLeftClickDown += OnArrow;
        inst_Input.mouseLeftClickUp += OnAttack;
        inst_Input.firstSkill += OnFirstSkill;
        inst_Input.secondSkill += OnSecondSkill;
        inst_Input.combinationSkill += OnCombinationSkill;

        s_navAgent.updatePosition = false;
        s_navAgent.updateRotation = false;
    }

    override protected void Update()
    {
        base.Update();
        moveDirection.y += gravity;
        if (lockOtherComponentInfluenceOnTransform)
        {
            return; //Navaigation 과 입력이동을 모두 막는다.
        }
        if (s_characterController.enabled)
        {
            s_characterController.Move(moveDirection); //TODO : input에서는 Fixed에서 Update하는데 얘는 Update에서 함
        }
        NavigationCheck();
    }

    private void OnIdle()
    {
        moveDirection.x = 0;
        moveDirection.z = 0;

        inst_Anim.animator.SetFloat(CharacterAnimationManager.AnimatorTrigger.Translation, 0.0f);
        inst_Anim.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, true);
    }

    private void OnTranslate(float horizontalWeight, float verticalWeight)
    {
        inst_Anim.animator.SetFloat(CharacterAnimationManager.AnimatorTrigger.Translation, Mathf.Abs(horizontalWeight) + Mathf.Abs(verticalWeight));
        inst_Anim.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, false);

        verticalWeight *= horizontalSpeed;
        horizontalWeight *= horizontalSpeed;

        Vector3 rightUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(1, 0, 0);
        Vector3 forwardUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(0, 0, 1);

        Vector3 dir = verticalWeight * forwardUnitVec + horizontalWeight * rightUnitVec;
        moveDirection.x = dir.x;
        moveDirection.z = dir.z;

        characterModel.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
    }

    private void OnJump()
    {
        if (s_characterController.isGrounded)
        {
            moveDirection = Vector3.up * jumpWeightConst;
            isJumped = true;
        }
    }

    private void NavigationCheck()
    {
        if (navigationStarted)
        {
            Vector2 characterXZ = new Vector2(transform.position.x, transform.position.z);
            Vector2 dest = new Vector2(navigationDestination.x, navigationDestination.z);

            if ((characterXZ - dest).sqrMagnitude < float.Epsilon)
            {
                s_navAgent.updatePosition = false;
                navigationStarted = false;

                s_characterController.enabled = true;
                inst_Anim.animator.SetFloat(CharacterAnimationManager.AnimatorTrigger.Translation, 0.0f);
                inst_Anim.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, true);
            }
        }
        if (s_navAgent.updatePosition == false)
        {
            s_navAgent.nextPosition = transform.position;
        }
    }

    private void OnArrow()
    {
        DrawArrow();
    }

    private void DrawArrow()
    {
    }

    private void OnAttack()
    {
        MeleeAttack();
    }

    private void OnFirstSkill()
    {
        Skill.GenerateSkill(fireSkillForDebug, targetForSkillDebug.position);
    }

    private void OnSecondSkill()
    {
        Skill.GenerateSkill(secondSkillForDebug, transform.position);
    }

    private void OnCombinationSkill()
    {
    }

    private void MeleeAttack()
    {
        Debug.Log("Attack!");
        if (wand)
        {
            wand = transform.Find("Wand").GetComponent<Hit>();
            wand.GetComponent<CapsuleCollider>().enabled = true;
        }
        StartCoroutine("AttackDelay");
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(3);
        if (wand)
        {
            wand.GetComponent<CapsuleCollider>().enabled = false;
        }
        Debug.Log("OffAttack");
    }
}