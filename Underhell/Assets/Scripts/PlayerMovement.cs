using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Variables
    // Fields //
    private Rigidbody rb;

    private bool isJumping = false;
    private bool hasDoubleJumped = false;
    private bool isDashing = false;

    private int groundLayerMask;
    private int rotation;

    private float actualJumpMaxHeight = Mathf.Infinity;

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

    // Public Properties //
    public int Rotation
    {
        get { return rotation; }
        set { rotation = value; }
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
        movementSpeed *= multiplier;
        jumpForce *= multiplier;
        Rotation = 1;
        ActualMovementPhase = MovementPhase.Idle;
    }

    void Update () {
        if (Input.GetKey(moveLeftKey))
        {
            rb.AddForce(Vector3.left * movementSpeed, ForceMode.Force);
            Rotation = -1;
        }
        else if (Input.GetKey(moveRightKey))
        {
            rb.AddForce(Vector3.right * movementSpeed, ForceMode.Force);
            Rotation = 1;
        }
        else
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKeyDown(jumpKey) && !hasDoubleJumped)
        {
            if (isJumping)
            {
                rb.velocity = new Vector3(0, 0, 0);
                hasDoubleJumped = true;
            }
            else
                isJumping = true;

            actualJumpMaxHeight = transform.position.y + jumpHeight;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            InvokeRepeating("ResetJump", 0.1f, 0.1f);
        }

        if (Input.GetKeyDown(dashLeftKey) && !isDashing)
            StartCoroutine(Dash(-1));
        if (Input.GetKeyDown(dashRightKey) && !isDashing)
            StartCoroutine(Dash(1));

        if (rb.velocity == Vector3.zero)
            ActualMovementPhase = MovementPhase.Idle;
        else if (rb.velocity.y != 0)
            ActualMovementPhase = MovementPhase.Jumping;
        else if (rb.velocity.x != 0)
            ActualMovementPhase = MovementPhase.Running;
    }

    void FixedUpdate() {
        if (actualJumpMaxHeight < transform.position.y)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(Vector3.down * jumpForce/2, ForceMode.Impulse);
        }
        else
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (rb.velocity.x >= maxHorizontalVelocity)
            rb.velocity = new Vector3(maxHorizontalVelocity, rb.velocity.y);
        if (rb.velocity.x <= -maxHorizontalVelocity)
            rb.velocity = new Vector3(-maxHorizontalVelocity, rb.velocity.y);

        if (rb.velocity.y >= maxVerticalVelocity)
            rb.velocity = new Vector3(rb.velocity.x, maxVerticalVelocity);
        if (rb.velocity.y <= -maxVerticalVelocity)
            rb.velocity = new Vector3(rb.velocity.x, -maxVerticalVelocity);

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    void ResetJump() {
        if (Physics.Raycast(transform.position, Vector3.down, transform.localScale.y, groundLayerMask))
        {
            isJumping = hasDoubleJumped = false;
            CancelInvoke("ResetJump");
        }
    }

    IEnumerator Dash(int isRight)
    {
        isDashing = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        for (int i = 0; i < 16; i++)
        {
            transform.Translate(dashDistance / 15 * isRight, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        rb.useGravity = true;

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
    #endregion
}

