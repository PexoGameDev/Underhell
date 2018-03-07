using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShieldModule))]
public class HPModule : MonoBehaviour {
    #region Variables
    // Fields //
    public float InvulnerabilityDuration = 0.2f;

    [SerializeField] private int hP = 10;

    private bool isInvulnerable = false;

    private Rigidbody rb;
    private ShieldModule shield;
    // Public Properties //
    public int HP
    {
        get { return hP; }
        set
        {
            if (shield.enabled)
                shield.enabled = false;
            else
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
        rb = GetComponent<Rigidbody>();
        shield = GetComponent<ShieldModule>();
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public void GetHit(int damage, float knockBackForce, Vector3 knockBackDirection)
    {
        if (!isInvulnerable)
        {
            HP -= damage;
            Vector3 knockBackTo = (transform.position - knockBackDirection + Vector3.up*0.5f).normalized * knockBackForce;
            knockBackTo.z = 0;
            rb.AddForce(knockBackTo, ForceMode.Impulse);
            CancelInvoke("ResetVelocity");
            Invoke("ResetVelocity", 0.3f);
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

    void ResetVelocity()
    {
        CancelInvoke("ResetVelocity");
        rb.velocity = Vector3.zero;
    }
    #endregion
}

