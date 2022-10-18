using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N1_MoveState : MoveState
{
    private NPC_1 npc;
    public N1_MoveState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, D_MoveState stateData, NPC_1 npc) 
        : base(stateMachine, entity, animBoolName, stateData)
    {
        this.npc = npc;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(npc.PlayerDetectedState);
        }

        else if (isDetectiongWall || !isDetectiongLedge)
        {
            npc.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(npc.IdleState);
        }
    }
}
