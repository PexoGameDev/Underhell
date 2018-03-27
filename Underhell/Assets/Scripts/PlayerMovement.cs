using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private float maxHorizontalVelocity = 35f;
    [SerializeField] private float maxVerticalVelocity = 20f;
    [SerializeField] private float movementSpeed = 500f;
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

    private int groundLayerMask;
    private int rotation;

    private float actualJumpMaxHeight = Mathf.Infinity;

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

            PlayerAnimationController.CrossfadeAnimation("Jump", 0.15f);
            actualJumpMaxHeight = transform.position.y + jumpHeight;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            InvokeRepeating("ResetJump", 1f, 0.1f);
        }

        if (Input.GetKeyDown(dashLeftKey) && !isDashing)
            StartCoroutine(Dash(-1));
        if (Input.GetKeyDown(dashRightKey) && !isDashing)
            StartCoroutine(Dash(1));

        RaycastHit groundHit;
        if (isJumping && rb.velocity.y < 0 && Physics.Raycast(transform.position, Vector3.down, out groundHit, groundLayerMask) && Vector3.Distance(transform.position, groundHit.point) <= 1f)
        {
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
        if (actualJumpMaxHeight < transform.position.y)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(Vector3.down * jumpForce * 0.5f, ForceMode.Impulse);
        }

        if (Input.GetKey(moveLeftKey))
        {
            if (Rotation != -1)
            {
                print("gotta rotate");
                Rotation = -1;
                if (!isJumping)
                {
                    PlayerAnimationController.CrossfadeAnimation("Turn", 0.3f);
                    //PlayerAnimationController.PlayAnimation("Turn");
                }
                StartCoroutine("Turn");
                //Invoke("Turn", 0.4f);
            }

            Vector3 vel = rb.velocity;
            rb.MovePosition(Vector3.SmoothDamp(transform.position, transform.position + Vector3.right * MovementSpeed * Rotation, ref vel, 1f));
        }
        else if (Input.GetKey(moveRightKey))
        {
            if (Rotation != 1)
            {
                print("gotta rotate");
                Rotation = 1;
                if (!isJumping)
                {
                    PlayerAnimationController.CrossfadeAnimation("Turn", 0.3f);
                    //PlayerAnimationController.PlayAnimation("Turn");
                }
                //Invoke("Turn", 0.4f);
                StartCoroutine("Turn");
            }

            Vector3 vel = rb.velocity;
            rb.MovePosition(Vector3.SmoothDamp(transform.position, transform.position + Vector3.right * MovementSpeed * Rotation, ref vel, 1f));
        }
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

    IEnumerator Turn()
    {
        for (int i = 0; i < 11; i++)
        {
            yield return new WaitForEndOfFrame();
        }
       // transform.Rotate(0, -180, 0);
        PlayerAnimationController.Animator.speed = 1;
    }
    #endregion
}

