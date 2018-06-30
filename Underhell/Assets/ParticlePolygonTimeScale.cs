using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticlePolygonTimeScale : MonoBehaviour {

    Scrollbar scrollbar;
	void Start () {
        scrollbar = GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = scrollbar.value;
	}
}
