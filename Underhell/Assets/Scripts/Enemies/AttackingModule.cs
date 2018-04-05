using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackingModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] [Range(0, 3)] public int IQ = 0;
    [SerializeField] public int Damage = 1;
    [SerializeField] public float Cooldown = 0.1f;
    [SerializeField] public float AttackRange = 1f;
    [SerializeField] public float KnockBackForce = 1f;

    [HideInInspector] public HPModule Player;
    [HideInInspector] public Enemy mainModule;
    public int playerLayer;
    public int groundLayer;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    public void Start () {
        mainModule = gameObject.GetComponent<Enemy>();
        Player = mainModule.Player.GetComponent<HPModule>();
        playerLayer = LayerMask.GetMask("Player");
        groundLayer = LayerMask.GetMask("Ground");

        InvokeRepeating("AttackPlayer", 0, 1f);
    }
    #endregion

    #region Public Methods
    public bool SeePlayer()
    {
        Ray ray = new Ray(transform.position + transform.localScale.y * 0.5f * Vector3.up, Player.transform.position - transform.position + Vector3.up * Player.transform.localScale.y);
        if (Physics.Raycast(ray, AttackRange, playerLayer))
            return !Physics.Raycast(ray, Vector3.Distance(transform.position, Player.transform.position), groundLayer);
        return false;
    }

    public bool SeePlayer(float customAttackRange)
    {
        Ray ray = new Ray(transform.position + transform.localScale.y * 0.5f * Vector3.up, Player.transform.position - transform.position + Vector3.up * Player.transform.localScale.y);
        if (Physics.Raycast(ray, customAttackRange, playerLayer))
            return !Physics.Raycast(ray, Vector3.Distance(transform.position, Player.transform.position), groundLayer);
        return false;
    }
    #endregion

    #region Private Methods
    private void AttackPlayer()
    {
        StartCoroutine("CheckAttack");
    }

    private IEnumerator CheckAttack()
    {
        Ray ray = new Ray(transform.position, Player.transform.position-transform.position);

        if (Physics.Raycast(ray, AttackRange, playerLayer))
        {
            yield return StartCoroutine(AnimateAttack());

            if (Physics.Raycast(ray, AttackRange, playerLayer))
                Player.GetHit(Damage, KnockBackForce, transform.position);

            yield return new WaitForSeconds(Cooldown);
        }
    }

    private IEnumerator AnimateAttack()
    {
        yield return new WaitForSeconds(0.2f);
    }
    #endregion
}

