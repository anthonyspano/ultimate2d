using System;

// Health system
public class HealthSystem
{
    public int healthMax;
    private int health;
    private float invulnTime;
    private bool invulnerability;

    public event EventHandler OnHealthChanged;

    public HealthSystem(int healthMax, float invulnTime)
    {
        // set health
        this.healthMax = healthMax;
        health = healthMax;
        // set invulnerability timer
        this.invulnTime = invulnTime;
        invulnerability = false;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        // if invuln is false, take damage
        if (invulnerability == false)
            health -= damageAmount;
        if (health < 0) health = 0;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
    }

    public float GetInvulnTime()
    {
        return invulnTime;
    }

    public bool SetInvulnerability(bool invuln)
    {
        invulnerability = invuln;
        return invulnerability;
    }

    public bool GetInvulnerability()
    {
        return invulnerability;
    }

}