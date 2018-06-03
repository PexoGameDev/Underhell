using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameArc : Spell {
    #region Variables
    // Fields //
    [Header("Flame Arc")]
    [SerializeField] private float arcLenght = 25f;
    [SerializeField] private int DPS = 5;

    private float duration = 0f;
    // Public Properties //

    // Private Properties //
    private float Duration
    {
        get { return duration; }
        set {
                    
            }
    }
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

        while (Input.GetMouseButton(1))
        {
            if(Physics.Raycast(Player.Entity.transform.position,endPoint, out hit, arcLenght))
            {
                arc.SetPositions(new Vector3[] { Player.Entity.transform.position, hit.point});
                if (hit.collider.gameObject.GetComponent<HPModule>())
                {
                    hit.collider.gameObject.GetComponent<HPModule>().GetHit(DPS, 0, Vector3.zero, AttackEffect.DamageSource.Fire);
                }
            }
            else
                arc.SetPositions(new Vector3[] { Player.Entity.transform.position, endPoint });

            if ((duration += Time.fixedDeltaTime) >= SpellcastingModule.maxSpellDuration)
            {
                duration = 0f;
                return true;
            }

            return false;
        }

        return true;
    }
    #endregion

    #region Private Methods
    #endregion
}

