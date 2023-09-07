using UnityEngine;

public class SpellHolder : MonoBehaviour
{
    public KeyCode hotkey;
    public Spell spell;
    [SerializeField] private Transform playerTrans;

    private AbilityState state = AbilityState.READY;

    // Update is called once per frame
    void Update()
    {
        if (spell != null)
        {
            switch (state)
            {
                case AbilityState.READY:
                    if (Input.GetKeyDown(hotkey)) { state = spell.Cast(playerTrans); }
                    break;
                case AbilityState.ACTIVE:
                    if (Input.GetKey(hotkey)) { state = spell.Act(playerTrans); }
                    else { state = AbilityState.READY; }
                    break;
                case AbilityState.COOLDOWN:
                    state = spell.Cooldown(playerTrans);
                    break;
            }
        }
    }
}
