using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    #region Variables
    // Fields //
    public static Text ScoreText;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public static void UpdateScore()
    {
        ScoreText.text = "Score: " + PersistentData.Score;
    }
    #endregion

    #region Private Methods
    #endregion
}

