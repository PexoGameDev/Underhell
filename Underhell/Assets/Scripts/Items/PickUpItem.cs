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
                UIManager.ItemNameText.text = EQitem.Name;
                UIManager.ItemDescriptionText.text = EQitem.Description;
                UIManager.ItemDescriptionText.canvasRenderer.SetAlpha(1f);
                UIManager.ItemNameText.CrossFadeAlpha(0f, 2f, true);
            }
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

