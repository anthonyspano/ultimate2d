using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.ultimate2d.combat
{
public class PowerManager : MonoBehaviour
{
    public float range;
    public int SpecialDamage;
    private Animator anim;
    private UltimateBar ultimateCharge;
    public int ultCost;
    public Image powerIcon;


    private void Start()
    {
        anim = GetComponent<Animator>();
        ultimateCharge = PlayerManager.Instance.GetComponent<UltimateBar>();
        powerIcon.color = new Color(0,0,0, .80f);
    }

    private void Update()
    {
        
        if(ultimateCharge.GetUlt() >= ultCost)
        {
            Debug.Log("ready");
            powerIcon.color = new Color(1,1,1, 1f);
        }
        else
            powerIcon.color = new Color(0,0,0, .80f);

        if(PlayerInput.Ultimate() && ultimateCharge.GetUlt() > ultCost)
        {
            Debug.Log("ultimate");
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

}