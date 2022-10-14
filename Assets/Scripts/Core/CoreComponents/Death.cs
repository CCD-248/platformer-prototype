using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] GameObject[] deathParticles;

    private void Start()
    {
        core.Stats.onHealthZero += Die;
    }

    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            core.ParticleManager.StartParticles(particle);
        }
        core.transform.parent.gameObject.SetActive(false);
    }
}
