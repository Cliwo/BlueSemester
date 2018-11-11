using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterManager : MonoBehaviour
{
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
    public bool IsNavigationStarted { get { return navigationStarted; } }
    private bool isJumped = false;
    private const float gravity = -0.03f;
    private const float navigationEpslion = 0.4f;
    private float hp = 500;
    private float attack = 10;
    private float speed = 10;
    private float cooldown = 5;
    private float eveasion = 1;

    [SerializeField]
    private ParticleManager particles;

    private EffectManager effectManager;

    //public Knockback skillFirst = new Knockback();
    public Weakness skillSecond = new Weakness();
    public FireWater skillCombo = new FireWater();

    private Hit wand;
    private SphereCollider myCollider;

    [SerializeField]
    private GameObject bullet;

    private BulletManager bulletManager;

    [SerializeField]
    private GameObject fireSkillForDebug;

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

        effectManager = GetComponentInChildren<EffectManager>();
    }

    private void Start()
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
        inst_Input.mouseLeftClickDown += OnArrow;
        inst_Input.mouseLeftClickUp += OnAttack;
        inst_Input.firstSkill += OnFirstSkill;
        inst_Input.secondSkill += OnSecondSkill;
        inst_Input.combinationSkill += OnCombinationSkill;

        s_navAgent.updatePosition = false;
        s_navAgent.updateRotation = false;
    }

    private void Update()
    {
        moveDirection.y += gravity;
        if (s_characterController.enabled)
        {
            s_characterController.Move(moveDirection);
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

        verticalWeight *= horizontalWeightConst;
        horizontalWeight *= horizontalWeightConst;

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
        Skill.GenerateSkill(fireSkillForDebug, targetForSkillDebug.position, Quaternion.identity);
    }

    private void OnSecondSkill()
    {
        inst_Anim.animator.SetTrigger(CharacterAnimationManager.AnimatorTrigger.Skill2);
        inst_Anim.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, false);
        RangeAttack(skillSecond);
    }

    private void OnCombinationSkill()
    {
        inst_Anim.animator.SetTrigger(CharacterAnimationManager.AnimatorTrigger.Skill3);
        inst_Anim.animator.SetBool(CharacterAnimationManager.AnimatorTrigger.Idle, false);
        RangeAttack(skillCombo);
    }

    private void MeleeAttack()
    {
        // Debug.Log("Attack!");
        // wand.GetComponent<CapsuleCollider>().enabled = true;
        // StartCoroutine("AttackDelay");
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            effectManager.StartEffects("PlayerHit");
            Debug.Log("player Damaged!!!!!!!!!!!!!!!!!");
            myCollider.isTrigger = true;
            collision.collider.isTrigger = true;
            collision.rigidbody.isKinematic = true;
            StartCoroutine(DamageDelay(collision));
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