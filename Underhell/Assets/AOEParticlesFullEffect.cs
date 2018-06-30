using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEParticlesFullEffect : MonoBehaviour {

    public GameObject IndicatorEffect;
    public GameObject Explosion;

    public float LoadingDuration = 1f;

    void Start()
    {
        Instantiate(IndicatorEffect, transform.position, Quaternion.identity);
        Invoke("SpawnExplosion", LoadingDuration);
    }

    // Update is called once per frame
    void SpawnExplosion()
    {
        Projectile proj = Instantiate(Explosion, transform.position, Quaternion.identity).GetComponent<Projectile>();
    }
}
