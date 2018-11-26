using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public CapsuleCollider playerCollider;
    public float life = 150;
    public Slider lifeSlider;
    public Text lifeTxt;

    Animator playerAnimator;

    private void Awake()
    {
        lifeSlider.value = life;
        lifeTxt.text = life.ToString();
    }

    // Use this for initialization
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        playerAnimator.SetTrigger("Impact");
        if (life <= 0)
        {
            DayNightManager.instance.isGameOver = true;
            life = 0;
            playerAnimator.SetTrigger("Die");
        }
        
        UpdateHealth();
    }

    void UpdateHealth()
    {
        lifeSlider.value = life;
        lifeTxt.text = life.ToString();
    }

    public void SetGameOver()
    {
        DayNightManager.instance.GameOver();
    }

    //Otherwise the player float when dies
    public void ModifyCollider()
    {
        playerCollider.height = 0;
        playerCollider.radius = 0.1f;
    }
}
