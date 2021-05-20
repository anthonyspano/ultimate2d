using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// barely works xD
public class AIScript : MonoBehaviour
{
    public float sightRange;
    public float attackRange;
    private float chargeTime;

    private GameObject player;

    private bool doneCharging;
    private SpriteRenderer sr;
    private Vector3 setPos;
    private GameObject targetPlace;
    private bool targetChosen;
    private float coolDown;
    private bool Moving;
    private bool Attacking;
    private GameObject bc;

    public float attackRate;
    public GameObject target;
    private GameObject targetPrefab;
    public float moveSpeed;
    public float chargeSpeed;
    private float invulnTimer;
	
	// health
    public HealthBar hBar;
    public HealthSystem hSystem;
    private int maxHealth;
    public int criticalHP;
	


    private void OnDrawGizmos()
    {
        // Draw circles around unit to show range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // Start is called before the first frame update
    void Start()
    {
        bc = transform.Find("Hitbox").gameObject;
        bc.SetActive(false);
        coolDown = attackRate;
        doneCharging = false;

        sr = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player");

		// health
        hSystem = new HealthSystem(100, 0f);
        hBar.Setup(hSystem);
		Debug.Log(hSystem.GetHealth());
		
		// resources
		targetPrefab = Resources.Load<GameObject>("Prefabs/target");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Insight: " + InSight(target));
        //Debug.Log("InRange: " + InRange(target));

        // If no health, die
        //if(hSystem.GetHealth() == 0)
        // {
            // Destroy(gameObject);
        // }



        if (!InRange(target))
        {
            Attacking = false;
        }

        if (coolDown != 0)
        {
            coolDown -= Time.deltaTime;
        }

        // Default
        if (!InSight(target))
        {
            Moving = false;
            Attacking = false;
        }

        // Vision
        if (InSight(target) && !InRange(target))
        {
            Moving = true;
            Attacking = false;
            Move();
        }

        // Attack Range
        if (InRange(target) && coolDown <= 0)
        {
            Moving = false;
            Attacking = true;
            ChargeAttack();
            StartCoroutine(Attack(setPos));
        }
        else
            Attacking = false;

        // if(IsLowHealth() && InRange(target))
        // {
            // // move until !InRange

        // }

        // if(IsLowHealth() && !InRange(target))
        // {
            // // Recover

            // // Shoot
        // }

    }


    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    void ChargeAttack()
    {
        // Indicator
        sr.color = Color.red;

        if(InRange(target))
            setPos = LockOn(target.transform.position);
    }

    IEnumerator Attack(Vector3 lockOnPos)
    {
        bc.SetActive(true);
        // Performs a charging attack
        // move to player
        transform.position = Vector3.MoveTowards(transform.position, lockOnPos, chargeSpeed * Time.deltaTime);
        //Debug.Log("Me: " + transform.position);
        //Debug.Log(lockOnPos);

        if (transform.position == lockOnPos)
        {
            targetChosen = false;
            Destroy(targetPlace);
            coolDown = attackRate;
            bc.SetActive(false);
        }
        yield return (transform.position == lockOnPos || Attacking == false);

    }

    void Flee()
    {

    }

    void Recover()
    {

    }

    void Shoot()
    {

    }

    bool InSight(GameObject target)
    {
        float dX = transform.position.x - target.transform.position.x;
        float dY = transform.position.y - target.transform.position.y;

        //Debug.Log("dX: " + dX + " dY: " + dY);
        //Debug.Log("sightRange: " + sightRange);
        if (dX < sightRange && dY < sightRange)
        {
            return true;
        }

        else
            return false;
    }

    bool InRange(GameObject target)
    {
        float dX = transform.position.x - target.transform.position.x;
        float dY = transform.position.y - target.transform.position.y;

        if (dX < attackRange && dY < attackRange)
        {
            return true;
        }

        else
            return false;
    }

    // bool IsLowHealth()
    // {
        // if (myHealth.GetHealthPercent() < criticalHP)
        // {
            // return true;
        // }

        // else
            // return false;
    // }


    private Vector3 LockOn(Vector3 pos)
    {
        if (!targetChosen)
        {
            // Create a placeholder for target position to charge to
            targetPlace = Instantiate(targetPrefab, pos, Quaternion.identity);

            targetChosen = true;
        }
        return targetPlace.transform.position;
    }
}
