using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArcaneAttackingModule : AttackingModule
{
    #region Variables
    // Fields //
    [SerializeField] private float ProjectileSpeed = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject ProjectileOrigin;
    [SerializeField] private float turnOffAutoTargetDistance = 2f;

    [SerializeField] private int SpecialAttackDamage = 10;
    [SerializeField] private float SpecialAttackCooldown = 5f;
    [SerializeField] private float SpecialAttackDetectRange = 5f;
    [SerializeField] private float SpecialAttackCastingTime = 0.75f;
    [SerializeField] private float SpecialAttackChaseTime = 0.25f;
    [SerializeField] private float SpecialAttackDelayBeforeCC = 0.25f;
    [SerializeField] private float SpecialAttackColiderDuration = 0.5f;
    [SerializeField] private float SpecialAttackCCDuration = 3f;
    [SerializeField] [Range(0, 1)] private float SpecialAttackChance = 0.33f;
    [SerializeField] private GameObject SpecialAttackProjectile;

    private bool isSpecialAttackOnCooldown = false;
    private int defaultIQ;
    private EnemyAnimationController animationController;

    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    new private void Start()
    {
        base.Start();
        animationController = GetComponent<EnemyAnimationController>();
        defaultIQ = mainModule.MovementModule.MovementIQ;
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void AttackPlayer()
    {
        if (IQ > 2 && !isSpecialAttackOnCooldown && SeePlayer(SpecialAttackDetectRange) && Random.Range(0, 1) < SpecialAttackChance)
            StartCoroutine("SpecialAttack");
        else
            StartCoroutine("AnimateAttack");
    }

    private IEnumerator AnimateAttack()
    {
        Ray ray = new Ray(transform.position + transform.localScale.y * 0.5f * Vector3.up, Player.transform.position - transform.position + Vector3.up * Player.transform.localScale.y);

        if (Physics.Raycast(ray, AttackRange, playerLayer))
        {
            if (!Physics.Raycast(ray,Vector3.Distance(transform.position,Player.transform.position), groundLayer))
            {

                CancelInvoke("AttackPlayer");
                mainModule.MovementModule.MovementIQ = 0;
                animationController.SetBool("IsRunning", false);
                animationController.CrossfadeAnimation("Attack", 0.2f);
                animationController.SetBool("IsAttacking",true);

                yield return new WaitForSeconds(animationController.AnimationClips["Arcane_attack"].length * 0.5f);
                ArcaneProjectile aProjectile = Instantiate(projectilePrefab, ProjectileOrigin.transform.position, Quaternion.identity).GetComponent<ArcaneProjectile>();
                aProjectile.transform.position = new Vector3(aProjectile.transform.position.x, aProjectile.transform.position.y, 0f);
                aProjectile.Damage = Damage;
                aProjectile.KnockBackForce = KnockBackForce;
                aProjectile.ProjectileSpeed = ProjectileSpeed;
                aProjectile.TurnOffAutoTargetDistance = turnOffAutoTargetDistance;
                if (IQ >= 2)
                    aProjectile.IsAutoTargeted = true;
                else
                    aProjectile.Direction = (Player.transform.position + Vector3.up - transform.position).normalized;

                yield return new WaitForSeconds(animationController.AnimationClips["Arcane_attack"].length * 0.5f);
                animationController.SetBool("IsAttacking", false);
                mainModule.MovementModule.MovementIQ = defaultIQ;
                yield return new WaitForSeconds(Cooldown);
                InvokeRepeating("AttackPlayer", 0, 1f);
            }
        }
    }

    private IEnumerator SpecialAttack()
    {
        CancelInvoke("AttackPlayer");

        mainModule.MovementModule.MovementIQ = 0;

        yield return new WaitForSeconds(SpecialAttackCastingTime);

        ArcaneSpecialAttack attack = Instantiate(SpecialAttackProjectile, transform.position, Quaternion.identity).GetComponent<ArcaneSpecialAttack>();
        attack.CCDelay = SpecialAttackDelayBeforeCC;
        attack.CCDuration = SpecialAttackCCDuration;
        attack.ColliderDuration = SpecialAttackColiderDuration;
        attack.Damage = SpecialAttackDamage;

        yield return new WaitForSeconds(SpecialAttackChaseTime);

        StartCoroutine(attack.Fire());

        mainModule.MovementModule.MovementIQ = defaultIQ;

        InvokeRepeating("AttackPlayer", 0, 1f);

        isSpecialAttackOnCooldown = true;
        yield return new WaitForSeconds(SpecialAttackCooldown);
        isSpecialAttackOnCooldown = false;
    }
    #endregion
}

