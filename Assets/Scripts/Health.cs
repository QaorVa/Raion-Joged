using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Health : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;

    public float health = 100f;
    public float maxHP = 100f;

    public static Health instance;

    public bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        health = health / 100f;
        maxHP = maxHP / 100f;

        FunctionPeriodic.Create(() => {
            if(health >= 0f)
            {

                healthBar.SetSize(health);

                if(health > .6f)
                {
                    healthBar.SetColor(Color.green);
                } else if(health <= .6f && health > .3f)
                {
                    healthBar.SetColor(Color.yellow);
                } else if(health <= .3f)
                {
                    healthBar.SetColor(Color.red);
                }
            }
            if(health < 0.01f)
            {
                health = 0;
                alive = false;
            }
        }, .03f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if(alive)
        {
            damage = damage / 100f;
            health -= damage;
        }
        
    }

    public void Heal(float heal)
    {
        if(alive)
        {
            if(health < maxHP)
            {
                heal = heal / 100f;
                health += heal;

                if(health > maxHP)
                {
                    health = maxHP;
                }
            }
            
        }
        
    }
}
