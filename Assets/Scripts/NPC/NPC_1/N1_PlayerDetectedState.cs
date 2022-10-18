using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N1_PlayerDetectedState : PlayerDetectedState
{
    private NPC_1 npc;
    private Player player;

    public N1_PlayerDetectedState(FiniteStateMachine stateMachine, Entity entity, string animBoolName, 
        D_PlayerDetectedState stateData, NPC_1 npc) 
        : base(stateMachine, entity, animBoolName, stateData)
    {
        this.npc = npc;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        npc.MessageText.text = npc.playerDetectedText;
        npc.DefaultCloud.SetActive(false);
        npc.ChatCloud.SetActive(true);
        core.Movement.SetVelocityY(10);
        player = npc.GetPlayer();
        Debug.Log(player.transform.name);
    }

    public override void Exit()
    {
        base.Exit();
        npc.ChatCloud.SetActive(false);
        npc.DefaultCloud.SetActive(true);
        npc.MessageText.text = npc.defaultText;
        player = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (player.InputHandler.TriggerDialog)
        {
            Debug.Log("trigger dialog");
            npc.DialogManager.TriggerDialog(npc.MessageText,player, npc.messages, npc.messages.Length);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(npc.IdleState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
