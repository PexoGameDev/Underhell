using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedBoots : EQItem {
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
        PlayerMovement playerMovement = player.GetComponentInParent<PlayerMovement>();

        playerMovement.JumpHeight += 2f;
        playerMovement.JumpForce += 8f;
    }

    public override void RevertEffects(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponentInParent<PlayerMovement>();

        playerMovement.JumpHeight -= 2f;
        playerMovement.JumpForce -= 8f;
    }

    #endregion

    #region Private Methods
    #endregion
}

