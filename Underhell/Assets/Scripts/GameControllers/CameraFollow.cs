using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    #region Variables
    [SerializeField] private Vector3 offest;
    [SerializeField] private float cameraSpeed = 1f;
    private GameObject player;
    // Fields //

    // Public Properties //

    // Private Properties //
    #endregion

    #region Unity Methods
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position,player.transform.position + offest,cameraSpeed);
	}
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}

