using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    #region Variables
    // Fields //
    private static Animator animator;
    private static Dictionary<string,AnimationClip> animationClips;
    // Public Properties //

    public static Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }

    public static Dictionary<string, AnimationClip> AnimationClips
    {
        get { return animationClips; }
        private set { animationClips = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake () {
        animator = gameObject.GetComponentInChildren<Animator>();
    }
    void Start()
    {
        AnimationClips = new Dictionary<string, AnimationClip>();
        foreach (AnimationClip ac in Animator.runtimeAnimatorController.animationClips)
            if (!AnimationClips.ContainsKey(ac.name))
                AnimationClips.Add(ac.name, ac);
    }
    #endregion

    #region Public Methods
    public static void PlayAnimation(string animationName, int layer = 0)
    {
        Animator.Play(animationName, layer);
    }

    public static void CrossfadeAnimation(string animationName, float delay, int layer = 0)
    {
        Animator.CrossFade(animationName,delay, layer);
    }

    public static void SetBool(string valueName, bool value)
    {
        animator.SetBool(valueName, value);
    }

    public static void SetFloat(string valueName, float value)
    {
        animator.SetFloat(valueName, value);
    }

    public static bool GetBool(string valueName)
    {
        return Animator.GetBool(valueName);
    }

    public static void SetAnimationSpeed(float speed)
    {
        Animator.speed = speed;
    }

    public static bool IsAnimationName(int layer, string name)
    {
        return Animator.GetCurrentAnimatorStateInfo(layer).IsName(name);
    }
    #endregion

    #region Private Methods
    #endregion
}

