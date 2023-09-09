using UnityEngine;
using UnityEngine.InputSystem;

public class SpellHolder2 : MonoBehaviour
{
    [SerializeField] private Controller _controller = null;
    public Spell spell;


    private AbilityState state = AbilityState.READY;

    private void Awake()
    {
        _controller = GetComponent<Controller>();
    }
    // Update is called once per frame
    void Update()
    {
        if (spell != null)
        {
            switch (state)
            {
                case AbilityState.READY:
                    if (_controller.input.RetrieveCast2Input()) { state = spell.Cast(transform); }
                    break;
                case AbilityState.ACTIVE:
                    if (_controller.input.RetrieveCast2Input()) { state = spell.Act(transform); }
                    else { state = AbilityState.READY; }
                    break;
                case AbilityState.COOLDOWN:
                    state = spell.Cooldown(transform);
                    break;
            }
        }
    }
}
