using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int hP = 10;

    // Public Properties //
    public int HP
    {
        get { return hP; }
        set { hP = value; }
    }

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public void GetHit(int damage)
    {
        HP -= damage;
        StartCoroutine("AnimateHurt");
    }
    #endregion

    #region Private Methods
    IEnumerator AnimateHurt()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        yield return null;
    }
    #endregion
}

