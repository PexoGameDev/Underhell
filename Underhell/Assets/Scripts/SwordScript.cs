using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MeleeWeapon {
    #region Variables
    // Fields //
    [SerializeField] int damage = 1;
    [SerializeField] GameObject prefab;

    // Public Properties //
    public override int Damage { get; set; }
    public override GameObject Prefab { get; set; }

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
	}
	
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyHPModule>())
            other.GetComponent<EnemyHPModule>().GetHit(damage);
    }
    #endregion

    #region Public Methods
    public override IEnumerator Attack(int isLookingRight, Transform parent)
    {
        Damage = damage;
        Prefab = prefab;
        GameObject sword = Instantiate(Prefab, parent);
        for (int i = 0; i < 18; i++)
        {
            sword.transform.Rotate(0,0,10f*-isLookingRight);
            yield return new WaitForFixedUpdate();
        }
        Destroy(sword);
    }
    #endregion

    #region Private Methods
    #endregion
}

