using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour {
    #region Variables
    // Fields //

    [SerializeField] private SkinnedMeshRenderer weapon;
    [SerializeField] private GameObject particleOrigin;
    [SerializeField] private GameObject meleeAttackParticles;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private MeleeWeapon Sword;
    [SerializeField] private Transform projectileOrigin;

    public float hitComboResetDelay = 0.4f;
    public float ThirdAttackDamageMultiplier = 1.5f;

    //MELEE WEAPON
    public int MeleeAttackDamage = 10;
    public float MeleeKnockBackForce = 1f;
    public float meleeAttackSpeedMultiplier = 1f;

    //RANGE WEAPON
    public int RangeDamage = 5;
    public float RangeKnockBackForce = 5f;
    public float ProjectileSpeed = 1f;
    public float ProjectileCooldown = 0.5f;
    public float rangeAttackSpeedMultiplier = 1f;

    private bool isAttacking = false;
    private int hitCombo = 0;

    private BoxCollider swordColider;
    private Mesh defaultMesh;
    private PlayerMovement playerMovement;
    // Public Properties //
    public List<AttackEffect> MeleeAttackEffects { get; set; }
    public List<AttackEffect> DistanceAttackEffects { get; set; }
    public int HitCombo
    {
        get { return hitCombo; }
        private set { hitCombo = value; }
    }

    public float RangeAttackSpeedMultipier
    {
        get { return rangeAttackSpeedMultiplier; }
        set { rangeAttackSpeedMultiplier = value; PlayerAnimationController.SetFloat("ShootSpeed", rangeAttackSpeedMultiplier); }
    }

    public float MeleeAttackSpeedMultiplier
    {
        get { return meleeAttackSpeedMultiplier; }
        set { meleeAttackSpeedMultiplier = value; PlayerAnimationController.SetFloat("AttackSpeed", meleeAttackSpeedMultiplier); }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        defaultMesh = weapon.sharedMesh;
        swordColider = Sword.GetComponent<BoxCollider>();
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
        swordColider.enabled = true;
        CancelInvoke("ResetHitCombo");

        switch(hitCombo)
        {
            case 0:
            default:
                PlayerAnimationController.PlayAnimation("Attack_1");
                GameObject particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_1"].length * 0.5f / PlayerAnimationController.GetFloat("AttackSpeed"));
                swordColider.enabled = false;
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_1"].length * 0.5f / PlayerAnimationController.GetFloat("AttackSpeed"));
                Destroy(particles);
                break;

            case 1:
                PlayerAnimationController.PlayAnimation("Attack_2");
                particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_2"].length * 0.5f / PlayerAnimationController.GetFloat("AttackSpeed"));
                swordColider.enabled = false;
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_2"].length * 0.5f / PlayerAnimationController.GetFloat("AttackSpeed"));
                Destroy(particles);
                break;

            case 2:
                PlayerAnimationController.PlayAnimation("Attack_3");
                particles = Instantiate(meleeAttackParticles, particleOrigin.transform);
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_3"].length * 0.5f / PlayerAnimationController.GetFloat("AttackSpeed"));
                swordColider.enabled = false;
                yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Attack_3"].length * 0.5f / PlayerAnimationController.GetFloat("AttackSpeed"));
                Destroy(particles);
                break;
        }

        PlayerAnimationController.SetBool("IsAttacking", false);
        isAttacking = false;
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
        PlayerAnimationController.PlayAnimation("Shoot");

        yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Shoot"].length * 0.6f / PlayerAnimationController.GetFloat("ShootSpeed"));

        if (ProjectileSpeed < 0 && playerMovement.Rotation > 0)
            ProjectileSpeed *= -1;
        else if (ProjectileSpeed > 0 && playerMovement.Rotation < 0)
            ProjectileSpeed *= -1;

        Projectile projectile = Instantiate(projectilePrefab, projectileOrigin.position, Quaternion.identity);
        projectile.gameObject.SetActive(true);

        yield return new WaitForSeconds(PlayerAnimationController.AnimationClips["Shoot"].length * 0.4f / PlayerAnimationController.GetFloat("ShootSpeed"));

        weapon.sharedMesh = defaultMesh;

        hitCombo = (hitCombo + 1) % 3;
        if (hitCombo != 0)
            Invoke("ResetHitCombo", hitComboResetDelay);

        PlayerAnimationController.SetBool("IsAttacking", false);

        yield return new WaitForSeconds(ProjectileCooldown);
        isAttacking = false;
    }
    void ResetHitCombo()
    {
        hitCombo = 0;
    }
    #endregion
}

