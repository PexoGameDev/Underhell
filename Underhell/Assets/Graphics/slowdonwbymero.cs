
using UnityEngine;

public class slowdonwbymero : MonoBehaviour {

	public float slowdonwFactor = 0.05f;
	
	void Start () 
	{
		Time.timeScale = slowdonwFactor;
		Time.fixedDeltaTime = Time.timeScale * .02f;
	}
	
	
}

