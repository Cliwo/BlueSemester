using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterManager : MonoBehaviour {
    
    //TODO(0916) : NavMeshAgent를 적용하고나서 점프가 작동이 이상함.
    public NavMeshAgent s_navAgent;

	public float jumpWeightConst = 2.6f;

    private CharacterController s_characterController;
    private CameraManager inst_Camera;
    private InputManager inst_Input;

    private Vector3 moveDirection = Vector3.zero;
	private bool isJumped = false;
	private const float gravity = -0.16f;

    private static CharacterManager instance;
    public static CharacterManager getInstance()
    {
        return instance;
    }

    public void NavigationStart(Vector3 worldPos)
    {
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

		inst_Input.OnTranslate += OnTranslate;
		inst_Input.OnJump += OnJump;
		inst_Input.mouseLeftClick += OnAttack;
		inst_Input.firstSkill += OnFirstSkill;
		inst_Input.secondSkill += OnSecondSkill;
		inst_Input.combinationSkill += OnCombinationSkill;
	}
	void Update() 
	{
		if(!s_characterController.isGrounded)
		{
			moveDirection += Vector3.up * gravity;
		}
		else if (s_characterController.isGrounded && moveDirection.y < 0)
		{
			moveDirection.y = 0f;
			isJumped = false;
		}
		s_characterController.Move(moveDirection);
		
	}
	
	void OnTranslate()
	{
		float verticalWeight = Input.GetAxis("Vertical");
		float horizontalWeight = Input.GetAxis("Horizontal");

		Vector3 rightUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(1, 0, 0);
		Vector3 forwardUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(0, 0, 1);

		Vector3 dir = verticalWeight * forwardUnitVec + horizontalWeight * rightUnitVec;
		moveDirection.x = dir.x;
		moveDirection.z = dir.z;
	}

	void OnJump()
	{
		if(!isJumped)
		{
			moveDirection += Vector3.up * jumpWeightConst;	
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
	}

	void OnSecondSkill()
	{
		Debug.Log("Second skill Dummy");
	}
	
	void OnCombinationSkill()
	{
		Debug.Log("Combination skill Dummy");
	}

}
