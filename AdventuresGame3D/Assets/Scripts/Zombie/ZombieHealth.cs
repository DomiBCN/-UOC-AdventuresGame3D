using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public float life = 150;
    public ZombieAI zombie;
    public CapsuleCollider zombieCollider;

    AudioSource zombieAudioSource;
    
    Animator zombieAnimator;

    // Use this for initialization
    void Start()
    {
        zombie = GetComponent<ZombieAI>();
        zombieAudioSource = zombie.zombieAudioSource;
        zombieAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        zombieAnimator.SetTrigger("Impact");

        if (life <= 0)
        {
            life = 0;
            zombieAnimator.SetTrigger("Die");
            zombieAudioSource.clip = zombie.zombieDieClip;
            zombieAudioSource.Play();
        }
    }

    public void DestroyZombie()
    {
        Destroy(gameObject, 0.8f);
    }

    //Otherwise the zombie float when dies
    public void ModifyCollider()
    {
        zombieCollider.height = 0;
        zombieCollider.radius = 0.1f;
    }
}
