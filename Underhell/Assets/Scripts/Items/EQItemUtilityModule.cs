using UnityEngine;
public class EQItemUtilityModule : MonoBehaviour {
    [Tooltip("Bonus to player's movement speed")]
    public float movementSpeed = 0f;
    public float jumpHeight = 0f;
    public float jumpForce = 0f;
    public float dashDistance = 0f;
    public float dashCooldown = 0f;
    [Range(0f, 0.1f)] public float pickingUpSpeedPercentage = 0f;
    [Range(0f, 0.99f)] public float slowPercentageWhenAttacking = 0f;
}


