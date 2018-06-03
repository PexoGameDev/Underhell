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
        set
        {
            if (value > 0)
                selectedEQIndex = value % 7;
            else
                if ((selectedEQIndex = value) < 0)
                    selectedEQIndex = 6;
        }
    }
    #endregion

    #region Unity Methods
    void Start () {
        items = new EQItem[7];
        chirographs = new List<Chirograph>();
    }

    void Update () {
        if (Input.mouseScrollDelta.y > 0f)
        {
            SelectedEQIndex++;
            if(Items[SelectedEQIndex] != null)
                UIManager.HighlightItemEQImage(SelectedEQIndex, Items[SelectedEQIndex].Name);
            else
                UIManager.HighlightItemEQImage(SelectedEQIndex, "");
        }
        else if (Input.mouseScrollDelta.y < 0f)
        {
            SelectedEQIndex--;
            if (Items[SelectedEQIndex] != null)
                UIManager.HighlightItemEQImage(SelectedEQIndex, Items[SelectedEQIndex].Name);
            else
                UIManager.HighlightItemEQImage(SelectedEQIndex, "");
        }


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
        UIManager.SetEQItemImage(itemsInEQ, item.Image);
        item.ApplyEffects(gameObject);
        itemsInEQ++;

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
        UIManager.SetEQItemImage(eqFieldNumber, null); // ADD DEFAULT EQ ITEM ICON
        //ADD DROPING ITEM ON THE GROUND
        //MOVE ALL ITEMS ABOVE IT ONE FIELD BELOW SO ALWAYS ADD NEW ITEM ON TOP OF ARRAY
        if(eqFieldNumber > 5)
            for(int i = eqFieldNumber + 1; i < Items.Length; i++)
            {
                if (Items[i] == null)
                    break;

                Items[i - 1] = Items[i];
            }

        Items[eqFieldNumber] = null;
        itemsInEQ--;
    }

    private IEnumerator DisplayPickedUpItem(EQItem eqItem)
    {
        print(eqItem.name);

        Color defaultNameColor = UIManager.ItemNameText.color;
        Color defaultDescriptionColor = UIManager.ItemDescriptionText.color;

        UIManager.ItemNameText.color = new Color(defaultNameColor.r, defaultNameColor.g, defaultNameColor.b, 1f);
        UIManager.ItemDescriptionText.color = new Color(defaultDescriptionColor.r, defaultDescriptionColor.g, defaultDescriptionColor.b, 1f);

        UIManager.ItemNameText.enabled = true;
        UIManager.ItemDescriptionText.enabled = true;

        UIManager.ItemNameText.text = eqItem.Name;
        UIManager.ItemDescriptionText.text = eqItem.Description;

        yield return new WaitForSeconds(4f);


        UIManager.ItemNameText.enabled = false;
        UIManager.ItemDescriptionText.enabled = false;

        //UIManager.ItemNameText.CrossFadeAlpha(0, 2f, false);
        //UIManager.ItemDescriptionText.CrossFadeAlpha(0, 2f, false);
    }
    #endregion
}

