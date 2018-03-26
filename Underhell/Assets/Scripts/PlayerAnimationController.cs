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

    public static void CrossfadeAnimation(string animationName, float delay)
    {
        Animator.CrossFade(animationName,delay);
    }
    public static void SetBool(string valueName, bool value)
    {
        animator.SetBool(valueName, value);
    }
    public static bool GetBool(string valueName)
    {
        return Animator.GetBool(valueName);
    }
    public static void SetAnimationSpeed(float speed)
    {
        Animator.speed = speed;
    }
    #endregion

    #region Private Methods
    #endregion
}

