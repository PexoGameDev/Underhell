using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFire : MonoBehaviour {
    #region Variables
    // Fields //
    HPModule targetAblaze;
    // Public Properties //
    public int Damage { get; set; }
    public float DamageInterval { get; set; }
    public float Duration { get; set; }

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        print("Im buuuurninnng!");
        //MAKING SURE THAT TARGET IS ONLY "ON ONE FIRE" - NO STACKING ALLOWED
        OnFire other;
        if ((other = gameObject.GetComponent<OnFire>()) && other!=this)
            Destroy(other);

        targetAblaze = gameObject.GetComponent<HPModule>();
        Destroy(this,Duration);
        InvokeRepeating("Burn", 0f, DamageInterval);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void Burn()
    {
        targetAblaze.GetHit(Damage);
    }
    #endregion
}

