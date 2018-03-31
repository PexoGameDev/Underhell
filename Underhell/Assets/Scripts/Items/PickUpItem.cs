using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PickUpItem : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] EQItem eqitem;
    // Public Properties //
    public EQItem EQItem {
        get { return eqitem; }
        private set { eqitem = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void OnTriggerEnter(Collider other)
    {
        EquipmentManager playereq;
        if (playereq = other.GetComponent<EquipmentManager>())
        {
            playereq.AddItem(EQItem);
            Destroy(gameObject);
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

