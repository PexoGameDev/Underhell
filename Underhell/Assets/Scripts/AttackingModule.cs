﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackingModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int IQ = 0;
    [SerializeField] private int damage = 1;
    [SerializeField] private float cooldown = 0.1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float knockBackForce = 1f;

    private int playerLayer;
    private HPModule Player;
    // Public Properties //
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        Player = gameObject.GetComponent<Enemy>().Player.GetComponent<HPModule>();
        playerLayer = LayerMask.GetMask("Player");

        InvokeRepeating("AttackPlayer", 0, 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        
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

        if (Physics.Raycast(ray, attackRange, playerLayer))
        {
            yield return StartCoroutine(AnimateAttack());

            if (Physics.Raycast(ray, attackRange, playerLayer))
                Player.GetHit(Damage, knockBackForce, transform.position);

            yield return new WaitForSeconds(cooldown);
        }
    }

    private IEnumerator AnimateAttack()
    {
        yield return new WaitForSeconds(0.2f);
    }
    #endregion
}

