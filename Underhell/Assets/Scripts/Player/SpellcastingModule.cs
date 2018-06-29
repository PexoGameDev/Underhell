using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellcastingModule : MonoBehaviour {
    #region Variables
    // Fields //
    [SerializeField] private GameObject GlyphsDisplay;
    [SerializeField] private GameObject castingBar;

    [HideInInspector] public List<Glyph> glyphsCast;
    [SerializeField] private Spellbook spellbook;

    private LineRenderer lineRenderer;
    [SerializeField] public static float maxChannelTime = 2f;

    private Spellbook.Element currentElement = Spellbook.Element.fire;

    private Coroutine CastingCoroutine;
    // Public Properties //

    // Private Properties //

    // Public Data Structures //
    public enum Glyph
    {
        yuh = 0,
        ixi = 1,
        auo = 2
    }
    #endregion

    #region Unity Methods
    void Start () {
        glyphsCast = new List<Glyph>();
        lineRenderer = Player.Entity.GetComponent<LineRenderer>() ?? Player.Entity.AddComponent<LineRenderer>();
        lineRenderer.enabled = false;
        castingBar.SetActive(false);
    }
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            if (CastingCoroutine != null)
                StopCoroutine(CastingCoroutine);
            CastingCoroutine = StartCoroutine(CastSpell());
        }

        if (Input.GetMouseButtonUp(0))
        {
            glyphsCast.Clear();
            lineRenderer.enabled = false;
        }

        if(Input.GetMouseButtonDown(1))
        {
            glyphsCast.Clear();
            Casting();
        }

        if (Input.GetMouseButtonUp(1))
        {
            GlyphsDisplay.SetActive(false);
            Time.timeScale = 1;
        }
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    private IEnumerator CastSpell()
    {
        if (glyphsCast.Count > 0)
        {
            string spellCode = "";
            for (int i = 0; i < glyphsCast.Count; i++)
            {
                spellCode += (int) glyphsCast[i];
            }
            
            print(spellCode);

            if(spellbook.Spells[currentElement].ContainsKey(spellCode))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    hit.point = new Vector3(hit.point.x, Player.SpellsOrigin.position.y, hit.point.z);

                    switch (spellbook.Spells[currentElement][spellCode].castingType)
                    {
                        case Spell.CastingType.Instant:
                        default:
                            spellbook.Spells[currentElement][spellCode].Cast(hit.point);
                            break;

                        case Spell.CastingType.Channel:
                            float channelTime = 0f;
                            while (Input.GetMouseButton(0) && channelTime < maxChannelTime)
                            {
                                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                                    hit.point = new Vector3(hit.point.x, Player.SpellsOrigin.position.y, hit.point.z);
                                spellbook.Spells[currentElement][spellCode].Cast(hit.point);
                                channelTime += ((ChanneledSpell)spellbook.Spells[currentElement][spellCode]).ChannelRefreshDelay;
                                yield return new WaitForSeconds(((ChanneledSpell)spellbook.Spells[currentElement][spellCode]).ChannelRefreshDelay);
                            }
                            break;

                        case Spell.CastingType.Casting:
                            castingBar.SetActive(true);
                            float castTime;
                            float fullDuration = castTime = ((CastSpell)spellbook.Spells[currentElement][spellCode]).CastingDuration;
                            Vector3 castingBarOriginalScale = castingBar.transform.localScale;
                            while (castTime > 0)
                            {
                                castTime -= 0.01f;
                                yield return new WaitForSeconds(0.01f);
                                castingBar.transform.localScale = castingBarOriginalScale * (castTime / fullDuration);
                            }
                            //ADD CONDITION TO BLOCK SPELLCASTING AND MOVEMENT WHILE CASTING

                            spellbook.Spells[currentElement][spellCode].Cast(hit.point);
                            castingBar.SetActive(false);
                            castingBar.transform.localScale = castingBarOriginalScale;
                            break;
                    }
                }
            }
            else
            {
                print("Wrong spell!");
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                hit.point = new Vector3(hit.point.x, Player.SpellsOrigin.position.y, hit.point.z);

                switch (spellbook.Spells[currentElement]["def"].castingType)
                {
                    case Spell.CastingType.Instant:
                    default:
                        spellbook.Spells[currentElement]["def"].Cast(hit.point);
                        break;

                    case Spell.CastingType.Channel:
                        float channelTime = 0f;
                        while (Input.GetMouseButton(0) && channelTime < maxChannelTime)
                        {
                            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                                hit.point = new Vector3(hit.point.x, Player.Entity.transform.position.y, hit.point.z);

                            spellbook.Spells[currentElement]["def"].Cast(hit.point);
                            channelTime += ((ChanneledSpell)spellbook.Spells[currentElement]["def"]).ChannelRefreshDelay;
                            yield return new WaitForSeconds(((ChanneledSpell)spellbook.Spells[currentElement]["def"]).ChannelRefreshDelay);
                        }
                        break;
                }
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

