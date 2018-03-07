using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEBUGUI : MonoBehaviour {
    #region Variables
    // Fields //
    public Button spawnEnemy;
    public GameObject enemy;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        AddButtonListeners();
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void AddButtonListeners()
    {
        spawnEnemy.onClick.AddListener(delegate { Instantiate(enemy, new Vector3(200f,50f,-3f), Quaternion.identity); });
    }
    #endregion
}

