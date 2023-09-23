using System.Collections;
using System.Collections.Generic;
using Rogue.Spell;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Spell spell;
    private SpellGenerator spellGenerator;

    private int inputIndex;

    private bool canInterrupt;

    private bool checkFlip;

    public PlayerAttackState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName,
        Spell spell,
        CombatInputs input
    ) : base(player, stateMachine, playerData, animBoolName)
    {
        this.spell = spell;
        //TODO SpellGenerator
        //spellGenerator = spell.GetComponent<SpellGenerator>();

        inputIndex = (int)input;

        spell.OnUseInput += HandleUseInput;

        spell.EventHandler.OnEnableInterrupt += HandleEnableInterrupt;
        this.spell.EventHandler.OnFinish += HandleFinish;
        this.spell.EventHandler.OnFlipSetActive += HandleFlipSetActive;
    }

    private void HandleFlipSetActive(bool value)
    {
        checkFlip = value;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        var playerInputHandler = player.InputHandler;

        var xInput = playerInputHandler.NormInputX;
        var attackInputs = playerInputHandler.AttackInputs;

        spell.CurrentInput = attackInputs[inputIndex];

        if (checkFlip)
        {
            // TODO Core Movement
            //Movement.CheckIfShouldFlip(xInput);
        }

        if (!canInterrupt)
            return;

        if (xInput != 0 || attackInputs[0] || attackInputs[1])
        {
            isAbilityDone = true;
        }
    }
    private void HandleSpellGenerating()
    {
        stateMachine.ChangeState(player.IdleState);
    }

    public override void Enter()
    {
        base.Enter();

        spellGenerator.OnSpellGenerating += HandleSpellGenerating;

        checkFlip = true;
        canInterrupt = false;

        spell.Enter();
    }


    public override void Exit()
    {
        base.Exit();

        spellGenerator.OnSpellGenerating -= HandleSpellGenerating;

        spell.Exit();
    }

    public bool CanTransitionToAttackState() => spell.CanEnterAttack;

    private void HandleEnableInterrupt() => canInterrupt = true;

    private void HandleUseInput() => player.InputHandler.UseAttackInput(inputIndex);

    private void HandleFinish()
    {
        AnimationFinishTrigger();
        isAbilityDone = true;
    }
}