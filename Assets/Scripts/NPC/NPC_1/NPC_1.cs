using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC_1 : Entity
{
    public DialogManager DialogManager { get => dialogManager; set => dialogManager = value; }
    public GameObject ChatCloud { get => chatCloud; set => chatCloud = value; }
    public GameObject DefaultCloud { get => defaultCloud; set => defaultCloud = value; }
    public TextMeshPro MessageText { get => messageText; set => messageText = value; }

    [SerializeField] private GameObject chatCloud;
    [SerializeField] private GameObject defaultCloud;
    [SerializeField] private TextMeshPro messageText;
    [SerializeField] private DialogManager dialogManager;


    public string defaultText;
    [TextArea]
    public string playerDetectedText;

    [TextArea]
    public string[] messages;

    public N1_IdleState IdleState { get; private set; }
    public N1_MoveState MoveState { get; private set; }
    public N1_PlayerDetectedState PlayerDetectedState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;

    public override void Awake()
    {
        base.Awake();
        IdleState = new N1_IdleState(stateMachine, this, "idle", idleStateData, this);
        MoveState = new N1_MoveState(stateMachine, this, "move", moveStateData, this);
        PlayerDetectedState = new N1_PlayerDetectedState(stateMachine, this, "idle", playerDetectedStateData, this);
    }

    private void Start()
    {
        stateMachine.Initialize(MoveState);
        chatCloud.SetActive(false);
        defaultCloud.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
        Debug.Log(p.transform.name + " entered me");
        }
    }

    public Player GetPlayer()
    {
        var hit = Physics2D.OverlapCircle(playerCheck.position, entityData.maxAgroDistance, entityData.whatIsPlayer);
        if (hit != null)
        {
            return hit.GetComponent<Player>();
        }
        return null;
    }

    public void RotateChat()
    {
        chatCloud.transform.Rotate(0, 180, 0);
    }
}
