using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShieldModule))]
public class HPModule : MonoBehaviour {
    #region Variables
    // Fields //
    public int MaxHP = 10;
    public float InvulnerabilityDuration = 0.2f;
    public float PoisonResistance = 0f;
    public float FireResistance = 0f;
    public float SlowResistance = 0f;
    public bool SnareResistance = false;
    public bool ParalyzeResistance = false;
    [SerializeField] GameObject hitParticles;

    private int hP;
    private bool isInvulnerable = false;
    private Rigidbody rb;
    private ShieldModule shield;
    private List<Spell> damageOverTimeSpellsInEffect;
    // Public Properties //
    public int HP
    {
        get { return hP; }
        set
        {
            if (((hP = value) <= hP) && shield.enabled)
                shield.enabled = false;
            else
                if ((hP = value) <= 0)
            {
                if (GetComponent<Enemy>())
                    GetComponent<Enemy>().Die();
                else
                    print("PLAYER DIED - GAME OVER");
            }
            else if ((hP = value) > MaxHP)
                hP = MaxHP;
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
        hP = MaxHP;
        damageOverTimeSpellsInEffect = new List<Spell>();
    }
    #endregion

    #region Public Methods
    public void GetHit(int damage, float knockBackForce, Vector3 knockBackDirection, AttackEffect.DamageSource damageSource = AttackEffect.DamageSource.Physical)
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

    public void TakeDamageOverTime(int damage, AttackEffect.DamageSource damageSource ,Spell source)
    {
        if(!damageOverTimeSpellsInEffect.Contains(source))
        {
            damageOverTimeSpellsInEffect.Add(source);
            GetHit(damage, 0f, Vector3.zero, damageSource);
            StartCoroutine(RemoveDamageOverTimeSource(source));
        }
    }
    #endregion

    #region Private Methods
    IEnumerator AnimateHurt()
    {
        isInvulnerable = true;
        GameObject particles = Instantiate(hitParticles);
        yield return new WaitForSeconds(InvulnerabilityDuration);
        Destroy(particles);
        isInvulnerable = false;
        yield return null;
    }

    void ResetVelocity()
    {
        CancelInvoke("ResetVelocity");
        rb.velocity = Vector3.zero;
    }

    IEnumerator RemoveDamageOverTimeSource(Spell source)
    {
        yield return new WaitForSeconds(1f);
        damageOverTimeSpellsInEffect.Remove(source);
    }
    #endregion
}

