using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFullEffectTest : MonoBehaviour {
    public GameObject LoadingParticleEffect;
    public GameObject MidAirParticleEffect;
    public GameObject FinalExplosion;
    public GameObject Target;

    public float LoadingDuration = 1f;

    void Start () {
        Instantiate(LoadingParticleEffect, transform.position, Quaternion.identity);
        Invoke("SpawnProjectile", LoadingDuration);
	}
	
	// Update is called once per frame
	void SpawnProjectile() {
        Projectile proj = Instantiate(MidAirParticleEffect, transform.position, Quaternion.identity).GetComponent<Projectile>();
        proj.target = Target;
        proj.FinalExplosion = FinalExplosion;
    }
}
