using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterManager : MonoBehaviour {
    
	public GameObject characterModel;
    //TODO(0916) : NavMeshAgent를 적용하고나서 점프가 작동이 이상함.
    [HideInInspector]
    public NavMeshAgent s_navAgent;

	public float jumpWeightConst = 2.6f;

    private CharacterController s_characterController;
    private CameraManager inst_Camera;
    private InputManager inst_Input;

    private Vector3 navigationDestination;
    private Vector3 moveDirection = Vector3.zero;
    private bool navigationStarted = false;
	private bool isJumped = false;
	private const float gravity = -0.03f;
	private const float horizontalWeightConst = 0.2f;
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
        navigationDestination = worldPos;
        s_navAgent.destination = worldPos;
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

        s_navAgent = GetComponent<NavMeshAgent>();
        s_characterController = GetComponent<CharacterController>();

		inst_Input.OnStand += OnIdle;
		inst_Input.OnTranslate += OnTranslate;
		inst_Input.OnJump += OnJump;
		inst_Input.mouseLeftClick += OnAttack;
		inst_Input.firstSkill += OnFirstSkill;
		inst_Input.secondSkill += OnSecondSkill;
		inst_Input.combinationSkill += OnCombinationSkill;
	}
	void Update() 
	{
		moveDirection.y += gravity;
		s_characterController.Move(moveDirection);

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
            if((transform.position - navigationDestination).sqrMagnitude < navigationEpslion)
            {
                s_navAgent.updatePosition = false;
                navigationStarted = false;
            }
        }
    }
}
