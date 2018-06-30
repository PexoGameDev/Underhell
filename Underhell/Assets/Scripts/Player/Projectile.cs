using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    #region Variables
    // Fields //
    public float projectileSpeed = 1f;
    public GameObject target;
    public GameObject FinalExplosion;

    private Vector3 direction;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        direction = (target.transform.position - transform.position).normalized;
    }
	
	void FixedUpdate () {
        transform.position += direction * projectileSpeed * Time.fixedDeltaTime;
	}

    void OnTriggerEnter(Collider other)
    {
        HPModule target;
        if (target = other.GetComponent<HPModule>()){}

        Instantiate(FinalExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

