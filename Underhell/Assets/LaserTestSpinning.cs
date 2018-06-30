using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTestSpinning : MonoBehaviour {

    public bool Spin = false;
    public float RotationSpeed = 1f;

    // Update is called once per frame
    void FixedUpdate () {
        if (Spin)
            transform.Rotate(Vector3.up * RotationSpeed * Time.fixedDeltaTime);
	}
}
