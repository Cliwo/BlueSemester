using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterManager : MonoBehaviour {
    
	public GameObject characterModel;
    [HideInInspector]
    public NavMeshAgent s_navAgent;

	public float jumpWeightConst = 2.6f;
	public float horizontalWeightConst = 0.2f;

    private CharacterController s_characterController;
    private CameraManager inst_Camera;
    private InputManager inst_Input;
	private CharacterAnimationManager inst_Anim;

    private Vector3 navigationDestination;
    private Vector3 moveDirection = Vector3.zero;
    private bool navigationStarted = false;
	public bool IsNavigationStarted {get {return navigationStarted;} }
	private bool isJumped = false;
	private const float gravity = -0.03f;
    private const float navigationEpslion = 0.4f;
    
    [SerializeField]
    private ParticleManager particles;


    private static CharacterManager instance;
    public static CharacterManager getInstance()
    {
        return instance;
    }

    public void NavigationStart(Vector3 worldPos)
    {
        navigationStarted = true;
        s_navAgent.updatePosition = true;
		//s_navAgent.updateRotation = true;
        navigationDestination = worldPos;
        s_navAgent.destination = worldPos;

		s_characterController.enabled = false;
		
		inst_Anim.animator.ResetTrigger(CharacterAnimationManager.AnimatorTrigger.Idle);
		inst_Anim.TriggerAnimator(CharacterAnimationManager.AnimatorTrigger.Walking);
		Vector3 forward = worldPos - transform.position;
		characterModel.transform.rotation = Quaternion.LookRotation(forward);
    }
	public void NavigationCancel()
	{
		if(navigationStarted)
		{
			navigationStarted = false;
			s_navAgent.updatePosition = false;
			//s_navAgent.updateRotation = false;

			s_characterController.enabled = true;
			inst_Anim.animator.ResetTrigger(CharacterAnimationManager.AnimatorTrigger.Walking);
			inst_Anim.TriggerAnimator(CharacterAnimationManager.AnimatorTrigger.Idle);
		}
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
		inst_Camera = CameraManager.getInstance();
		inst_Input = InputManager.getInstance();
		inst_Anim = CharacterAnimationManager.getInstance();

        s_navAgent = GetComponent<NavMeshAgent>();
        s_characterController = GetComponent<CharacterController>();

		inst_Input.OnStand += OnIdle;
		inst_Input.OnTranslate += OnTranslate;
		inst_Input.OnTranslate += (_, __) => NavigationCancel();
		inst_Input.OnJump += OnJump;
		inst_Input.mouseLeftClick += OnAttack;
		inst_Input.firstSkill += OnFirstSkill;
		inst_Input.secondSkill += OnSecondSkill;
		inst_Input.combinationSkill += OnCombinationSkill;

		s_navAgent.updatePosition = false;
		s_navAgent.updateRotation = false;
	}
	void Update() 
	{
		moveDirection.y += gravity;
		if(s_characterController.enabled)
		{
			s_characterController.Move(moveDirection);
		}
        NavigationCheck();
    }

	void OnIdle()
	{
		moveDirection.x = 0;
		moveDirection.z = 0;	
	}
	void OnTranslate(float horizontalWeight , float verticalWeight)
	{
		verticalWeight *= horizontalWeightConst;
		horizontalWeight *= horizontalWeightConst;

		Vector3 rightUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(1, 0, 0);
		Vector3 forwardUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(0, 0, 1);

		Vector3 dir = verticalWeight * forwardUnitVec + horizontalWeight * rightUnitVec;
		moveDirection.x = dir.x;
		moveDirection.z = dir.z;

		characterModel.transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
	}

	void OnJump()
	{
		if(s_characterController.isGrounded)
		{
			moveDirection = Vector3.up * jumpWeightConst;	
			isJumped = true;
		}
	}

	void OnAttack()
	{
		Debug.Log("Attack!");
	}
	
	void OnFirstSkill()
	{
		Debug.Log("First skill Dummy");
        particles.OnSkill();
	}

	void OnSecondSkill()
	{
		Debug.Log("Second skill Dummy");
	}
	
	void OnCombinationSkill()
	{
		Debug.Log("Combination skill Dummy");
	}

    void NavigationCheck()
    {
        if(navigationStarted)
        {
			Vector2 characterXZ = new Vector2(transform.position.x, transform.position.z);
			Vector2 dest = new Vector2(navigationDestination.x, navigationDestination.z);

            if((characterXZ-dest).sqrMagnitude < float.Epsilon)
            {
                s_navAgent.updatePosition = false;
				//s_navAgent.updateRotation = false;
                navigationStarted = false;

				s_characterController.enabled =true;
				inst_Anim.animator.ResetTrigger(CharacterAnimationManager.AnimatorTrigger.Walking);
				inst_Anim.TriggerAnimator(CharacterAnimationManager.AnimatorTrigger.Idle);
            }
        }
		if(s_navAgent.updatePosition == false)
		{
			s_navAgent.nextPosition = transform.position;
		}
    }
}
