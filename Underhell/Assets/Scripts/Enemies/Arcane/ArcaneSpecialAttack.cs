using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneSpecialAttack : MonoBehaviour {
    #region Variables
    // Fields //
    public float CCDuration = 0.5f;
    public float ColliderDuration = 0.25f;
    public float CCDelay = 0.5f;
    public int Damage = 0;

    private BoxCollider myCollider;
    private bool isMoving = true;
    private int groundLayerMask;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        myCollider = GetComponent<BoxCollider>();
        groundLayerMask = LayerMask.GetMask("Ground");
        myCollider.enabled = false;
    }

    void FixedUpdate () {
        RaycastHit hit;
        if (isMoving && Physics.Raycast(Player.Entity.transform.position + Vector3.up, Vector3.down, out hit, groundLayerMask))
            transform.position = hit.point;
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<HPModule>() == Player.Entity.GetComponent<HPModule>())
        {
            Player.Entity.GetComponent<PlayerMovement>().ApplyCC(CC.CCEffect.Snare, CCDuration);
            Player.Entity.GetComponent<HPModule>().GetHit(Damage, 0, Vector3.zero);
        }
    }
    #endregion

    #region Public Methods
    public IEnumerator Fire()
    {
        isMoving = false;
        yield return new WaitForSeconds(CCDelay);
        myCollider.enabled = true;
        Destroy(gameObject, ColliderDuration);
    }
    #endregion

    #region Private Methods
    #endregion
}

