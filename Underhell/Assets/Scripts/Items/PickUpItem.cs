using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PickUpItem : MonoBehaviour {
    #region Variables
    // Fields //
    public EQItem EQitem;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void OnTriggerEnter(Collider other)
    {
        EquipmentManager playereq;
        if (playereq = other.GetComponent<EquipmentManager>())
        {
            if (playereq.AddItem(EQitem))
            {
                Destroy(gameObject);
            }
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

