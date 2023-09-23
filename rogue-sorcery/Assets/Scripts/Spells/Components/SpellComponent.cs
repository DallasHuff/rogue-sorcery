using Rogue.CoreSystem;
using UnityEngine;

namespace Rogue.Spell.Components
{
    public abstract class SpellComponent : MonoBehaviour
    {
        protected Spell spell;

        protected AnimationEventHandler AnimationEventHandler => spell.EventHandler;
        protected Core Core => spell.Core;
        protected float attackStartTime => spell.AttackStartTime;

        protected bool isAttackActive;

        public virtual void Init()
        {

        }

        protected virtual void Awake()
        {
            spell = GetComponent<Spell>();
        }

        protected virtual void Start()
        {
            spell.OnEnter += HandleEnter;
            spell.OnExit += HandleExit;
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnDestroy()
        {
            spell.OnEnter -= HandleEnter;
            spell.OnExit -= HandleExit;
        }
    }

    public abstract class SpellComponent<T1, T2> : SpellComponent where T1 : ComponentData<T2> where T2 : AttackData
    {
        protected T1 data;
        protected T2 currentAttackData;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentAttackData = data.GetAttackData(spell.CurrentAttackCounter);
        }

        public override void Init()
        {
            base.Init();

            data = spell.Data.GetData<T1>();
        }
    }
}