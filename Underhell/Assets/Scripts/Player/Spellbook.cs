using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour {
    #region Variables
    // Fields //

    // Public Properties //
    public Dictionary<string, Spell> Spells
    {
        get;
        private set;
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        FillSpells();
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void FillSpells()
    {
        Spells = new Dictionary<string, Spell>();
        Spells.Add("010", GetComponent<Fireball>());
        Spells.Add("232", GetComponent<FlameArc>());

    }
    #endregion
}

