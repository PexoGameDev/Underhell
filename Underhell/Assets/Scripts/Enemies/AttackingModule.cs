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

    public int playerLayer;
    public HPModule Player;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    public void Start () {
        print("BASE START");
        Player = gameObject.GetComponent<Enemy>().Player.GetComponent<HPModule>();
        playerLayer = LayerMask.GetMask("Player");

        InvokeRepeating("AttackPlayer", 0, 1f);
    }
    #endregion

    #region Public Methods
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

