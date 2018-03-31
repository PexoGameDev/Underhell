using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneProjectile : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private GameObject hitParticles;

    public int Damage = 0;
    public bool IsAutoTargeted = false;
    public float ProjectileSpeed = 1f;
    public float KnockBackForce = 0f;
    public Vector3 Direction;

    private HPModule player;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HPModule>();
        Destroy(gameObject, 3f);
	}
	
	void FixedUpdate () {
        if (IsAutoTargeted)
            transform.position = Vector3.MoveTowards(transform.position, player.gameObject.transform.position, ProjectileSpeed);
        else
            transform.position += Direction * ProjectileSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HPModule>() == player)
            player.GetHit(Damage, KnockBackForce, transform.position);
        Instantiate(hitParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

