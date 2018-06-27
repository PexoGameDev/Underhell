using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcastingModule : MonoBehaviour {
    #region Variables
    // Fields //
    public GameObject GlyphsDisplay;

    [SerializeField] private KeyCode opsiKey;
    [SerializeField] private KeyCode ixiKey;
    [SerializeField] private KeyCode auoKey;
    [SerializeField] private KeyCode yuhKey;

    public List<Glyph> glyphsCast;
    [SerializeField] private Spellbook spellbook;

    private LineRenderer lineRenderer;
    [SerializeField] public static float maxSpellDuration = 2f;
    // Public Properties //

    // Private Properties //

    // Public Data Structures //
    public enum Glyph
    {
        opsi = 0,
        ixi = 1,
        auo = 2,
        yuh = 3
    }
    #endregion

    #region Unity Methods
    void Start () {
        glyphsCast = new List<Glyph>();
        lineRenderer = Player.Entity.GetComponent<LineRenderer>() ?? Player.Entity.AddComponent<LineRenderer>();
        lineRenderer.enabled = false;
	}
	
	void Update () {
        if (Input.GetKeyDown(opsiKey))
            glyphsCast.Add(Glyph.opsi);
        if (Input.GetKeyDown(ixiKey))
            glyphsCast.Add(Glyph.ixi);
        if (Input.GetKeyDown(auoKey))
            glyphsCast.Add(Glyph.auo);
        if (Input.GetKeyDown(yuhKey))
            glyphsCast.Add(Glyph.yuh);
        if (Input.GetMouseButton(1))
            CastSpell();
        if (Input.GetMouseButtonUp(1))
        {
            glyphsCast.Clear();
            lineRenderer.enabled = false;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Casting();
        }

        if (Input.GetMouseButtonUp(0))
        {
            GlyphsDisplay.SetActive(false);
            Time.timeScale = 1;
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private void CastSpell()
    {
        if (glyphsCast.Count > 0)
        {
            string spellCode = "";
            for (int i = 0; i < glyphsCast.Count; i++)
            {
                spellCode += (int) glyphsCast[i];
            }

            print(spellCode);

            try
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    while (!spellbook.Spells[spellCode].Cast(hit.point))
                        return;
            }
            catch
            {
                print("Wrong spell!");
            }
        }
        lineRenderer.enabled = false;
        glyphsCast.Clear();
    }

    private void Casting()
    {
        Time.timeScale = 0.33f;
        GlyphsDisplay.SetActive(true);
    }
    #endregion
}

