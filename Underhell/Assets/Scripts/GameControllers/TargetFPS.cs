using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFPS : MonoBehaviour {

    [SerializeField] private int targetFPS = 120;

    void Awake () {
        Application.targetFrameRate = targetFPS;
    }
}
