using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float maxHeath;
    private float currentHeath;

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHeath = maxHeath;
    }

    public void DecreaseHealth(float damage)
    {
        currentHeath -= damage;

        if (currentHeath <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        gameManager.Respawn();
        Destroy(gameObject);
    }
}
