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
            {"def", GetComponentInChildren<FireBolts>() },
            {"10", GetComponentInChildren<PillarOfFlames>() },
            {"02", GetComponentInChildren<Combustion>() },
            {"102", GetComponentInChildren<FlameArc>() },
            {"1012", GetComponentInChildren<FlameChains>() },
            {"20102", GetComponentInChildren<DancingPhoenix>() },
        };

        ElectricitySpells = new Dictionary<string, Spell>
        {
            {"def", GetComponentInChildren<FireBolts>() }
        };

        QuantumSpell = new Dictionary<string, Spell>
        {
            {"def", GetComponentInChildren<FireBolts>() }
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

