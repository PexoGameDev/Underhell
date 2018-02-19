using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] MeleeWeapon SwordPrefab;
    [SerializeField] MeleeWeapon AxePrefab;

    MeleeWeapon actualWeapon;

    private bool isAttacking = false;

    private PlayerMovement playerMovement;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        actualWeapon = SwordPrefab.GetComponent<MeleeWeapon>();
    }
    void Start () {
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
        if (Input.GetKeyDown(KeyCode.Q))
            actualWeapon = (actualWeapon == AxePrefab) ? SwordPrefab : AxePrefab;
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    IEnumerator Attack()
    {
        yield return StartCoroutine(actualWeapon.Attack(playerMovement.Rotation,transform));
        isAttacking = false;
    }
    #endregion
}

