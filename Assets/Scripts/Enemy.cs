using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy FX Settings")]
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitFX;
    [SerializeField] AudioClip deathSoundFX;

    [Header("Point settings")]
    [SerializeField] int hitPoints = 1;
    [SerializeField] int pointsOnDestroy = 100;

    ScoreBoard scoreBoard;
    Transform parent;

    void Start()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;

        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindWithTag("Respawn").transform;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        PlayParticles(hitFX);
        scoreBoard.IncreaseScore(pointsOnDestroy);
        hitPoints--;
    }

    void KillEnemy()
    {
        gameObject.SetActive(false);
        PlayParticles(deathFX);
        Destroy(gameObject);
    }

    void PlayParticles(GameObject particle)
    {
        GameObject fx = Instantiate(particle, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
    }
}
