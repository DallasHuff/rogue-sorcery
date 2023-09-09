using UnityEngine;

public class Spell : ScriptableObject
{
    [SerializeField]
    public Element ele { get; private set; }
    public string spellName;
    public string description;
    public float baseCooldownTime;
    public float cooldownTime;
    public float currCD;
    public float activeTime;
    private float currActiveTime;
    public float knockbackForce;

    public virtual AbilityState Cast(Transform playerTrans) { return AbilityState.READY; }

    public virtual AbilityState Act(Transform playerTrans) { return AbilityState.COOLDOWN; }

    public virtual AbilityState Cooldown(Transform playerTrans)
    {
        if (currCD > 0)
        {
            currCD -= Time.deltaTime;
            return AbilityState.COOLDOWN;
        }
        else
        {
            return AbilityState.READY;
        }
    }
}
