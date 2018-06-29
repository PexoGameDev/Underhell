using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingPhoenixEffect : SpellEffect {
    public int damage = 0;
    public Vector3 direction;
    public float speed = 1f;

    private void Start()
    {
        Destroy(gameObject, 10f);
        direction = (target - Player.SpellsOrigin.position).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        HPModule enemy;
        if(enemy = other.GetComponent<HPModule>())
        {
            enemy.GetHit(damage, 0f, Vector3.zero, AttackEffect.DamageSource.Fire);
        }
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;
    }
}
