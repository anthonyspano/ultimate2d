using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthSystem healthSystem;
    
    
    void Start()
    {
        healthSystem = new HealthSystem(100, 0);
        var healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        healthSystem.OnHealthChanged += OnDamage;
    }



    private void OnDamage(object sender, System.EventArgs e)
    {
        Debug.Log("enemy hit");
    }
}
