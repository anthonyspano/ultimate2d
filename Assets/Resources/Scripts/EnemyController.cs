using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
	public Transform pfHealthBar;
	
	private HealthSystem hSystem;
	public HealthBar hBar;

    // Decision Tree
	public enum CurrentState {Moving, Doing, Shooting};
	public CurrentState myState;
	
	public void setState (CurrentState state)
	{ 
		myState = state;
	}
	
	
	EnemyMove _Move;
	Shoot _Shoot;
		
	
    void Start()
    {
		// Health
        hSystem = new HealthSystem(100, 0f);
        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(transform.position.x, transform.position.y + 0.2f), Quaternion.identity, transform);
		hBar = healthBarTransform.GetComponent<HealthBar>();
		hBar.Setup(hSystem);
		
		// // Decision Tree
		// myState = CurrentState.Moving;
		
		// // Move
		// _Move = GetComponent<EnemyMove>();
		// // Shoot
		// _Shoot = GetComponent<Shoot>();
    }

    // private void Update()
    // {
		// Health
        // if (hSystem.GetHealth() == 0)
            // Destroy(gameObject);
			
		// Decision Tree
		// if(myState == CurrentState.Moving)
		// {
			// cut off mass update calls to function
			// myState = CurrentState.Doing;
			// StartCoroutine(_Move.FinishMove());	
		// }

		// if(myState == CurrentState.Shooting)
		// {
			// cut off mass calls
			// myState = CurrentState.Doing;
			// _Shoot.FireProjectile();
			
		// }
    // }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
		
        if (collision.gameObject.CompareTag("Player"))
        {
            hSystem.Damage(20);
        }
    }
}
