using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N1_IdleState : IdleState
{
    private NPC_1 npc;
    public N1_IdleState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_IdleState stateData, NPC_1 npc) 
        : base(stateMachine, entity, animBoolName, stateData)
    {
        this.npc = npc;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            npc.RotateChat();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(npc.PlayerDetectedState);
        }
        else if (isIdleTimeOver)
        {
            stateMachine.ChangeState(npc.MoveState);
        }
    }
}
