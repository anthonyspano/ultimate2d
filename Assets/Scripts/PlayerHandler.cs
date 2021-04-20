using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    
    private HealthSystem pHealth;
    public HealthBar healthBar;


    private void Start()
    {
        pHealth = new HealthSystem(100, 1f);
        healthBar.Setup(pHealth);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            pHealth.Damage(20);
    }


}
