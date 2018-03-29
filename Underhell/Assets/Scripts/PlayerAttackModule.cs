using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private float hitComboResetDelay = 0.4f;
    [SerializeField] private SkinnedMeshRenderer weapon;
    [SerializeField] private GameObject particleOrigin;
    [SerializeField] private GameObject meleeAttackParticles;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private MeleeWeapon Sword;
    [SerializeField] private Transform barellTransform;

    private bool isAttacking = false;
    private int hitCombo = 0;

    private Mesh defaultMesh;
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
        defaultMesh = weapon.sharedMesh;
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
            StartCoroutine(Shoot());
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    IEnumerator Attack()
    {
        PlayerAnimationController.SetBool("IsAttacking", true);
        isAttacking = true;
        Sword.GetComponent<BoxCollider>().enabled = true;
        CancelInvoke("ResetHitCombo");

        switch(hitCombo)
        {
            case 0:
            default:
                PlayerAnimationController.PlayAnimation("Attack_1", 1);
                GameObject particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_1"].length);
                Destroy(particles);
                break;

            case 1:
                PlayerAnimationController.PlayAnimation("Attack_2", 1);
                particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_2"].length);
                Destroy(particles);
                break;

            case 2:
                PlayerAnimationController.PlayAnimation("Attack_3", 1);
                particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_3"].length);
                Destroy(particles);
                break;
        }

        //yield return StartCoroutine(WeaponController.MeleeWeapon.Attack(playerMovement.Rotation,transform, hitCombo, playerMovement.ActualMovementPhase, MeleeAttackEffects));
        Sword.GetComponent<BoxCollider>().enabled = false;
        isAttacking = false;
        PlayerAnimationController.SetBool("IsAttacking", false);
        hitCombo = (hitCombo + 1) % 3;
        if(hitCombo!=0)
            Invoke("ResetHitCombo", hitComboResetDelay);
    }
    IEnumerator Shoot()
    {
        CancelInvoke("ResetHitCombo");
        isAttacking = true;
        PlayerAnimationController.SetBool("IsAttacking", true);

        weapon.sharedMesh = null;
        PlayerAnimationController.PlayAnimation("Shoot", 1);

        yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Shoot"].length * 0.6f);

        Projectile projectile = Instantiate(projectilePrefab, barellTransform.position, Quaternion.identity);
        projectile.gameObject.SetActive(true);
        projectile.ProjectileSpeed *= playerMovement.Rotation;

        yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Shoot"].length * 0.4f);

        weapon.sharedMesh = defaultMesh;

        hitCombo = (hitCombo + 1) % 3;
        if (hitCombo != 0)
            Invoke("ResetHitCombo", hitComboResetDelay);

        PlayerAnimationController.SetBool("IsAttacking", false);

        yield return new WaitForSeconds(projectile.Cooldown);
        isAttacking = false;
    }
    void ResetHitCombo()
    {
        hitCombo = 0;
    }
    #endregion
}

