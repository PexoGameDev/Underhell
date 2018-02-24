using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModule : MonoBehaviour {
    #region Variables
    // Fields //
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
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

