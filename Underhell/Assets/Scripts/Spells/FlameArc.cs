using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameArc : ChanneledSpell {
    #region Variables
    // Fields //
    [Header("Flame Arc")]
    [SerializeField] private float arcLenght = 25f;
    [SerializeField] private int DPS = 5;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public override bool Cast(Vector3 target)
    {
        Vector3 endPoint = Vector3.MoveTowards(Player.Entity.transform.position, target, arcLenght);

        LineRenderer arc = (Player.Entity.GetComponent<LineRenderer>() == null) ?  Player.Entity.AddComponent<LineRenderer>() : Player.Entity.GetComponent<LineRenderer>();
        arc.enabled = true;
        arc.SetColors(Color.yellow, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(Player.Entity.transform.position,endPoint, out hit, arcLenght, 1 << LayerMask.NameToLayer("Player")))
        {
            arc.SetPositions(new Vector3[] { Player.Entity.transform.position, hit.point});
            if (hit.collider.gameObject.GetComponent<HPModule>())
            {
                hit.collider.gameObject.GetComponent<HPModule>().GetHit(DPS, 0, Vector3.zero, AttackEffect.DamageSource.Fire);
            }
        }
        else
            arc.SetPositions(new Vector3[] { Player.Entity.transform.position, endPoint });

        return true;
    }
    #endregion

    #region Private Methods
    #endregion
}

