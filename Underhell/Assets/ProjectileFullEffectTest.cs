using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFullEffectTest : MonoBehaviour {
    public GameObject LoadingParticleEffect;
    public float LoadingDuration = 1f;
    public GameObject MidAirParticleEffect;
    public GameObject Target;
    public float ProjectileSpeed = 10f;
    public GameObject FinalExplosion;


    void Start () {
        Instantiate(LoadingParticleEffect, transform.position, LoadingParticleEffect.transform.rotation);
        Invoke("SpawnProjectile", LoadingDuration);
	}
	
	// Update is called once per frame
	void SpawnProjectile() {
        Projectile proj = Instantiate(MidAirParticleEffect, transform.position, MidAirParticleEffect.transform.rotation).GetComponent<Projectile>();
        proj.target = Target;
        proj.FinalExplosion = FinalExplosion;
        proj.projectileSpeed = ProjectileSpeed;
    }
}
