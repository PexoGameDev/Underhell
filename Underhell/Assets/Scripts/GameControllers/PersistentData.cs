using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private int curse;

    private static int score;

    // Public Properties //
    public static int Score
    {
        get { return score; }
        set
        {
            score = value;
            UIManager.UpdateScore();
        }
    }
    public int Curse
    {
        get { return curse; }
        set { curse = value; }
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

