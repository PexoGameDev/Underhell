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
         */
         Debug.DrawRay(transform.position - Vector3.up * transform.localScale.y * 0.75f, Vector3.right * Rotation, Color.red);
         Debug.DrawRay(transform.position + Vector3.up * JumpHeight, Vector3.right * Rotation,Color.green);
         Debug.DrawRay(transform.position + Vector3.up * JumpHeight / 2, Vector3.right * Rotation, Color.yellow);
         Debug.DrawRay(transform.position + Vector3.up * JumpHeight / 4, Vector3.right * Rotation, Color.magenta);


         Debug.DrawRay(transform.position + Vector3.right * Rotation * DashDistance, Vector3.down, Color.red);
         Debug.DrawRay(transform.position, Vector3.right, Color.green);
         Debug.DrawRay(transform.position, new Vector3(DashDistance * Rotation, JumpHeight, 0), Color.red);
         Debug.DrawRay(transform.position + Vector3.right * Rotation * DashDistance + Vector3.up * 2, Vector3.down*10, Color.red);
    }

    void FixedUpdate()
    {
        switch(MovementIQ)
        {
            case 0: break;

            case 1:
                print("Case 1");
                if (!CheckWall())
                {
                    print("No wall, doing step");
                    DoStep();
                }
                else
                {
                    print("Found wall, rotating");
                    Rotation *= -1;
                }
                break; //Walks even to death

            case 2:
                print("Case 2, checking if am dashing");
                if (!isDashing && !isJumping)
                {
                    print("I'm not dashing, checking am I jumping");
                    if (isJumpingDown)
                    {
                        print("I'm jumping, doing a step not to fall to face");
                        DoStep();
                    }
                    else
                     if (!CheckWall())
                    {
                        print("I'm not jumping, and there is no wall, im checking ground");
                        if (CheckGround(0.5f))
                        {
                            print("Ground avaiable, i'm doing next step");
                            DoStep();
                        }
                        else
                        {
                            print("No ground on this level! I'll check ground much lower and *DECIDE* " + decision + " | whether or not i'll jump donw");
                            if (decision && CheckGround(JumpHeight * 1.1f))
                            {
                                print("I decided to jump lower");
                                isJumpingDown = true;
                                InvokeRepeating("ResetJump", 0.1f, 0.1f);
                            }
                            else
                            {
                                print("I decided NOT to jump lower, turning back now");
                                Rotation *= -1;
                            }
                        }
                    }
                    else
                    {
                        print("There is a wall turning back.");
                        Rotation *= -1;
                    }
                }
                break;

            case 3:
                print("Case 3 - I'll check now: \n a) Am I dashing \n b) Am I jumping \n c) Is there JumpableWall nerby \n d) and decide =" + decision);
                if (decision && !isDashing && !isJumping && CheckJumpableWall())
                {
                    print("I found some wall and i'm capable of jumping so I *decided* to jump");
                    StartCoroutine(Jump());
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
                if (decision && !isDashing && !isDashOnCooldown)
                {
                    StartCoroutine(ReachPlatformAbove());
                }
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
        return (Physics.Raycast(transform.position + Vector3.right * transform.localScale.x/2 * Rotation, Vector3.down, transform.localScale.y * height, groundLayerMask));
    }

    private bool CheckWall()
    {
        return (Physics.Raycast(transform.position - Vector3.down*transform.localScale.y/2, Vector3.right*Rotation, transform.localScale.x * 0.75f, groundLayerMask));
    }

    private bool CheckJumpableWall()
    {
        RaycastHit hit;
        Vector3 raycastOriginPoint = transform.position+Vector3.up*JumpHeight;
        bool foundPlatform = false;
        Vector3 offset = DashDistance * Vector3.right * Rotation / 10;
        for(int i = 0; i<11; i++)
        {
            if (Physics.Raycast(raycastOriginPoint + offset * i, Vector3.down, out hit, JumpHeight - 0.05f, groundLayerMask) && !Physics.Raycast(transform.position + Vector3.up*hit.point.y,hit.point) && !Physics.Raycast(transform.position,Vector3.up,hit.point.y-transform.position.y,groundLayerMask))
                if (hit.collider.gameObject != ActualPlatform)
                {
                    foundPlatform = true;
                    targetPlatform = hit.collider.gameObject;
                    TargetPoint = hit.point + Vector3.up * transform.localScale.y*1.5f - Vector3.right * Rotation * hit.collider.gameObject.transform.localScale.x/2;
                    break;
                }
        }
        return foundPlatform;
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
        print("IM JUMPING NOW!!!!");
        isJumping = true;
        rb.useGravity = false;

        Vector3 direction = TargetPoint - transform.position;
        while (Vector3.Distance(transform.position, TargetPoint) > 0.5f)
        {
            rb.MovePosition(transform.position + direction/30);
            yield return new WaitForFixedUpdate();
        }
        transform.position = TargetPoint;

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

    private IEnumerator ReachPlatformAbove()
    {
        Vector3 targetPoint = new Vector3(lastPlatform.transform.position.x - lastPlatform.transform.localScale.x/2*Rotation,lastPlatform.transform.position.y + lastPlatform.transform.localScale.y,0);

        isJumping = true;
        isDashing = true;

        rb.useGravity = false;
        while(Vector3.Distance(transform.position,targetPoint)>0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, 0.4f);
            yield return new WaitForFixedUpdate();
        }
        transform.position = targetPoint;
        rb.useGravity = true;

        isJumping = false;
        isDashing = false;
    }
    #endregion
}

