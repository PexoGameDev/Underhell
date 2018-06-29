using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameArc : ChanneledSpell {
    #region Variables
    // Fields //
    [Header("Flame Arc")]
    [SerializeField] private float arcLenght = 25f;
    [SerializeField] private int DPS = 5;
    LineRenderer arc;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    private void Start()
    {
        arc = Player.Entity.GetComponent<LineRenderer>() ?? Player.Entity.AddComponent<LineRenderer>();
    }
    #endregion

    #region Public Methods
    public override void Cast(Vector3 target)
    {
        Vector3 endPoint = Vector3.MoveTowards(Player.SpellsOrigin.position, target, arcLenght);

        arc.enabled = true;
        arc.SetColors(Color.yellow, Color.red);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(Player.SpellsOrigin.position, endPoint - Player.SpellsOrigin.position, Vector3.Distance(Player.SpellsOrigin.position, endPoint));

        for (int i = 0; i < hits.Length; i++)
            if (hits[i].collider.gameObject.GetComponent<HPModule>())
                hits[i].collider.gameObject.GetComponent<HPModule>().GetHit(DPS, 0, Vector3.zero, AttackEffect.DamageSource.Fire);

        //    print("XD");
        //    arc.SetPositions(new Vector3[] { Player.SpellsOrigin.position, hit.point });
        //}
        //else
        arc.SetPositions(new Vector3[] { Player.SpellsOrigin.position, endPoint });
    }

    public override void ChannelEnd(Vector3 target)
    {
        arc.enabled = false;
    }
    #endregion

    #region Private Methods
    #endregion
}

