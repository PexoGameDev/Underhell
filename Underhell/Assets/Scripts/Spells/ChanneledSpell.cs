using UnityEngine;
public abstract class ChanneledSpell : Spell {
    public float ChannelRefreshDelay;

    public abstract void ChannelEnd(Vector3 target);
}
