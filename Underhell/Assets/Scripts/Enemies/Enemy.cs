using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AttackingModule))]
[RequireComponent(typeof(HPModule))]
[RequireComponent(typeof(MovementModule))]
public class Enemy : MonoBehaviour {
    #region Variables
    // Fields //
    private GameObject player;
    private HPModule hpModule;
    private MovementModule movementModule;
    private AttackingModule attackingModule;
    private AwarnessModule awarnessModule;

    [SerializeField] private int scoreValue = 10;
    // Public Properties //
    public GameObject Player
    {
        get { return player; }
        private set { player = value; }
    }
    public HPModule HPModule
    {
        get { return hpModule; }
        private set { hpModule = value; }
    }

    public MovementModule MovementModule
    {
        get { return movementModule; }
        private set { movementModule = value; }
    }

    public AttackingModule AttackingModule
    {
        get { return attackingModule; }
        private set { attackingModule = value; }
    }

    public AwarnessModule AwarnessModule
    {
        get { return awarnessModule; }
        private set { awarnessModule = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hpModule = gameObject.GetComponent<HPModule>();
        movementModule = gameObject.GetComponent<MovementModule>();
        attackingModule = gameObject.GetComponent<AttackingModule>();
        awarnessModule = gameObject.GetComponent<AwarnessModule>();
    }
    void Start () {
    }

    void Update () {
		
	}
    #endregion

    #region Public Methods
    public void Die()
    {
        //PersistentData.Score += scoreValue;
        //add to objectpool
        //instead of destroying
        gameObject.GetComponentInChildren<Animator>().Play("Die");
        print("I diededed");
        Destroy(gameObject,0.75f);
    }
    #endregion

    #region Private Methods
    #endregion
}

