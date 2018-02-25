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
    [SerializeField] private float maxDashCooldown = 2f;

    private bool isJumping = false;
    private bool hasDoubleJumped = false;
    private bool isDashing = false;
    private bool isDashOnCooldown = false;

    private int groundLayerMask;
    private int rotation;

    private float actualJumpMaxHeight = Mathf.Infinity;

    private Rigidbody rb;
    private GameObject lastPlatform;
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

    public float MaxDashCooldown
    {
        get { return maxDashCooldown; }
        set { maxDashCooldown = value; }
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
        lastPlatform = gameObject;
        Rotation = (Random.Range(0,2) >= 1) ? -1 : 1;
	}
	
	void Update () {
        /* IQ Debugging
         Debug.DrawRay(transform.position - Vector3.up * transform.localScale.y * 0.75f, Vector3.right * Rotation, Color.red);
         Debug.DrawRay(transform.position + Vector3.up * JumpHeight, Vector3.right * Rotation,Color.green);
         Debug.DrawRay(transform.position + Vector3.up * JumpHeight / 2, Vector3.right * Rotation, Color.yellow);
         Debug.DrawRay(transform.position + Vector3.up * JumpHeight / 4, Vector3.right * Rotation, Color.magenta);

         Debug.DrawRay(transform.position + Vector3.right * transform.localScale.x * Rotation, Vector3.down, Color.blue);

         Debug.DrawRay(transform.position + Vector3.right * Rotation * DashDistance, Vector3.down, Color.red);
         Debug.DrawRay(transform.position, Vector3.right, Color.green);
         */
        Debug.DrawRay(transform.position, new Vector3(DashDistance * Rotation, JumpHeight, 0), Color.red);

    }

    void FixedUpdate()
    {
        switch(MovementIQ)
        {
            case 0: break;

            case 1: DoStep();  break; //Walks even to death

            case 2:
                if(!isDashing)
                    if (CheckGround()) //First makes sure it won't fall off the stage
                        DoStep();
                    else
                        Rotation *= -1;
                break;

            case 3:
                if (CheckJumpableWall() && !isJumping)
                    Jump();
                else
                    goto case 2;
                break;

            case 4:
                if (!isDashing && !isDashOnCooldown && !CheckGround() && CheckGap())
                    StartCoroutine(Dash());
                else
                    goto case 3;
                break;

            case 5:
                if (!isDashing && !isDashOnCooldown && CheckJumpablePlatformAfterGap())
                    StartCoroutine(ReachPlatformAbove());
                else
                    goto case 4;
                break;
        }

        if (actualJumpMaxHeight < transform.position.y && !isDashing)
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(Vector3.down * jumpForce / 2, ForceMode.Impulse);
        }

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
        return (
            Physics.Raycast(transform.position - Vector3.up * transform.localScale.y * 0.75f, Vector3.right * Rotation, transform.localScale.x, groundLayerMask) 
            && !Physics.Raycast(transform.position + Vector3.up*JumpHeight,Vector3.right*Rotation,transform.localScale.x,groundLayerMask)
            );
    }

    private bool CheckGap()
    {
        for (int i = 1; (DashDistance/i)>transform.localScale.x ; i++)
        if(Physics.Raycast(transform.position + Vector3.right*Rotation*DashDistance/i,Vector3.down,transform.localScale.y * 10, groundLayerMask)
            && !Physics.Raycast(transform.position, Vector3.right*Rotation, DashDistance/i))
            return true;
        return false;
    }

    private bool CheckJumpablePlatformAfterGap()
    {
        RaycastHit hit;
        bool result = Physics.Raycast(transform.position, new Vector3(DashDistance * Rotation, JumpHeight, 0), out hit, Mathf.Sqrt(DashDistance * DashDistance + JumpHeight * JumpHeight), groundLayerMask);
        if (result && hit.collider.gameObject != lastPlatform)
        {
            lastPlatform = hit.collider.gameObject;
            Invoke("ResetLastPlatform", Random.Range(1f, 2f));
            return true;
        }

        return false;

        /*
        bool hasFoundPlatform = false;
        bool isSpaceOverPlatform = false;
        int iterator = 1;
        for (; iterator < 26; iterator++)
            if (Physics.Raycast(transform.position + Vector3.up * (JumpHeight/25) * iterator, Vector3.right * Rotation, DashDistance - transform.localScale.x, groundLayerMask))
            {
                iterator++;
                hasFoundPlatform = true;
                break;
            }

        if(hasFoundPlatform)
            for (; iterator < 26; iterator++)
                if(!Physics.Raycast(transform.position + Vector3.up * (JumpHeight / 25) * iterator, Vector3.right * Rotation, DashDistance - transform.localScale.x, groundLayerMask))
                {
                    isSpaceOverPlatform = true;
                    break;
                }

        bool inRange = (hasFoundPlatform&&isSpaceOverPlatform);

        bool obstacles = !Physics.Raycast(transform.position + Vector3.up*(JumpHeight/25)*iterator, Vector3.right*Rotation,DashDistance - transform.localScale.x);

        return (inRange && obstacles);*/
    }

    private void Jump()
    {
        int JumpStep = 2;
        for (; JumpStep < 20; JumpStep++)
            if (Physics.Raycast(transform.position + Vector3.up * JumpHeight / JumpStep, Vector3.right * Rotation, transform.localScale.x, groundLayerMask))
            {
                JumpStep--;
                break;
            }

        rb.AddForce(Vector3.up * JumpForce / JumpStep, ForceMode.Impulse);
        actualJumpMaxHeight = transform.position.y + JumpHeight;
        isJumping = true;
        InvokeRepeating("ResetJump", 0.1f, 0.1f);
    }

    private void DoStep()
    {
        transform.position += Vector3.right * Rotation * MovementSpeed;
    }

    private void ResetJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, transform.localScale.y, groundLayerMask))
        {
            isJumping = false;
            CancelInvoke("ResetJump");
        }
    }

    private void ResetLastPlatform()
    {
        lastPlatform = gameObject;
    }

    private IEnumerator Dash()
    {
        int DashStep = 2;
        for (; DashStep < 6; DashStep++)
            if (!Physics.Raycast(transform.position + Vector3.right * Rotation * DashDistance / DashStep, Vector3.down, transform.localScale.y * 10, groundLayerMask))
            {
                DashStep--;
                break;
            }

        isDashing = true;
        isDashOnCooldown = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        for (int i = 0; i < 31; i++)
        {
            transform.Translate(DashDistance*Rotation/(30*DashStep), 0.05f, 0);
            yield return new WaitForSeconds(0.01f);
        }

        rb.velocity = Vector3.zero;
        rb.useGravity = true;

        isDashing = false;
        yield return new WaitForSeconds(Random.Range(0,maxDashCooldown));
        isDashOnCooldown = false;
    }

    private IEnumerator ReachPlatformAbove()
    {
        float jumpMult = JumpHeight / (lastPlatform.transform.position.y - transform.position.y);
        float dashMult = DashDistance / (lastPlatform.transform.position.x - transform.position.x);

        isJumping = true;
        isDashing = true;
        InvokeRepeating("ResetJump", 0.1f, 0.1f);

        rb.AddForce(new Vector3(0.5f * Rotation, jumpMult * JumpForce, 0f), ForceMode.Impulse);
        rb.useGravity = false;

        //Dash

        rb.useGravity = true;

        yield return null;
    }
    #endregion
}

