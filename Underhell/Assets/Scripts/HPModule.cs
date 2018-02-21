using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPModule : MonoBehaviour {
    #region Variables
    // Fields //
    public float InvulnerabilityDuration = 0.2f;

    [SerializeField] private int hP = 10;

    private bool isInvulnerable = false;
    // Public Properties //
    public int HP
    {
        get { return hP; }
        set
        {
            if ((hP = value) <= 0)
            {
                if (GetComponent<Enemy>())
                    GetComponent<Enemy>().Die();
                else
                    Destroy(gameObject);
            }
            else
                hP = value;
        }
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
        if (!isInvulnerable)
        {
            HP -= damage;
            StartCoroutine("AnimateHurt");
        }
    }
    #endregion

    #region Private Methods
    IEnumerator AnimateHurt()
    {
        isInvulnerable = true;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(InvulnerabilityDuration);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        isInvulnerable = false;
        yield return null;
    }
    #endregion
}

