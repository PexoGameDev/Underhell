using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int runes;

    private List<Chirograph> chirographs;
    private List<EQItem> items;
    // Public Properties //
    public int Runes
    {
        get { return runes; }
        set { runes = value; }
    }

    public List<Chirograph> Chirographs {
        get { return chirographs;}
        set { chirographs = value; }
    }
    public List<EQItem> Items
    {
        get { return items; }
        set { items = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        items = new List<EQItem>();
        chirographs = new List<Chirograph>();

    }

    void Update () {
		
	}
    #endregion

    #region Public Methods
    public void AddItem(EQItem item)
    {
        Items.Add(item);
        item.ApplyEffects(gameObject);
    }

    public void AddChirograph(Chirograph chirograph)
    {
        Chirographs.Add(chirograph);
    }
    #endregion

    #region Private Methods
    private void DropItem(EQItem item)
    {
        item.RevertEffects(gameObject);
        Items.Remove(item);
    }
    #endregion
}

