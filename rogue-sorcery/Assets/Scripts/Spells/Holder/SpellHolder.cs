using UnityEngine;

public class SpellHolder : MonoBehaviour
{
    public KeyCode hotkey;
    public Spell spell;

    private AbilityState state = AbilityState.READY;

    // Update is called once per frame
    void Update()
    {
        if (spell != null)
        {
            switch (state)
            {
                case AbilityState.READY:
                    if (Input.GetButton("Fire1")) { state = spell.Cast(transform); }
                    break;
                case AbilityState.ACTIVE:
                    if (Input.GetButton("Fire1")) { state = spell.Act(transform); }
                    else { state = AbilityState.READY; }
                    break;
                case AbilityState.COOLDOWN:
                    state = spell.Cooldown(transform);
                    break;
            }
        }
    }
}
