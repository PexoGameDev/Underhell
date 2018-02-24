using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModule : MonoBehaviour {
    #region Variables
    // Fields //
    [Range(0, 5)] public int MovementIQ;

    [SerializeField] private float maxHorizontalVelocity = 35f;
    [SerializeField] private float maxVerticalVelocity = 20f;
    [SerializeField] private float movementSpeed = 500f;
    [SerializeField] private float multiplier = 1f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float dashCooldown = 2f;

    private bool isJumping = false;
    private bool hasDoubleJumped = false;
    private bool isDashing = false;

    private int groundLayerMask;
    private int rotation;

    private float actualJumpMaxHeight = Mathf.Infinity;

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

    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Start () {
        rb = GetComponent<Rigidbody>();
        Rotation = 1;
	}
	
	void Update () {
        Debug.DrawRay(transform.position - Vector3.up * transform.localScale.y * 0.75f, Vector3.right * Rotation, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight, Vector3.right * Rotation,Color.green);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight / 2, Vector3.right * Rotation, Color.yellow);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight / 4, Vector3.right * Rotation, Color.magenta);

        Debug.DrawRay(transform.position + Vector3.right * transform.localScale.x * Rotation, Vector3.down, Color.blue);
    }

    void FixedUpdate()
    {
        switch(MovementIQ)
        {
            case 0: break;

            case 1:
                if (CheckGround())
                    DoStep();
                else
                    Rotation *= -1;
                break;

            case 2:
                if (CheckJumpableWall() && !isJumping)
                    Jump();
                else
                    goto case 1;
                break;
        }

        if (actualJumpMaxHeight < transform.position.y)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(Vector3.down * jumpForce / 2, ForceMode.Impulse);
        }
        else
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private bool CheckGround()
    {
        return (Physics.Raycast(transform.position + Vector3.right * transform.localScale.x * Rotation, Vector3.down, transform.localScale.y * 10, groundLayerMask));
    }

    private bool CheckJumpableWall()
    {
        return (Physics.Raycast(transform.position - Vector3.up * transform.localScale.y * 0.75f, Vector3.right * Rotation, transform.localScale.x, groundLayerMask) && !Physics.Raycast(transform.position + Vector3.up*JumpHeight,Vector3.right*Rotation,transform.localScale.x,groundLayerMask));
    }

    private void Jump()
    {
        int JumpStep = 1;
        if (!Physics.Raycast(transform.position + Vector3.up * JumpHeight / 2, Vector3.right * Rotation, transform.localScale.x, groundLayerMask))
            JumpStep = 2;
        if (!Physics.Raycast(transform.position + Vector3.up * JumpHeight / 4, Vector3.right * Rotation, transform.localScale.x, groundLayerMask))
            JumpStep = 4;

        rb.AddForce(Vector3.up * JumpForce / JumpStep, ForceMode.Impulse);
        actualJumpMaxHeight = transform.position.y + JumpHeight;
        isJumping = true;
        InvokeRepeating("ResetJump", 0.1f, 0.1f);
    }

    private void DoStep()
    {
        transform.position += Vector3.right * Rotation * MovementSpeed;
    }

    void ResetJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, transform.localScale.y, groundLayerMask))
        {
            isJumping = false;
            CancelInvoke("ResetJump");
        }
    }
    #endregion
}

