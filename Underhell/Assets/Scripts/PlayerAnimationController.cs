using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    #region Variables
    // Fields //

    private static Animator animator;
    // Public Properties //

    public static Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }


    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake () {
        animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	void Update () {
		
	}
    #endregion

    #region Public Methods
    public static void PlayAnimation(string animationName)
    {
        Animator.Play(animationName);
    }
    #endregion

    #region Private Methods
    #endregion
}

