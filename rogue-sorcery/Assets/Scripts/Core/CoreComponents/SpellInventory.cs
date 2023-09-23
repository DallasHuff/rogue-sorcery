using System;
using Rogue.Spell;
using UnityEngine;

namespace Rogue.CoreSystem
{
    public class SpellInventory : CoreComponent
    {
        public event Action<int, SpellDataSO> OnSpellDataChanged;

        [field: SerializeField] public SpellDataSO[] spellData { get; private set; }

        public bool TrySetSpell(SpellDataSO newData, int index, out SpellDataSO oldData)
        {
            if (index >= spellData.Length)
            {
                oldData = null;
                return false;
            }

            oldData = spellData[index];
            spellData[index] = newData;

            OnSpellDataChanged?.Invoke(index, newData);

            return true;
        }

        public bool TryGetSpell(int index, out SpellDataSO data)
        {
            if (index >= spellData.Length)
            {
                data = null;
                return false;
            }

            data = spellData[index];
            return true;
        }

        public bool TryGetEmptyIndex(out int index)
        {
            for (var i = 0; i < spellData.Length; i++)
            {
                if (spellData[i] is not null)
                    continue;

                index = i;
                return true;
            }

            index = -1;
            return false;
        }

        public SpellSwapChoice[] GetSpellSwapChoices()
        {
            var choices = new SpellSwapChoice[spellData.Length];

            for (var i = 0; i < spellData.Length; i++)
            {
                var data = spellData[i];

                choices[i] = new SpellSwapChoice(data, i);
            }

            return choices;
        }
    }
}