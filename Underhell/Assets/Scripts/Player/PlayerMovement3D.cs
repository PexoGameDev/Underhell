using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour {
    #region Variables
    // Fields //
    [Range(0f, 0.1f)] public float pickingUpSpeedPercentage = 0.25f;
    [Range(0f, 0.99f)] public float slowPercentageWhenAttacking = 0.6f;
    public float movementSpeed = 250f;
    public float jumpHeight = 5f;
    public float jumpForce = 20f;

    [SerializeField] private KeyCode moveForwardKey = KeyCode.W;
    [SerializeField] private KeyCode moveBackKey = KeyCode.S;
    [SerializeField] private KeyCode moveLeftKey = KeyCode.A;
    [SerializeField] private KeyCode moveRightKey = KeyCode.D;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private bool hasDoubleJumped = false;
    private bool isJumping = false;
    private bool isReadyToLand = false;

    private bool isSnared = false;

    private int groundLayerMask;
    private float rotation;
    private float horizontalVelocity = 0f;

    private Vector3 deltaPosition;

    private Rigidbody rb;
    // Public Properties //
    public float Rotation
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
    #endregion

    #region Unity Methods
    void Awake() {
        rb = GetComponent<Rigidbody>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }
    void Start () {
        Rotation = -1;
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

        if ((Input.GetKeyUp(moveRightKey) && !Input.GetKey(moveLeftKey)) || (Input.GetKeyUp(moveLeftKey) && !Input.GetKey(moveRightKey)))
        {
            PlayerAnimationController.SetBool("IsRunning", false);
            if(!PlayerAnimationController.GetBool("IsAttacking") && !PlayerAnimationController.GetBool("IsJumping"))
                PlayerAnimationController.Animator.Play("Idle");
        }

        RaycastHit groundHit;
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

        deltaPosition = Vector3.zero;

        if (Input.GetKey(moveLeftKey))
        {
            deltaPosition.z = 1;
/*            if (Rotation != -1)
            {
                Rotation = -1;
                transform.Rotate(0, 180, 0);
                if (!PlayerAnimationController.GetBool("IsAttacking"))
                    PlayerAnimationController.PlayAnimation("Turn");
            }*/
        }
        else if (Input.GetKey(moveRightKey))
        {
            deltaPosition.z = -1;
            /*
            if (Rotation != 1)
            {
                Rotation = 1;
                transform.Rotate(0, 180, 0);
                if (!PlayerAnimationController.GetBool("IsAttacking"))
                    PlayerAnimationController.PlayAnimation("Turn");
            }*/
        }

        if(Input.GetKey(moveForwardKey))
        {
            deltaPosition.x = 1;
        }
        else if (Input.GetKey(moveBackKey))
        {
            deltaPosition.x = -1;
        }
    }

    void FixedUpdate() {
        if (deltaPosition != Vector3.zero)
        {
            PlayerAnimationController.SetBool("IsRunning", true);

            horizontalVelocity += MovementSpeed * pickingUpSpeedPercentage;
            if (horizontalVelocity > MovementSpeed)
                horizontalVelocity = MovementSpeed;

            if (PlayerAnimationController.GetBool("IsRunning") &&
                !PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
                !PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("Turn") &&
                !PlayerAnimationController.GetBool("IsAttacking") &&
                !PlayerAnimationController.GetBool("IsJumping"))
                PlayerAnimationController.CrossfadeAnimation("Walk-Run", 0.01f);

            if (PlayerAnimationController.GetBool("IsAttacking"))
                horizontalVelocity *= slowPercentageWhenAttacking;

            transform.LookAt(transform.position + deltaPosition);
            transform.Rotate(Vector3.up * 90f);
            transform.position += (deltaPosition * horizontalVelocity) * Time.fixedDeltaTime / 20;

            //Vector3 vel = rb.velocity;
            //print(deltaPosition * horizontalVelocity);
            //rb.MovePosition(Vector3.SmoothDamp(transform.position, transform.position + deltaPosition * horizontalVelocity, ref vel, 1f));

            //rb.MovePosition(transform.forward * horizontalVelocity);

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

    IEnumerator ReadyToLand()
    {
        yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Jump"].length * 0.5f);
        isReadyToLand = true;
    }

    public IEnumerator ApplyCCEffects(CC.CCEffect effect, float cooldown)
    {
        switch (effect)
        {
            default:
                break;
            case CC.CCEffect.Snare:

                float defJumpForce = JumpForce;
                float defMovementSpeed = MovementSpeed;

                isSnared = true;
                MovementSpeed = 0;
                JumpForce = 0;

                yield return new WaitForSeconds(cooldown);

                MovementSpeed = defMovementSpeed;
                JumpForce = defJumpForce;
                isSnared = false;

                break;
        }
    }

    public void ApplyCC(CC.CCEffect effect, float cooldown)
    {
        StartCoroutine(ApplyCCEffects(effect, cooldown));
    }
    #endregion
}

