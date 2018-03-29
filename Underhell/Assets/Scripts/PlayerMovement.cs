using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] [Range(0f, 0.1f)] private float pickingUpSpeedPercentage = 0.25f;
    [SerializeField] private float maxHorizontalVelocity = 35f;
    [SerializeField] private float maxVerticalVelocity = 20f;
    [SerializeField] private float movementSpeed = 250f;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashCooldown = 2f;

    [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode dashLeftKey = KeyCode.Q;
    [SerializeField] private KeyCode dashRightKey = KeyCode.E;
    [SerializeField] private KeyCode moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode crouchKey = KeyCode.S;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private bool isJumping = false;
    private bool hasDoubleJumped = false;
    private bool isDashing = false;
    private bool IsRotating = false;

    private int groundLayerMask;
    private int rotation;

    private float actualJumpMaxHeight = Mathf.Infinity;
    private float horizontalVelocity = 0f;

    private Rigidbody rb;
    private Vector3 targetPosition;
    // Public Properties //
    public int Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float JumpHeight
    {
        get { return jumpHeight; }
        set { jumpHeight = value; }
    }

    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    public float DashDistance
    {
        get { return dashDistance; }
        set { dashDistance = value; }
    }

    public float DashCooldown
    {
        get { return dashCooldown; }
        set { dashCooldown = value; }
    }

    public MovementPhase ActualMovementPhase { get; set; }
    // Private Properties //

    // Public Data Structures //
    public enum MovementPhase
    {
        Idle,
        Running,
        Jumping
    }
    #endregion

    #region Unity Methods
    void Awake() {
        rb = GetComponent<Rigidbody>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }
    void Start () {
        targetPosition = transform.position;
        Rotation = -1;
        ActualMovementPhase = MovementPhase.Idle;
    }

    void Update () {
        if (Input.GetKeyDown(jumpKey) && !hasDoubleJumped)
        {
            if (isJumping)
            {
                rb.velocity = new Vector3(0, 0, 0);
                hasDoubleJumped = true;
            }
            else
            {
                isJumping = true;
            }

            if(!PlayerAnimationController.GetBool("IsAttacking"))
                PlayerAnimationController.CrossfadeAnimation("Jump", 0.15f);

            PlayerAnimationController.SetBool("IsJumping", true);
            actualJumpMaxHeight = transform.position.y + jumpHeight;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            InvokeRepeating("ResetJump", 1f, 0.1f);
        }

        if (Input.GetKeyDown(dashLeftKey) && !isDashing)
            StartCoroutine(Dash(-1));
        if (Input.GetKeyDown(dashRightKey) && !isDashing)
            StartCoroutine(Dash(1));

        if ((Input.GetKeyUp(moveRightKey) && !Input.GetKey(moveLeftKey)) || (Input.GetKeyUp(moveLeftKey) && !Input.GetKey(moveRightKey)))
        {
            PlayerAnimationController.SetBool("IsRunning", false);
            if(!PlayerAnimationController.GetBool("IsAttacking") && !PlayerAnimationController.GetBool("IsJumping"))
                PlayerAnimationController.Animator.Play("Idle");
        }

        RaycastHit groundHit;
        if (isJumping && rb.velocity.y <= 0 && Physics.Raycast(transform.position, Vector3.down, out groundHit, groundLayerMask) && Vector3.Distance(transform.position, groundHit.point) <= 1f)
        {
            PlayerAnimationController.SetBool("IsJumping", false);
            if(!PlayerAnimationController.GetBool("IsAttacking"))
                PlayerAnimationController.CrossfadeAnimation("Jump_land", 0.15f);
        }
                
        if (rb.velocity == Vector3.zero)
        {
            ActualMovementPhase = MovementPhase.Idle;
        }
        else if (rb.velocity.y != 0)
        {
            ActualMovementPhase = MovementPhase.Jumping;
        }
        else if (rb.velocity.x != 0)
        {
            ActualMovementPhase = MovementPhase.Running;
        }
    }

    void FixedUpdate() {

        if (Input.GetKey(moveLeftKey) || Input.GetKey(moveRightKey))
        {
            horizontalVelocity += MovementSpeed * pickingUpSpeedPercentage;
            if (horizontalVelocity > MovementSpeed)
                horizontalVelocity = MovementSpeed;
        }
        else
        {
            horizontalVelocity -= MovementSpeed * pickingUpSpeedPercentage;
            if (horizontalVelocity < 0)
                horizontalVelocity = 0;
        }

        if (Input.GetKey(moveLeftKey))
        {
            PlayerAnimationController.SetBool("IsRunning", true);
            if (Rotation != -1)
            {
                Rotation = -1;
                transform.Rotate(0, 180, 0);
                if (!PlayerAnimationController.GetBool("IsAttacking"))
                    PlayerAnimationController.PlayAnimation("Turn");

                horizontalVelocity = MovementSpeed * pickingUpSpeedPercentage;
            }
        }
        else if (Input.GetKey(moveRightKey))
        {
            PlayerAnimationController.SetBool("IsRunning", true);
            if (Rotation != 1)
            {
                Rotation = 1;
                transform.Rotate(0, 180, 0);
                if (!PlayerAnimationController.GetBool("IsAttacking"))
                    PlayerAnimationController.PlayAnimation("Turn");

                horizontalVelocity = MovementSpeed * pickingUpSpeedPercentage;
            }
        }

        if (Input.GetKey(moveLeftKey) || Input.GetKey(moveRightKey))
        {
            if (PlayerAnimationController.GetBool("IsRunning") && !PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && !PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Turn") && !PlayerAnimationController.GetBool("IsAttacking") && !PlayerAnimationController.GetBool("IsJumping"))
                if(horizontalVelocity < 150)
                    PlayerAnimationController.CrossfadeAnimation("Walk", 0.01f);
                else
                    PlayerAnimationController.CrossfadeAnimation("Run", 0.01f);

            Vector3 vel = rb.velocity;
            rb.MovePosition(Vector3.SmoothDamp(transform.position, transform.position + Vector3.right * horizontalVelocity * Rotation, ref vel, 1f));
        }

        PlayerAnimationController.SetFloat("Velocity", horizontalVelocity);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    void ResetJump() {
        Vector3 originPoint = transform.position + Vector3.up * transform.localScale.y * 0.5f;
        if (Physics.Raycast(originPoint, Vector3.down, transform.localScale.y * 0.52f, groundLayerMask) 
            || Physics.Raycast(originPoint + Vector3.right * transform.localScale.x, Vector3.down, transform.localScale.y * 0.52f, groundLayerMask) 
            || Physics.Raycast(originPoint - Vector3.right * transform.localScale.x, Vector3.down, transform.localScale.y * 0.52f, groundLayerMask))
        {
            CancelInvoke("ResetJump");
            isJumping = hasDoubleJumped = false;
        }
    }

    IEnumerator Dash(int isRight)
    {
        isDashing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        for (int i = 0; i < 16; i++)
        {
            transform.position += new Vector3(dashDistance / 15 * isRight, 0, 0);
            yield return new WaitForFixedUpdate();
        }

        rb.useGravity = true;

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
    #endregion
}

