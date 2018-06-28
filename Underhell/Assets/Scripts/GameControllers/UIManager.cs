using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    #region Variables
    // Fields //
    public static Text ScoreText;
    public static Text ItemNameText;
    public static Text ItemDescriptionText;
    public static Text HighlightedItemText;
    public static Image[] ItemsEQImages;

    private static Image highlightedItemEQImage;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        ItemNameText = GameObject.Find("ItemNameText").GetComponent<Text>();
        ItemDescriptionText = GameObject.Find("ItemDescriptionText").GetComponent<Text>();
        HighlightedItemText = GameObject.Find("HighlightedItemText").GetComponent<Text>();
        ItemsEQImages = GameObject.Find("EquipmentUI").GetComponentsInChildren<Image>();
    }

    void Update () {
		
	}
    #endregion

    #region Public Methods
        HighlightedItemText.text = itemName;
    public static void UpdateScore() => ScoreText.text = "Score: " + PersistentData.Score;
    #endregion

    #region Private Methods
    #endregion
}

