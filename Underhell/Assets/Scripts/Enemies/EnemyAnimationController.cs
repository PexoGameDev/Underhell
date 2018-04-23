using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {
    #region Variables
    // Fields //
    private Animator animator;
    private Dictionary<string, AnimationClip> animationClips;
    // Public Properties //

    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }

    public Dictionary<string, AnimationClip> AnimationClips
    {
        get { return animationClips; }
        private set { animationClips = value; }
    }
    // Private Properties //
    #endregion

    #region Unity Methods
    void Awake()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        AnimationClips = new Dictionary<string, AnimationClip>();

        foreach (AnimationClip ac in Animator.runtimeAnimatorController.animationClips)
            if (!AnimationClips.ContainsKey(ac.name))
                AnimationClips.Add(ac.name, ac);
    }
    #endregion

    #region Public Methods
    public void PlayAnimation(string animationName, int layer = 0)
    {
        Animator.Play(animationName, layer);
    }

    public void CrossfadeAnimation(string animationName, float delay, int layer = 0)
    {
        Animator.CrossFade(animationName, delay, layer);
    }

    public void SetBool(string valueName, bool value)
    {
        animator.SetBool(valueName, value);
    }

    public void SetFloat(string valueName, float value)
    {
        animator.SetFloat(valueName, value);
    }

    public bool GetBool(string valueName)
    {
        return Animator.GetBool(valueName);
    }

    public float GetFloat(string valueName)
    {
        return Animator.GetFloat(valueName);
    }

    public void SetAnimationSpeed(float speed)
    {
        Animator.speed = speed;
    }

    public bool IsAnimationName(int layer, string name)
    {
        return Animator.GetCurrentAnimatorStateInfo(layer).IsName(name);
    }
    #endregion

    #region Private Methods
    #endregion
}

