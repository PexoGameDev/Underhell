using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour {

    RaycastHit hit;

	void Update () {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            hit.point = new Vector3(hit.point.x, Player.Entity.transform.position.y, hit.point.z);
        transform.LookAt(hit.point);
    }
}
