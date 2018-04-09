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
    [SerializeField] private EQItem[] items;
    private Coroutine fadingCoroutine;
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

        if (Input.GetKeyDown(KeyCode.Backspace))
            DropItem(SelectedEQIndex);
    }
    #endregion

    #region Public Methods
    public bool AddItem(EQItem item)
    {
        if (itemsInEQ >= 7)
            return false;

        Items[itemsInEQ] = item;
        itemsInEQ++;
        item.ApplyEffects(gameObject);

        if(fadingCoroutine != null)
            StopCoroutine(fadingCoroutine);
        fadingCoroutine = StartCoroutine(DisplayPickedUpItem(item));
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
        print("I'm droping: " + Items[0].name);
        Items[eqFieldNumber].RevertEffects(gameObject);
        //ADD DROPING ITEM ON THE GROUND
        //MOVE ALL ITEMS ABOVE IT ONE FIELD BELOW SO ALWAYS ADD NEW ITEM ON TOP OF ARRAY
        Items[eqFieldNumber] = null;
        itemsInEQ--;
    }

    private IEnumerator DisplayPickedUpItem(EQItem eqItem)
    {
        Color defaultNameColor = UIManager.ItemNameText.color;
        Color defaultDescriptionColor = UIManager.ItemDescriptionText.color;

        //UIManager.ItemNameText.color = new Color(defaultNameColor.r, defaultNameColor.g, defaultNameColor.b, 1f);
       // UIManager.ItemDescriptionText.color = new Color(defaultDescriptionColor.r, defaultDescriptionColor.g, defaultDescriptionColor.b, 1f);

        UIManager.ItemNameText.text = eqItem.Name;
        UIManager.ItemDescriptionText.text = eqItem.Description;

        while (UIManager.ItemNameText.color.a > 0)
        {
            UIManager.ItemNameText.color = new Color(defaultNameColor.r, defaultNameColor.g, defaultNameColor.b, UIManager.ItemNameText.color.a - 0.01f);
            UIManager.ItemDescriptionText.color = new Color(defaultDescriptionColor.r, defaultDescriptionColor.g, defaultDescriptionColor.b, UIManager.ItemNameText.color.a - 0.01f);
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}

