using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int runes;

    private int selectedEQIndex = 0;
    private int itemsInEQ = 0;
    private List<Chirograph> chirographs;
    private EQItem[] items;
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
    public EQItem[] Items
    {
        get { return items; }
        set { items = value; }
    }
    // Private Properties //
    private int SelectedEQIndex
    {
        get { return selectedEQIndex; }
        set { selectedEQIndex = (value + 1) % 7; }
    }
    #endregion

    #region Unity Methods
    void Start () {
        items = new EQItem[7];
        chirographs = new List<Chirograph>();
    }

    void Update () {
        if (Input.mouseScrollDelta.y > 0)
            SelectedEQIndex++;
        if (Input.mouseScrollDelta.y < 0)
            SelectedEQIndex--;

    }
    #endregion

    #region Public Methods
    public bool AddItem(EQItem item)
    {
        if (itemsInEQ >= 7)
            return false;

        Items[itemsInEQ++] = item;
        item.ApplyEffects(gameObject);
        return true;
    }

    public void AddChirograph(Chirograph chirograph)
    {
        Chirographs.Add(chirograph);
    }
    #endregion

    #region Private Methods
    private void DropItem(int eqFieldNumber)
    {
        Items[eqFieldNumber].RevertEffects(gameObject);
        Items[eqFieldNumber] = null;
        itemsInEQ--;
    }
    #endregion
}

