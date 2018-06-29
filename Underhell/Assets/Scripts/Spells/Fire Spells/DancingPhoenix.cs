using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingPhoenix : CastSpell {

    [SerializeField] private GameObject phoenixPrefab;
    public override void Cast(Vector3 target)
    {
        Instantiate(phoenixPrefab, Player.SpellsOrigin.position, Quaternion.identity).GetComponentInChildren<DancingPhoenixEffect>().target = target;
    }
}
