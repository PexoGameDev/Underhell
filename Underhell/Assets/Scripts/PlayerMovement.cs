using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    #region Variables
    // Fields //
    private Rigidbody rb;

    [SerializeField] private float maxHorizontalVelocity = 10f;
    [SerializeField] private float maxVerticalVelocity = 10f;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float multiplier = 50f;

    [SerializeField] private KeyCode moveLeftKey;
    [SerializeField] private KeyCode moveRightKey;
    [SerializeField] private KeyCode crouchKey;
    [SerializeField] private KeyCode jumpKey;

    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        rb = GetComponent<Rigidbody>();
        movementSpeed *= multiplier;
        jumpForce *= multiplier;

    }

    void Update () {

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKey(moveLeftKey))
            rb.AddForce(Vector3.left * movementSpeed, ForceMode.Force);
        else if (Input.GetKey(moveRightKey))
            rb.AddForce(Vector3.right * movementSpeed, ForceMode.Force);
        else
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (Input.GetKeyDown(jumpKey))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

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
    #endregion
}

