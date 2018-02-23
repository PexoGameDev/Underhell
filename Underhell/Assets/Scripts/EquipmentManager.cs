using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int runes;

    private Chirograph[] chirographs;
    private EQItem[] items;
    // Public Properties //
    public int Runes
    {
        get { return runes; }
        set { runes = value; }
    }

    public Chirograph[] Chirographs {
        get { return chirographs;}
        set { chirographs = value; }
    }
    public EQItem[] Items
    {
        get { return items; }
        set { items = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

