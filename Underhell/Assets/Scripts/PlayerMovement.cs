using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] [Range(0f, 0.1f)] private float pickingUpSpeedPercentage = 0.25f;
    [SerializeField] [Range(0f, 0.99f)] private float slowPercentageWhenAttacking = 0.6f;
    [SerializeField] private float movementSpeed = 250f;
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

    private bool hasDoubleJumped = false;
    private bool hasDashedDown = false;
    private bool isJumping = false;
    private bool isDashing = false;
    private bool isReadyToLand = false;

    private int groundLayerMask;
    private int rotation;

    private float horizontalVelocity = 0f;

    private Rigidbody rb;
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
        Rotation = -1;
        ActualMovementPhase = MovementPhase.Idle;
    }

    void Update () {
        if (Input.GetKeyDown(jumpKey) && !hasDoubleJumped)
        {
            if (isJumping)
            {
                rb.velocity = Vector3.zero;
                hasDoubleJumped = true;
            }
            else
            {
                isJumping = true;
            }

            if(!PlayerAnimationController.GetBool("IsAttacking"))
                PlayerAnimationController.CrossfadeAnimation("Jump", 0.15f);

            PlayerAnimationController.SetBool("IsJumping", true);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            InvokeRepeating("ResetJump", 1f, 0.1f);
            StartCoroutine("ReadyToLand");
        }

        if (Input.GetKeyDown(crouchKey) && !hasDashedDown)
        {
            rb.velocity = Vector3.zero;
            hasDashedDown = true;

            if (!PlayerAnimationController.GetBool("IsAttacking"))
                PlayerAnimationController.CrossfadeAnimation("Jump", 0.15f);

            PlayerAnimationController.SetBool("IsJumping", true);
            rb.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
            InvokeRepeating("ResetDashDown", 1f, 0.1f);
            isReadyToLand = true;
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
        Debug.DrawRay(transform.position + Vector3.up*transform.localScale.y, Vector3.down * 100f, Color.red);
        if(Physics.Raycast(transform.position + Vector3.up * transform.localScale.y, Vector3.down, out groundHit, groundLayerMask))
        {
            if (isReadyToLand && Vector3.Distance(transform.position, groundHit.point) <= 1f)
            {
                if(!PlayerAnimationController.GetBool("IsAttacking"))
                    PlayerAnimationController.CrossfadeAnimation("Jump_land", 0.15f);
                PlayerAnimationController.SetBool("IsJumping", false);
                isReadyToLand = false;
            }

            if (Vector3.Distance(transform.position,groundHit.point)>= 2f)
            {
                if (!PlayerAnimationController.GetBool("IsAttacking"))
                    PlayerAnimationController.CrossfadeAnimation("Jump_air", 0.1f);
                PlayerAnimationController.SetBool("IsJumping", true);
                isReadyToLand = true;
            }
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
            }
        }
        else if (Input.GetKey(moveRightKey))
        {
            if (Rotation != 1)
            {
                Rotation = 1;
                transform.Rotate(0, 180, 0);
                if (!PlayerAnimationController.GetBool("IsAttacking"))
                    PlayerAnimationController.PlayAnimation("Turn");
            }
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
            PlayerAnimationController.SetBool("IsRunning", true);

            horizontalVelocity += MovementSpeed * pickingUpSpeedPercentage;
            if (horizontalVelocity > MovementSpeed)
                horizontalVelocity = MovementSpeed;

            if (PlayerAnimationController.GetBool("IsRunning") && !PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
                !PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Turn") && !PlayerAnimationController.GetBool("IsAttacking") &&
                !PlayerAnimationController.GetBool("IsJumping"))
                PlayerAnimationController.CrossfadeAnimation("Walk-Run", 0.01f);

            if (PlayerAnimationController.GetBool("IsAttacking"))
                horizontalVelocity *= slowPercentageWhenAttacking;

            Vector3 vel = rb.velocity;
            rb.MovePosition(Vector3.SmoothDamp(transform.position, transform.position + Vector3.right * horizontalVelocity * Rotation, ref vel, 1f));
        }
        else
            horizontalVelocity = 0;


        PlayerAnimationController.SetFloat("Velocity", horizontalVelocity / 150f);
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

    void ResetDashDown()
    {
        Vector3 originPoint = transform.position + Vector3.up * transform.localScale.y * 0.5f;
        if (Physics.Raycast(originPoint, Vector3.down, transform.localScale.y * 0.52f, groundLayerMask)
            || Physics.Raycast(originPoint + Vector3.right * transform.localScale.x, Vector3.down, transform.localScale.y * 0.52f, groundLayerMask)
            || Physics.Raycast(originPoint - Vector3.right * transform.localScale.x, Vector3.down, transform.localScale.y * 0.52f, groundLayerMask))
        {
            CancelInvoke("ResetDashDown");
            hasDashedDown = false;
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

    IEnumerator ReadyToLand()
    {
        yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Jump"].length * 0.5f);
        isReadyToLand = true;
    }
    #endregion
}

