using System;
using System.Collections.Generic;
using System.Linq;
using Rogue.CoreSystem;
using Rogue.Spell.Components;
using UnityEngine;

namespace Rogue.Spell
{
    public class SpellGenerator : MonoBehaviour
    {
        public event Action OnSpellGenerating;

        [SerializeField] private Spell spell;
        [SerializeField] private CombatInputs combatInput;

        private List<SpellComponent> componentAlreadyOnSpell = new List<SpellComponent>();

        private List<SpellComponent> componentsAddedToSpell = new List<SpellComponent>();

        private List<Type> componentDependencies = new List<Type>();

        private Animator anim;

        private SpellInventory spellInventory;

        private void GenerateSpell(SpellDataSO data)
        {
            OnSpellGenerating?.Invoke();

            spell.SetData(data);

            if (data is null)
            {
                spell.SetCanEnterAttack(false);
                return;
            }

            componentAlreadyOnSpell.Clear();
            componentsAddedToSpell.Clear();
            componentDependencies.Clear();

            componentAlreadyOnSpell = GetComponents<SpellComponent>().ToList();

            componentDependencies = data.GetAllDependencies();

            foreach (var dependency in componentDependencies)
            {
                if (componentsAddedToSpell.FirstOrDefault(component => component.GetType() == dependency))
                    continue;

                var spellComponent =
                    componentAlreadyOnSpell.FirstOrDefault(component => component.GetType() == dependency);

                if (spellComponent == null)
                {
                    spellComponent = gameObject.AddComponent(dependency) as SpellComponent;
                }

                spellComponent.Init();

                componentsAddedToSpell.Add(spellComponent);
            }

            var componentsToRemove = componentAlreadyOnSpell.Except(componentsAddedToSpell);

            foreach (var spellComponent in componentsToRemove)
            {
                Destroy(spellComponent);
            }

            anim.runtimeAnimatorController = data.AnimatorController;

            spell.SetCanEnterAttack(true);
        }

        private void HandleSpellDataChanged(int inputIndex, SpellDataSO data)
        {
            if (inputIndex != (int)combatInput)
                return;

            GenerateSpell(data);
        }

        #region Plumbing

        private void Start()
        {
            spellInventory = spell.Core.GetCoreComponent<SpellInventory>();

            spellInventory.OnSpellDataChanged += HandleSpellDataChanged;

            anim = GetComponentInChildren<Animator>();

            if (spellInventory.TryGetSpell((int)combatInput, out var data))
            {
                GenerateSpell(data);
            }
        }

        private void OnDestroy()
        {
            spellInventory.OnSpellDataChanged -= HandleSpellDataChanged;
        }

        #endregion
    }
}