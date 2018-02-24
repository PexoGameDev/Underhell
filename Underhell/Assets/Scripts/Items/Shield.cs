using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : EQItem {
    
    #region Variables
    // Fields //

    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
		
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public override void ApplyEffects(GameObject player)
    {
        player.GetComponent<ShieldModule>().enabled = true;
    }

    public override void RevertEffects(GameObject player)
    {
        print("can't drop this?");
        //player.GetComponent<ShieldModule>().enabled = false;
    }
    #endregion

    #region Private Methods
    #endregion
}

