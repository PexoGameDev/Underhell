using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorysAttackingModule : AttackingModule {
    #region Variables
    // Fields //
    [SerializeField] private int SpecialAttackDamage = 10;
    [SerializeField] private float SpecialAttackKnockback = 5f;
    [SerializeField] private float SpecialAttackCooldown = 5f;
    [SerializeField] private float SpecialAttackDetectRange = 5f;
    [SerializeField] private float SpecialAttackDuration = 3f;
    [SerializeField] private float SpecialAttackMovementMultiplier = 1.5f;
    [SerializeField] [Range(0, 1)] private float SpecialAttackChance = 0.33f;

    [SerializeField] private BoxCollider specialAttackCollider;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    new void Start () {
        base.Start();

        specialAttackCollider.enabled = false;

        if(IQ > 2)
            InvokeRepeating("SpecialAttackDecision", 0f, 2.5f);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HPModule>() == Player)
        {
            Player.GetHit(SpecialAttackDamage, SpecialAttackKnockback, transform.position);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void SpecialAttackDecision()
    {
        if (SeePlayer(SpecialAttackDetectRange) && Random.Range(0, 1) < SpecialAttackChance)
            StartCoroutine("SpecialAttack");
    }

    IEnumerator SpecialAttack()
    {
        CancelInvoke("SpecialAttackDecision");
        float baseMovement = mainModule.MovementModule.MovementSpeed;
        yield return new WaitForSeconds(0.5f);
        specialAttackCollider.enabled = true;
        mainModule.MovementModule.MovementSpeed *= SpecialAttackMovementMultiplier;

        transform.localScale += Vector3.one;
        mainModule.MovementModule.IsChasingPlayer = false;
        mainModule.AwarnessModule.chaseMelee = false;

        yield return new WaitForSeconds(SpecialAttackDuration);
        transform.localScale -= Vector3.one;
        specialAttackCollider.enabled = false;
        mainModule.AwarnessModule.chaseMelee = true;
        mainModule.MovementModule.MovementSpeed = baseMovement;

        yield return new WaitForSeconds(SpecialAttackCooldown);
        InvokeRepeating("SpecialAttackDecision", 2.5f, 2.5f);
    }

    #endregion
}

