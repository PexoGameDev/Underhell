using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballEffect : SpellEffect {
    #region Variables
    // Fields //
    [Header("Fireball")]
    [SerializeField] private int damage;
    [SerializeField] private float blastRadius;
    [SerializeField] private float speed;
    private Vector3 direction;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start()
    {
        direction = (target - transform.position).normalized;
        Destroy(gameObject, 10f);
    }

    void FixedUpdate()
    {
        transform.position += direction * speed;
        //Vector3.MoveTowards(transform.position, transform.position + target, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HPModule>() != null)
        {
            Explode();
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Explode()
    {
        Collider[] colids = Physics.OverlapSphere(transform.position, blastRadius);

        HPModule enemy;
        foreach (Collider col in colids)
        {
            if ((enemy = col.GetComponent<HPModule>()) != null)
                enemy.GetHit(damage, 0, Vector3.zero, AttackEffect.DamageSource.Fire);
        }
        print("KaBoom!");
        Destroy(gameObject);

    }
    #endregion
}

