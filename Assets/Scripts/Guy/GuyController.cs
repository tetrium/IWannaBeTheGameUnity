using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyController : MonoBehaviour,IDamageReporter
{
    [Header("Animation")]
    [SerializeField] AnimatorController animatorController;
 
    [Header("Movement")]
    public bool playerIsOnGround = false;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float doubleJumpForce = 12;
    [SerializeField] float jumpForce = 12;
    [SerializeField] float speed = 8;
    // Controls

    private Vector2 axisDirection;
    private Vector2 lastAxisDirection;
    private bool canDoubleJump = false;
    private bool jumpButtonPressed = false;

    [Header("Player States")]
    public bool playerIsDead = false;

    [Header("Effects")]
    [SerializeField] GameObject damagePrefab;


    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
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
    }

    void CheckGround() {
        playerIsOnGround = Physics2D.Raycast(this.transform.position, Vector2.down, 0.6f, groundMask);
    }
    void GetControls() {
        axisDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpButtonPressed = Input.GetButtonDown("Jump");

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
            _rigidBody2D.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
        if (jumpButtonPressed&& playerIsOnGround) {

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
}
