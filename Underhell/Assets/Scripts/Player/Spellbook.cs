using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour {
    #region Variables
    // Fields //

    // Public Properties //
    public Dictionary<Element, Dictionary<string, Spell>> Spells
    {
        get;
        private set;
    }

    private Dictionary<string, Spell> FireSpells;
    private Dictionary<string, Spell> ElectricitySpells;
    private Dictionary<string, Spell> QuantumSpell;

    // Private Properties //

    // Public Data Structures //
    public enum Element
    {
        fire = 0,
        electricity = 1,
        quantum = 2
    }
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
        FireSpells = new Dictionary<string, Spell>
        {
            {"def", GetComponent<Fireball>() },
            {"201", GetComponent<FlameArc>() }
        };

        ElectricitySpells = new Dictionary<string, Spell>
        {
            {"def", GetComponent<Fireball>() }
        };

        QuantumSpell = new Dictionary<string, Spell>
        {
            {"def", GetComponent<Fireball>() }
        };

        Spells = new Dictionary<Element, Dictionary<string,Spell>>
        {
            { Element.fire, FireSpells },
            { Element.electricity, ElectricitySpells },
            { Element.quantum, QuantumSpell }
        };

    }
    #endregion
}

