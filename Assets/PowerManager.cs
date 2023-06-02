using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public float range;
    public int SpecialDamage;
    private Animator anim;
    private UltimateBar ultimateCharge;
    public int ultCost;


    private void Start()
    {
        anim = GetComponent<Animator>();
        ultimateCharge = PlayerManager.Instance.GetComponent<UltimateBar>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && ultimateCharge.GetUlt() > ultCost)
        {
            ultimateCharge.AddUlt(-ultCost);
            anim.SetBool("IsExploding", true);

            // do damage to area
            var hits = Physics2D.OverlapCircleAll(transform.position, range, PlayerManager.Instance.enemyLayerMask);
            foreach(var col in hits)
            {
                if(col.CompareTag("Projectile"))
                {
                    Destroy(col.gameObject);
                }
                if(col.CompareTag("Enemy"))
                    col.gameObject.GetComponent<EnemyTakeDamage>().healthSystem.Damage(SpecialDamage);
            }
        }
    }

    public void StopAnimation()
    {
        anim.SetBool("IsExploding", false);
    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(transform.position, range);
    // }
}
