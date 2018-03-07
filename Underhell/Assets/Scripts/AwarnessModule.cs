using System.Collections;
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
    void Update()
    {
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.yellow);    
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private bool SeePlayer()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, DetectionRange))
            return hit.collider.gameObject == player;
        return false;
    }

    private void DetectPlayer()
    {
        if (SeePlayer())
        {
            enemy.MovementModule.IsChasingPlayer = true;
            timeSinceLastSeenPlayer = 0f;
        }
        else
        {
            timeSinceLastSeenPlayer += detectionDelay;
            if (enemy.MovementModule.IsChasingPlayer && timeSinceLastSeenPlayer >= stopChasingAfterSeconds)
            {
                enemy.MovementModule.IsChasingPlayer = false;
            }
        }

    }
    #endregion
}

