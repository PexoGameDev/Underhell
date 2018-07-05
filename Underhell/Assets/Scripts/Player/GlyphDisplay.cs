using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphDisplay : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private SpellcastingModule.Glyph glyph;
    private SpellcastingModule spellcastingModule;
    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        spellcastingModule = Player.Entity.GetComponent<SpellcastingModule>();
    }

    public void AddGlyph()
    {
        spellcastingModule.glyphsCast.Add(glyph);
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

