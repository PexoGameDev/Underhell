using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private float hitComboResetDelay = 0.4f;
    [SerializeField] private GameObject particleOrigin;
    [SerializeField] private GameObject meleeAttackParticles;

    private bool isAttacking = false;
    private int hitCombo = 0;

    private PlayerMovement playerMovement;
    // Public Properties //
    public List<AttackEffect> MeleeAttackEffects { get; set; }
    public List<AttackEffect> DistanceAttackEffects { get; set; }

    // Private Properties //
    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start () {
        
	}
	
	void Update () {
		if(Input.GetMouseButton(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        if (Input.GetMouseButton(1) &&  !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Shoot());
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    IEnumerator Attack()
    {
        isAttacking = true;

        CancelInvoke("ResetHitCombo");

        switch(hitCombo)
        {
            case 0:
            default:
                PlayerAnimationController.PlayAnimation("hero_attack_1");
                PlayerAnimationController.PlayAnimation("sword_attack_1");
                GameObject particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                while(!PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("hero_attack_1_return"))
                {
                    yield return new WaitForEndOfFrame();
                }
                Destroy(particles);
                break;

            case 1:
                PlayerAnimationController.PlayAnimation("hero_attack_2");
                PlayerAnimationController.PlayAnimation("sword_attack_2");
                particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                while (!PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("hero_attack_2_return"))
                {
                    yield return new WaitForEndOfFrame();
                }
                Destroy(particles);
                break;

            case 2:
                PlayerAnimationController.PlayAnimation("hero_attack_3");
                PlayerAnimationController.PlayAnimation("sword_attack_3");
                particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                while (!PlayerAnimationController.Animator.GetCurrentAnimatorStateInfo(0).IsName("hero_attack_3_return"))
                {
                    yield return new WaitForEndOfFrame();
                }
                Destroy(particles);
                break;
        }
        
        //yield return StartCoroutine(WeaponController.MeleeWeapon.Attack(playerMovement.Rotation,transform, hitCombo, playerMovement.ActualMovementPhase, MeleeAttackEffects));

        isAttacking = false;
        hitCombo = (hitCombo + 1) % 3;
        if(hitCombo!=0)
            Invoke("ResetHitCombo", hitComboResetDelay);
    }
    IEnumerator Shoot()
    {
        CancelInvoke("ResetHitCombo");
        Projectile projectile = Instantiate(WeaponController.Projectile,transform.position,Quaternion.identity);
        projectile.gameObject.SetActive(true);
        projectile.ProjectileSpeed *= playerMovement.Rotation;

        hitCombo = (hitCombo + 1) % 3;

        if (hitCombo != 0)
            Invoke("ResetHitCombo", hitComboResetDelay);
        yield return new WaitForSeconds(projectile.Cooldown);
        isAttacking = false;
    }
    void ResetHitCombo()
    {
        hitCombo = 0;
    }
    #endregion
}

