using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CharacterManager : MonoBehaviour
{
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
    private const float gravity = -0.16f;
    private const float navigationEpslion = 0.4f;

    private float hp = 500;
    private float attack = 10;
    private float speed = 10;
    private float cooldown = 5;
    private float eveasion = 1;

    [SerializeField]
    private ParticleManager particles;

    public Knockback skillFirst = new Knockback();
    public Weakness skillSecond = new Weakness();
    public FireWater skillCombo = new FireWater();

    private Hit wand;
    private SphereCollider myCollider;

    [SerializeField]
    private GameObject bullet;

    private BulletManager bulletManager;

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

        wand = transform.Find("Wand").GetComponent<Hit>();
        myCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (!s_characterController.isGrounded)
        {
            moveDirection += Vector3.up * gravity;
        }
        else if (s_characterController.isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
            isJumped = false;
        }
        s_characterController.Move(moveDirection);

        NavigationCheck();
    }

    private void OnTranslate()
    {
        float verticalWeight = Input.GetAxis("Vertical");
        float horizontalWeight = Input.GetAxis("Horizontal");

        Vector3 rightUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(1, 0, 0);
        Vector3 forwardUnitVec = inst_Camera.transform.localToWorldMatrix * new Vector4(0, 0, 1);

        Vector3 dir = verticalWeight * forwardUnitVec + horizontalWeight * rightUnitVec;
        moveDirection.x = dir.x;
        moveDirection.z = dir.z;
    }

    private void OnJump()
    {
        if (!isJumped)
        {
            moveDirection += Vector3.up * jumpWeightConst;
            isJumped = true;
        }
    }

    private void OnAttack()
    {
        MeleeAttack();
    }

    private void OnFirstSkill()
    {
        RangeAttack(skillFirst);
    }

    private void OnSecondSkill()
    {
        RangeAttack(skillSecond);
    }

    private void OnCombinationSkill()
    {
        RangeAttack(skillCombo);
    }

    private void MeleeAttack()
    {
        Debug.Log("Attack!");
        wand.GetComponent<CapsuleCollider>().enabled = true;
        StartCoroutine("AttackDelay");
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(3);
        wand.GetComponent<CapsuleCollider>().enabled = false;
        Debug.Log("OffAttack");
    }

    private void RangeAttack(ICrowdControlSkill skill)
    {
        Debug.Log("Attack - " + skill);
        GameObject go = Instantiate(bullet, this.transform.position, this.transform.rotation);
        bulletManager = go.GetComponent<BulletManager>();
        bulletManager.skill = skill;
    }

    private void NavigationCheck()
    {
        if (navigationStarted)
        {
            if ((transform.position - navigationDestination).sqrMagnitude < navigationEpslion)
            {
                s_navAgent.updatePosition = false;
                navigationStarted = false;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("player Damaged!!!!!!!!!!!!!!!!!");
            myCollider.isTrigger = true;
            collision.collider.isTrigger = true;
            collision.rigidbody.isKinematic = true;
            DamageDelay(collision);
        }
    }

    private IEnumerator DamageDelay(Collision collision)
    {
        yield return new WaitForSeconds(1);
        myCollider.isTrigger = false;
        collision.collider.isTrigger = false;
        collision.rigidbody.isKinematic = false;
    }
}