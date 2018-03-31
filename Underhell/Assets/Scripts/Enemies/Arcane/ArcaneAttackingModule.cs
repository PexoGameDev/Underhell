using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArcaneAttackingModule : AttackingModule
{
    #region Variables
    // Fields //
    [SerializeField] private float ProjectileSpeed = 1f;
    [SerializeField] private GameObject projectilePrefab;

    private int defaultIQ; 
    private Enemy mainModule;
    private int groundLayer;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    new private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        mainModule = gameObject.GetComponent<Enemy>();
        defaultIQ = mainModule.MovementModule.MovementIQ;
        base.Start();
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void AttackPlayer()
    {
       StartCoroutine("AnimateAttack");
    }

    private IEnumerator AnimateAttack()
    {
        Ray ray = new Ray(transform.position, Player.transform.position - transform.position);
        Debug.DrawRay(transform.position, (Player.transform.position - transform.position).normalized * AttackRange, Color.blue);

        if (Physics.Raycast(ray, AttackRange, playerLayer))
        {
            print("player in range");
            if (!Physics.Raycast(transform.position, Player.transform.position, groundLayer))
            {
                print("no walls between");

                CancelInvoke("AttackPlayer");
                mainModule.MovementModule.MovementIQ = 0;
                yield return new WaitForSeconds(0.5f);

                //PLAY ANIMATION
                ArcaneProjectile aProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<ArcaneProjectile>();
                aProjectile.Damage = Damage;
                aProjectile.KnockBackForce = KnockBackForce;
                aProjectile.ProjectileSpeed = ProjectileSpeed;
                if (IQ >= 2)
                    aProjectile.IsAutoTargeted = true;
                else
                    aProjectile.Direction = (Player.transform.position - transform.position).normalized;

                yield return new WaitForSeconds(0.5f);
                mainModule.MovementModule.MovementIQ = defaultIQ;
                yield return new WaitForSeconds(Cooldown);
                InvokeRepeating("AttackPlayer", 0, 1f);
            }
        }
    }
    #endregion
}

