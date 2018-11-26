using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackEventHandler : MonoBehaviour
{

    public BoxCollider swordCollider;
    public ParticleSystem bloodParticlesPrefab;

    [Header("Audio")]
    public AudioSource paladinSwordAudioSource;
    public AudioClip cutZombie;

    [Header("Damage")]
    public float headDamage = 50;
    public float bodyDamage = 20;

    bool hitClipPlayed;

    public void SwordDamage()
    {
        hitClipPlayed = false;
        Collider[] cols = Physics.OverlapBox(swordCollider.bounds.center, swordCollider.bounds.extents, swordCollider.transform.rotation);

        foreach (var col in cols)
        {
            if (col.transform.root.tag == "Zombie")
            {
                if (col.name == "Head")
                {
                    col.GetComponentInParent<ZombieHealth>().TakeDamage(headDamage);
                    AddBlood(col);
                    CutZombieAudio();
                }
                else if (col.name == "Spine")
                {
                    col.GetComponentInParent<ZombieHealth>().TakeDamage(bodyDamage);
                    AddBlood(col);
                    CutZombieAudio();
                }

            }
        }
    }

    void AddBlood(Collider col)
    {
        ParticleSystem bloodParticles = GameObject.Instantiate(bloodParticlesPrefab, col.transform.position, col.transform.rotation);
        Destroy(bloodParticles, 2);
    }

    void CutZombieAudio()
    {
        hitClipPlayed = true;
        paladinSwordAudioSource.clip = cutZombie;
        paladinSwordAudioSource.Play();
    }
}
