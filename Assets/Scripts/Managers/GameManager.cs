using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;
    private float respawnStartTime;
    private CinemachineVirtualCamera cameraVar;
    private Player playerScript;

    private bool respawn;

    private void Start()
    {
        var b = true;
        cameraVar = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        playerScript = player.GetComponent<Player>();
        //playerScript.Core.Stats.onHealthZero += Respawn;
        while (b)
        {
            try
            {
                playerScript.Core.Stats.onHealthZero += Respawn;
                b = false;
            }
            catch
            {
                b = true;
            }
        }

    }

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnStartTime = Time.time;
        respawn = true;
        Debug.Log("Respawn");
    }

    private void CheckRespawn()
    {
        if (Time.time > respawnStartTime + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, spawnPos);
            cameraVar.m_Follow = playerTemp.transform;
            respawn = false;
        }
    }
}
