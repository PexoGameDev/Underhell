﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class AwarnessModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] [Range(0, 5)] private int IQ = 0;
    [SerializeField] private float stopChasingAfterSeconds = 3f;
    [SerializeField] private float detectionDelay = 0.1f;
    [SerializeField] private float detectionRange = 5f;
    public bool chaseMelee;

    private float timeSinceLastSeenPlayer = 0f;
    private GameObject player;
    private Enemy enemy;
    // Public Properties //

    public float DetectionRange
    {
        get { return detectionRange; }
        set { detectionRange = value; }
    }

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        enemy = gameObject.GetComponent<Enemy>();
        player = enemy.Player;
        InvokeRepeating("DetectPlayer", 0f, detectionDelay);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    public bool SeePlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + transform.localScale.y * Vector3.up * 0.5f, player.transform.position - transform.position, out hit, DetectionRange))
            return hit.collider.gameObject == player;
        return false;
    }
    public bool SeePlayer(float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.localScale.y * Vector3.up * 0.5f, player.transform.position - transform.position, out hit, distance))
            return hit.collider.gameObject == player;
        return false;
    }

    private void DetectPlayer()
    {
        if (SeePlayer())
        {
            timeSinceLastSeenPlayer = 0f;
        }
        else
        {
            timeSinceLastSeenPlayer += detectionDelay;
        }

    }
    #endregion
}

