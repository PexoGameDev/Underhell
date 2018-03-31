using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModule : MonoBehaviour {
    #region Variables
    // Fields //
    [Range(0, 5)] public int MovementIQ = 0;
    [Range(0, 100)] public int Chaoticness = 0;

    [SerializeField] private float movementSpeed = 500f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private float maxDashCooldown = 2f;

    private bool decision = false;
    private bool isJumping = false;
    private bool isJumpingDown = false;
    private bool isDashing = false;
    private bool isChasingPlayer= false;
    private bool isDashOnCooldown = false;

    private int groundLayerMask;
    private int rotation;

    private float actualJumpMaxHeight = Mathf.Infinity;

    private Rigidbody rb;
    private GameObject lastPlatform;
    private GameObject targetPlatform;
    private Vector3 targetPoint;
    private Enemy enemy;

    // Public Properties //
    public bool IsChasingPlayer
    {
        get { return isChasingPlayer; }
        set { isChasingPlayer = value; }
    }
    public int Rotation
    {
        get { if (!isChasingPlayer)
                return rotation;
            else
                return (enemy.Player.transform.position.x > transform.position.x) ? 1 : -1;
        }
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
    private GameObject ActualPlatform
    {
        get {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 100f, groundLayerMask))
                return hit.collider.gameObject;

            return null;
            }
        set { ActualPlatform = value; }
    }

    private Vector3 TargetPoint
    {
        get { return targetPoint; }
        set { targetPoint = value;
              targetPoint.z = 0; }
    }
    #endregion

    #region Unity Methods
    void Awake()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Start () {
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody>();
        lastPlatform = gameObject;
        Rotation = (Random.Range(0,2) >= 1) ? -1 : 1;
        InvokeRepeating("Decide", 0f, 0.1f);
    }
	
	void Update () {
        /* IQ Debugging
        Debug.DrawRay(transform.position + Vector3.right * transform.localScale.x * 0.5f * Rotation, Vector3.down * transform.localScale.y * 0.51f, Color.green); // Checks is floor avaiable
        Debug.DrawRay(transform.position - Vector3.up * transform.localScale.y * 0.5f, Vector3.right * Rotation * transform.localScale.x * 0.75f, Color.yellow); // Checks Wall


        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.1f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.2f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.3f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.4f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.5f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.6f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.7f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.8f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.9f, Vector3.down * JumpHeight, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance, Vector3.down * JumpHeight, Color.red);

        RaycastHit hit;
        Debug.DrawRay(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.4f, Vector3.down, Color.green);
        Physics.Raycast(transform.position + Vector3.up * JumpHeight + Vector3.right * Rotation * DashDistance * 0.4f, Vector3.down, out hit, JumpHeight - 0.05f, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.up * (hit.point.y - transform.position.y), Color.magenta);
        Debug.DrawRay(new Vector3(transform.position.x, hit.point.y * 1.01f, transform.position.z), hit.point - transform.position, Color.green);
         */
    }

    void FixedUpdate()
    {
        switch(MovementIQ)
        {
            case 0: break;

            case 1: //Walks stright left/right even to death, truns back if meets wall
                if (!CheckWall())
                    DoStep();
                else
                    Rotation *= -1;
                break; 

            case 2: // Walks Left/Right, if meets wall or gap
                if (!isDashing && !isJumping)
                    if (CheckGround(0.51f) && !CheckWall())
                        DoStep();
                    else
                        Rotation *= -1;
                break;

            case 3: // Changes his vertical positon, jumps on higher platforms (up to max jump height) or jumps down on lower platforms
                if (decision && !isDashing && !isJumping && CheckJumpableWall())
                {
                    StartCoroutine(Jump());
                }
                else if(decision && !isDashing && !isJumping && CheckGround(JumpHeight * 0.99f))
                {
                    isJumpingDown = true;
                    InvokeRepeating("ResetJump", 0.1f, 0.1f);
                }
                goto case 2;

            case 4:
                print("Case 4, i'll check am i dashing or is dash on CD also i'll check is this over of the ground and is there any ground after my maximum dash range, and i'll decide =" + decision);
                if (decision && !isDashing && !isDashOnCooldown && !CheckGround(0.5f) && CheckGap())
                {
                    print("All of above is true so i'll dash now.");
                    StartCoroutine(Dash());
                }
                else
                    goto case 3;
                break;

            case 5:
               goto case 4;
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Decide()
    {
        if (isChasingPlayer)
        {
            decision = true;
        }
        else
        {
            int rand = Random.Range(0, 100);
            decision = (rand < Chaoticness);
        }
    }
    private bool CheckGround(float height)
    {
        return (Physics.Raycast(transform.position + Vector3.right * transform.localScale.x * 0.5f * Rotation, Vector3.down, transform.localScale.y * height, groundLayerMask));
    }

    private bool CheckWall()
    {
        return (Physics.Raycast(transform.position - Vector3.up * transform.localScale.y * 0.5f, Vector3.right * Rotation, transform.localScale.x * 0.75f, groundLayerMask));
    }

    private bool CheckJumpableWall()
    {   
        RaycastHit hit;
        Vector3 raycastOriginPoint = transform.position + Vector3.up * JumpHeight; // Casts Ray from point directly above body
        Vector3 offset = DashDistance * Vector3.right * Rotation * 0.1f; // Tenth part of max horizontal distance enemy can travel (enemy checks distance in 10% intervals)

        for (int i = 0; i < 11; i++)
        {
            if (Physics.Raycast(raycastOriginPoint + offset * i, Vector3.down, out hit, JumpHeight - 0.05f, groundLayerMask))
            {
                if (!Physics.Raycast(transform.position, Vector3.up * (hit.point.y - transform.position.y), groundLayerMask))
                {
                    if (!Physics.Raycast(new Vector3(transform.position.x, hit.point.y + gameObject.transform.localScale.y * 0.51f, transform.position.z), Vector3.right * Rotation, Vector3.Distance(hit.point, new Vector3(transform.position.x, hit.point.y + gameObject.transform.localScale.y * 0.51f, transform.position.z))))
                    {
                        if (hit.collider.gameObject != ActualPlatform)
                        {
                            targetPlatform = hit.collider.gameObject;
                            TargetPoint = hit.collider.gameObject.transform.position + Vector3.up * hit.collider.gameObject.transform.localScale.y * 0.51f + Vector3.up * transform.localScale.y - Vector3.right * Rotation * hit.collider.gameObject.transform.localScale.x * 0.49f; // Choosing point just on the very edge of platform, closest to entity
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    private bool CheckGap()
    {
        for (int i = 1; i<6 ; i++)
        if(Physics.Raycast(transform.position + Vector3.right*Rotation*DashDistance * i / 5 + Vector3.up*1.75f,Vector3.down,transform.localScale.y * 10, groundLayerMask)
            && !Physics.Raycast(transform.position + Vector3.up*transform.localScale.x*2, Vector3.right*Rotation, DashDistance*i/5))
            return true;
        return false;
    }

    private IEnumerator Jump()
    {
        isJumping = true;
        rb.useGravity = false;

        float deltaX = transform.position.x - TargetPoint.x;
        float deltaY = TargetPoint.y - transform.position.y;
        float Y0 = transform.position.y;
        float a = -1 / deltaY;

        for (int i = 30; i >= 0; i--)
        {
            transform.position = new Vector3(transform.position.x - deltaX/30, transform.position.y + deltaY/465*i, 0);
            yield return new WaitForFixedUpdate();
        }

        rb.useGravity = true;
        isJumping = false;
    }

    private void DoStep()
    {
        Vector3 vel = rb.velocity;
        rb.MovePosition(Vector3.SmoothDamp(transform.position, transform.position + Vector3.right * Rotation * MovementSpeed,ref vel,1f));
    }

    private void ResetJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, transform.localScale.y, groundLayerMask))
        {
            isJumping = false;
            isJumpingDown = false;
            CancelInvoke("ResetJump");
        }
    }

    private void ResetLastPlatform()
    {
        lastPlatform = gameObject;
    }

    private IEnumerator Dash()
    {
        int DashStep = 1;
        for (; DashStep < 11; DashStep++)
            if (Physics.Raycast(transform.position + Vector3.right * Rotation * DashDistance * DashStep / 10, Vector3.down, transform.localScale.y * JumpHeight*1.1f, groundLayerMask))
                break;

        isDashing = true;
        isDashOnCooldown = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        Vector3 targetPoint = transform.position + new Vector3(DashDistance * DashStep/10 * Rotation, 0.03f * 15, 0);

        for (int i = 0; i < 16; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, DashDistance * DashStep / 150);
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = targetPoint;

        rb.velocity = Vector3.zero;
        rb.useGravity = true;

        isDashing = false;
        yield return new WaitForSeconds(Random.Range(0f,maxDashCooldown));
        while (!CheckGround(transform.localScale.y))
            yield return new WaitForEndOfFrame();
        isDashOnCooldown = false;
    }
    #endregion
}

