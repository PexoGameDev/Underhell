using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePolygonProjectileTest : MonoBehaviour {

    [Header("Dotykaj")]
    public bool RespawnFullEffect = true;

    public float RespawningDelay = 2f;

    [Header("Nie Dotykaj")]
    public GameObject FullEffect;
    public Vector3 position;
    void Start () {
        InvokeRepeating("SpawnProjectile", RespawningDelay, RespawningDelay);
	}
	
	// Update is called once per frame
	void SpawnProjectile() {
        Instantiate(FullEffect, transform).transform.localPosition = position;
	}
}
