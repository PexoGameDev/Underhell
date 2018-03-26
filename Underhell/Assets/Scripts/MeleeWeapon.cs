using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeWeapon : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int damage = 1;
    [SerializeField] private float cooldown = 0.1f;
    [SerializeField] private float knockBackForce = 1f;
    [SerializeField] private string weaponName = "";

    private Transform originalParent;
    private Quaternion originalRotation;
    private GameObject player;
    // Public Properties //
    public virtual int Damage { get; set; }
    public virtual string WeaponName { get; set; }
    // Private Properties //
    private List<AttackEffect> AttackEffects;
    #endregion
    void Awake()
    {
        originalParent = transform.parent;
        originalRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    #region Unity Methods
    void Start() {
        Damage = damage;
        WeaponName = weaponName;
    }

    void OnTriggerEnter(Collider other)
    {
        print("yo?");
        HPModule target;
        if (target = other.GetComponent<HPModule>())
        {
            target.GetHit(Damage, knockBackForce, player.transform.position);
            //foreach (AttackEffect ae in AttackEffects)
            //    ae.ApplyEffect(target);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

