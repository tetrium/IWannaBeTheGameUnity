using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuyController : MonoBehaviour, IDamageReporter, ISceneStateReporter
{
    [Header("Animation")]
    [SerializeField] AnimatorController animatorController;

    [Header("Projectile")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunPivot;
    [SerializeField] float shootForce = 12;
    [Header("Movement")]
    public bool playerIsOnGround = false;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float doubleJumpForce = 12;
    [SerializeField] float jumpForce = 12;
    [SerializeField] float speed = 8;
    [Header("Player States")]
    public bool guyIsShooting;

    // Controls

    private Vector2 axisDirection;
    private Vector2 lastAxisDirection;
    private bool canDoubleJump = false;
    private bool jumpButtonPressed = false;
    private bool fireButtonPressed = false;
    [Header("Player States")]
    public bool playerIsDead = false;

    [Header("Effects")]
    [SerializeField] GameObject damagePrefab;
    [Header("Sfx")]
    [SerializeField] AudioClip shootSfx;
    [SerializeField] AudioClip jumpSfx;


    private Rigidbody2D _rigidBody2D;
    public SceneId sceneFrom;

    private static GuyController _instace;

    public static GuyController instance {
        get {
            if (_instace == null) {
                _instace = FindObjectOfType<GuyController>();
            }
            DontDestroyOnLoad(_instace.gameObject);

            return _instace;
        }
        set {
            _instace = value;
        }
    }


    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(_instace.gameObject);
        }

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        SceneHelper.instance.AddNewObserver(this);

        animatorController.PlayAnimation(AnimationId.Idle);
        _rigidBody2D = GetComponent<Rigidbody2D>();
     
    }
    void Update()
    {
        

        CheckGround();
        GetControls();
        UpdateMovement();
        UpdateLookDirection();
        HandleJump();
        HandleAttack();
    }

    void HandleAttack() {
        if (fireButtonPressed && !guyIsShooting) {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot() {
        guyIsShooting = true;
        AudioManager.instance.PlaySfx(shootSfx);
       var projectile= Instantiate(bulletPrefab, gunPivot.position, Quaternion.identity);
        projectile.GetComponent<BulletController>().Shoot(this.transform.right* shootForce);
        Destroy(projectile.gameObject, 4);

        yield return new WaitForSeconds(0.2f);
        guyIsShooting = false;
    }
    void CheckGround() {
        playerIsOnGround = Physics2D.Raycast(this.transform.position, Vector2.down+Vector2.right*0.03f, 0.6f, groundMask)||
            Physics2D.Raycast(this.transform.position, Vector2.down + Vector2.left * 0.03f, 0.6f, groundMask);

    }
    void GetControls() {
        axisDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpButtonPressed = Input.GetButtonDown("Jump");
        fireButtonPressed = Input.GetButtonDown("Fire1");
        if (Mathf.Abs(axisDirection.x) > 0) {
            lastAxisDirection.x = axisDirection.x;
        }
        if (Mathf.Abs(axisDirection.y) > 0)
        {
            lastAxisDirection.y = axisDirection.y;
        }
    }
    void HandleJump() {
        if (canDoubleJump && jumpButtonPressed && !playerIsOnGround) {
            _rigidBody2D.velocity = Vector2.zero;
            AudioManager.instance.PlaySfx(jumpSfx);

            _rigidBody2D.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
        if (jumpButtonPressed&& playerIsOnGround) {
            AudioManager.instance.PlaySfx(jumpSfx);

            canDoubleJump = true;

            _rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (!playerIsOnGround)
        {
            if (_rigidBody2D.velocity.y > 0)
            {
                animatorController.PlayAnimation(AnimationId.Jump);

            }
            if (_rigidBody2D.velocity.y < 0)
            {
                animatorController.PlayAnimation(AnimationId.Fall);

            }

        }


    }
    void UpdateMovement() {
        _rigidBody2D.velocity = new Vector2(axisDirection.x * speed, _rigidBody2D.velocity.y);
        if (playerIsOnGround) {
          
                if (Mathf.Abs(_rigidBody2D.velocity.x) > 0)
                {
                    animatorController.PlayAnimation(AnimationId.Walk);


                }
                else {
                    animatorController.PlayAnimation(AnimationId.Idle);

                }
            
        }
    }

    void UpdateLookDirection() {
        if (lastAxisDirection.x >= 0)
        {
            this.transform.rotation = Quaternion.identity;
        }
        else {
            this.transform.rotation = Quaternion.Euler(0,180,0);

        }
    }

    public void TakeDamage(int damagePoints)
    {

        if (!playerIsDead) {
            playerIsDead = true;
            var go= Instantiate(damagePrefab, this.transform.position, Quaternion.Euler(-90,0,0));
            Destroy(go, 2);
            Destroy(this.gameObject);
        }



    }
    public void OnStartLoadNewLevel() {
        this.transform.position = new Vector3(-10, 0, 0);
        sceneFrom = SceneHelper.instance.GetCurrentSceneId();
    }
    public void OnLoadNewLevel()
    {

        var returnPortal = FindObjectsOfType<ScenePortal>().FirstOrDefault(x=>x.GetSceneToLoad()== sceneFrom);
        if (returnPortal != null) {
            this.transform.position = returnPortal.GetSpawnPosition();
        }

    }
}
