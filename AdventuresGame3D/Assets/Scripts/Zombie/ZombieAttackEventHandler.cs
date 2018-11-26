using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackEventHandler : MonoBehaviour
{

    public Transform zombieClaw;

    [Header("Damage")]
    public float headDamage = 20;
    public float bodyDamage = 10;

    LayerMask mask;
    PlayerHealth playerHealth;
    float totalDamage = 0;

    private void Start()
    {
        mask = LayerMask.GetMask("Player");
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    public void ClawAttackDamage()
    {
        totalDamage = 0;
        Collider[] cols = Physics.OverlapSphere(zombieClaw.position, 0.25f, mask);
        
        foreach (var col in cols)
        {
            if (col.name == "Head")
            {
                totalDamage += headDamage;
            }
            else if(col.name == "Spine")
            {
                totalDamage += bodyDamage;
            }
            
        }
        if(totalDamage > 0)
        {
            playerHealth.TakeDamage(totalDamage);
        }
    }
}
