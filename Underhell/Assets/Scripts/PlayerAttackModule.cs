using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private float hitComboResetDelay = 0.4f;

    [SerializeField] private GameObject BETATESTATTACKEFFECT;

    private bool isAttacking = false;

    private int hitCombo = 0;

    private MeleeWeapon actualWeapon;

    private PlayerMovement playerMovement;
    // Public Properties //
    public List<AttackEffect> AttackEffects { get; set; }
    // Private Properties //
    private MeleeWeapon[] MeleeWeapons { get; set; }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start () {
        AttackEffects = new List<AttackEffect> { BETATESTATTACKEFFECT.GetComponent<BlazeAttack>() };
        MeleeWeapons = WeaponController.MeleeWeapons;
        actualWeapon = MeleeWeapons[0];
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.R) && !isAttacking)
            actualWeapon = MeleeWeapons[(Array.IndexOf(MeleeWeapons,actualWeapon)+1)%MeleeWeapons.Length];
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    IEnumerator Attack()
    {
        CancelInvoke("ResetHitCombo");

        actualWeapon.gameObject.SetActive(true);
        yield return StartCoroutine(actualWeapon.Attack(playerMovement.Rotation,transform, hitCombo, playerMovement.ActualMovementPhase, AttackEffects));
        actualWeapon.gameObject.SetActive(false);
        isAttacking = false;

        hitCombo = (hitCombo + 1) % 3;

        if(hitCombo!=0)
            Invoke("ResetHitCombo", hitComboResetDelay);
    }

    void ResetHitCombo()
    {
        hitCombo = 0;
    }
    #endregion
}

