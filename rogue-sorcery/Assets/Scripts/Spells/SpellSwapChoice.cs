using System;

namespace Rogue.Spell
{
    public class SpellSwapChoiceRequest
    {
        public SpellSwapChoice[] Choices { get; }
        public SpellDataSO NewSpellData { get; }
        public Action<SpellSwapChoice> Callback;

        public SpellSwapChoiceRequest(
            Action<SpellSwapChoice> callback,
            SpellSwapChoice[] choices,
            SpellDataSO newSpellData
        )
        {
            Callback = callback;
            Choices = choices;
            NewSpellData = newSpellData;
        }
    }

    public class SpellSwapChoice
    {
        public SpellDataSO SpellData { get; }
        public int Index { get; }

        public SpellSwapChoice(SpellDataSO spellData, int index)
        {
            SpellData = spellData;
            Index = index;
        }
    }
}