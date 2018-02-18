using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] GameObject SwordPrefab;

    private bool isAttacking = false;

    private PlayerMovement playerMovement;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start () {
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    IEnumerator Attack()
    {
        GameObject sword = Instantiate(SwordPrefab, transform);

        if(playerMovement.Rotation == -1)
            sword.transform.Rotate(0, 0, 90f);
        else
            sword.transform.Rotate(0, 0, -90f);

        for (int i = 0; i < 18; i++)
        {
            sword.transform.Rotate(0, 0, 10f);
            yield return new WaitForFixedUpdate();
        }
        isAttacking = false;
        Destroy(sword);
    }
    #endregion
}

