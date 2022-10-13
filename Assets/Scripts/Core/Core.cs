using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement { get; private set; }
    public CollisionSenses CollisionSenses { get; private set; }
    public Combat Combat { get; private set; }
    public Stats Stats { get; private set; }
    public ParticleManager ParticleManager { get; private set; }
    private List<ILogicUpdate> components = new List<ILogicUpdate> ();

    private void Awake()
    {
        Combat = GetComponentInChildren<Combat>();
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Stats = GetComponentInChildren<Stats>();
        ParticleManager = GetComponentInChildren<ParticleManager>();

        if (!Movement || !CollisionSenses)
        {
            Debug.Log("no core component");
        }
    }

    public void LogicUpdate()
    {
        foreach (var component in components)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(ILogicUpdate comp)
    {
        if (!components.Contains(comp))
        {
            components.Add(comp);
        }
    }
}
