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

    // make position of beam be position of RotateAroundPlayer
    private void Update()
    {
        
        if(ultimateCharge.GetUlt() >= ultCost)
        {
            powerIcon.color = new Color(1,1,1, 1f);
        }
        else
            powerIcon.color = new Color(0,0,0, .80f);

        if(PlayerInput.Ultimate() && ultimateCharge.GetUlt() > ultCost)
        {
            Debug.Log("ultimate");
            ultimateCharge.AddUlt(-ultCost);
            //anim.SetBool("IsBeaming", true);
            anim.Play("BeamAttack");
            PlayerManager.Instance.CanMove = false;
            StartCoroutine("PushBack"); // being pushed back during the ultimate

            // do damage to area
            var hits = Physics2D.OverlapCircleAll(transform.position, range, PlayerManager.Instance.enemyLayerMask);
            foreach(var col in hits)
            {
                if(col.CompareTag("Projectile"))
                {
                    Destroy(col.gameObject);
                }
                if(col.CompareTag("BigCultist"))
                    col.gameObject.GetComponent<BossTakeDamage>().healthSystem.Damage(SpecialDamage);
            }

        
            

        }
    }

    private IEnumerator PushBack()
    {
        yield return null;
        while(anim.GetCurrentAnimatorStateInfo(0).IsName("BeamAttack"))
        {
            // push player back
            PlayerManager.Instance.PushBack();
            yield return null;
        }

        yield return null;
    }
    public void StopAnimation()
    {
        PlayerManager.Instance.CanMove = true;
        anim.SetBool("IsBeaming", false);
        anim.Play("Empty");

    }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(transform.position, range);
    // }
}

}