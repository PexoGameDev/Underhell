using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArcaneAttackingModule : AttackingModule
{
    #region Variables
    // Fields //
    [SerializeField] private float ProjectileSpeed = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float turnOffAutoTargetDistance = 2f;

    private int defaultIQ; 
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    new private void Start()
    {
        defaultIQ = mainModule.MovementModule.MovementIQ;
        base.Start();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, Player.transform.position - transform.position + Vector3.up, Color.red);
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
        Ray ray = new Ray(transform.position + transform.localScale.y * 0.5f * Vector3.up, Player.transform.position - transform.position + Vector3.up * Player.transform.localScale.y);

        if (Physics.Raycast(ray, AttackRange, playerLayer))
        {
            if (!Physics.Raycast(ray,Vector3.Distance(transform.position,Player.transform.position), groundLayer))
            {

                CancelInvoke("AttackPlayer");
                mainModule.MovementModule.MovementIQ = 0;
                yield return new WaitForSeconds(0.5f);

                //PLAY ANIMATION
                ArcaneProjectile aProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<ArcaneProjectile>();
                aProjectile.Damage = Damage;
                aProjectile.KnockBackForce = KnockBackForce;
                aProjectile.ProjectileSpeed = ProjectileSpeed;
                aProjectile.TurnOffAutoTargetDistance = turnOffAutoTargetDistance;
                if (IQ >= 2)
                    aProjectile.IsAutoTargeted = true;
                else
                    aProjectile.Direction = (Player.transform.position + Vector3.up - transform.position).normalized;

                yield return new WaitForSeconds(0.5f);
                mainModule.MovementModule.MovementIQ = defaultIQ;
                yield return new WaitForSeconds(Cooldown);
                InvokeRepeating("AttackPlayer", 0, 1f);
            }
        }
    }
    #endregion
}

