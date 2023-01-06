using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Changes the size of the health bar sprite
public class HealthBar : MonoBehaviour
{

    public HealthSystem healthSystem;

    public float invulnTimer;
    private bool once;

    public Slider healthBarGUI;


    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged; // Event in the health system
        once = true;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
		//Vector3 healthBarSize = transform.Find("Bar").localScale;
        //transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1, 0);
        healthBarGUI.value = healthSystem.GetHealthPercent();
        healthSystem.SetInvulnerability(true);

        
    }
	

    private void Update()
    {
        if (healthSystem.GetInvulnerability())
        {
            if (once)
            {
                invulnTimer = healthSystem.GetInvulnTime();
                once = false;
            }
            else
                invulnTimer -= Time.deltaTime;
        }

        if (invulnTimer <= 0)
        {
            healthSystem.SetInvulnerability(false);
            once = true;
        }

    }


}