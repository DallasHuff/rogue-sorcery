using System;
using Rogue.Interaction;
using Rogue.Interaction.Interactables;
using Rogue.Spell;

namespace Rogue.CoreSystem
{
    public class SpellSwap : CoreComponent
    {
        public event Action<SpellSwapChoiceRequest> OnChoiceRequested;
        public event Action<SpellDataSO> OnSpellDiscarded;

        private InteractableDetector interactableDetector;
        private SpellInventory spellInventory;

        private SpellDataSO newSpellData;

        private SpellPickup spellPickup;

        private void HandleTryInteract(IInteractable interactable)
        {
            if (interactable is not SpellPickup pickup)
                return;

            spellPickup = pickup;

            newSpellData = spellPickup.GetContext();

            if (spellInventory.TryGetEmptyIndex(out var index))
            {
                spellInventory.TrySetSpell(newSpellData, index, out _);
                interactable.Interact();
                newSpellData = null;
                return;
            }

            OnChoiceRequested?.Invoke(new SpellSwapChoiceRequest(
                HandleSpellSwapChoice,
                spellInventory.GetSpellSwapChoices(),
                newSpellData
            ));
        }

        private void HandleSpellSwapChoice(SpellSwapChoice choice)
        {
            if (!spellInventory.TrySetSpell(newSpellData, choice.Index, out var oldData))
                return;

            newSpellData = null;

            OnSpellDiscarded?.Invoke(oldData);

            if (spellPickup is null)
                return;

            spellPickup.Interact();

        }

        protected override void Awake()
        {
            base.Awake();

            interactableDetector = core.GetCoreComponent<InteractableDetector>();
            spellInventory = core.GetCoreComponent<SpellInventory>();
        }

        private void OnEnable()
        {
            interactableDetector.OnTryInteract += HandleTryInteract;
        }


        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryInteract;
        }
    }
}