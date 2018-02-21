﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeWeapon : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int damage = 1;

    [SerializeField] private float cooldown = 0.1f;

    [SerializeField] private string weaponName = "";

    private Transform originalParent;
    private Quaternion originalRotation;
    // Public Properties //
    public virtual int Damage { get; set; }
    public virtual string WeaponName { get; set; }
    // Private Properties //
    #endregion
    void Awake()
    {
        originalParent = transform.parent;
        originalRotation = transform.rotation;
    }
    #region Unity Methods
    void Start() {
        Damage = damage;
        WeaponName = weaponName;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HPModule>())
            other.GetComponent<HPModule>().GetHit(Damage);
    }
    #endregion

    #region Public Methods
    public virtual IEnumerator Attack(int isLookingRight, Transform parent, int hitComboStep, PlayerMovement.MovementPhase movementPhase)
    {
        // ADD ATTACK VARIANTS DEPENDING ON MOVEMENTPHASE AND HITCOMBOSTEP //

        transform.parent = parent;
        gameObject.transform.localPosition = Vector3.zero;

        for (int i = 0; i < 36; i++)
        {
            gameObject.transform.Rotate(0, 0, 5f * -isLookingRight);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(cooldown);

        transform.parent = originalParent;
        transform.rotation = originalRotation;
    }
    #endregion

    #region Private Methods
    #endregion
}

