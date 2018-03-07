using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    #region Variables

    // Fields //
    [SerializeField] private int damage = 5;
    [SerializeField] private float knockBackForce = 5f;
    [SerializeField] private float projectileSpeed = 1f;
    [SerializeField] private float cooldown = 0.5f;

    private List<AttackEffect> AttackEffects;
    // Public Properties //
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float KnockBackForce
    {
        get { return knockBackForce; }
        set { knockBackForce = value; }
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    public float Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        AttackEffects = new List<AttackEffect>();
        Destroy(gameObject, 1f);
	}
	
	void FixedUpdate () {
        transform.position += Vector3.right * ProjectileSpeed;
	}

    void OnTriggerEnter(Collider other)
    {
        print("xd?");
        HPModule target;
        if (target = other.GetComponent<HPModule>())
        {
            target.GetHit(Damage, knockBackForce, transform.position);
            foreach (AttackEffect ae in AttackEffects)
                ae.ApplyEffect(target);
        }
        Destroy(gameObject);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

