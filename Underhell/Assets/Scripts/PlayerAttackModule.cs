using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour {
    #region Variables
    // Fields //
    MeleeWeapon actualWeapon;

    private bool isAttacking = false;

    private PlayerMovement playerMovement;
    // Public Properties //

    // Private Properties //
    private MeleeWeapon[] MeleeWeapons { get; set; }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start () {
        MeleeWeapons = WeaponController.MeleeWeapons;
        actualWeapon = MeleeWeapons[0];
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isAttacking)
            actualWeapon = MeleeWeapons[(Array.IndexOf(MeleeWeapons,actualWeapon)+1)%MeleeWeapons.Length];
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    IEnumerator Attack()
    {
        actualWeapon.gameObject.SetActive(true);
        yield return StartCoroutine(actualWeapon.Attack(playerMovement.Rotation,transform));
        actualWeapon.gameObject.SetActive(false);
        isAttacking = false;
    }
    #endregion
}

