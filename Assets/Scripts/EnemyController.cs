using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HealthBar hBar;
    public HealthSystem hSystem;

    // Start is called before the first frame update
    void Start()
    {
        hSystem = new HealthSystem(100, 0f);
        hBar.Setup(hSystem);
    }

    private void Update()
    {
        if (hSystem.GetHealth() == 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hSystem.Damage(20);
            
                


        }
    }
}
