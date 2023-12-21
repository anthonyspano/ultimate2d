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


    // circling player
    private Vector3 positionOffset;
    [Range(0, 360)]
    private float angle = 0;
    public float CircleRadius;
    public float ElevationOffset = 0;
    public float RotationSpeed = 1;

    private float cA = 0f;
    private float sA = 90f;



    private void Start()
    {
        anim = GetComponent<Animator>();
        ultimateCharge = PlayerManager.Instance.GetComponent<UltimateBar>();
        powerIcon.color = new Color(0,0,0, .80f);
        //Debug.Log(Mathf.Lerp(16.42f, 90, 0.1f));
        //Debug.Log(Mathf.Cos(9));
        
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
            BeamSetup();
            anim.Play("BeamAttack");
            PlayerManager.Instance.CanMove = false;
            StartCoroutine("PushBack"); // being pushed back during the ultimate

            // do damage to area
            // var hits = Physics2D.OverlapCircleAll(transform.position, range, PlayerManager.Instance.enemyLayerMask);
            // foreach(var col in hits)
            // {
            //     if(col.CompareTag("Projectile"))
            //     {
            //         Destroy(col.gameObject);
            //     }
            //     if(col.CompareTag("BigCultist"))
            //         col.gameObject.GetComponent<BossTakeDamage>().healthSystem.Damage(SpecialDamage);
            // }       
        }
        // if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        // {
        //     // move in a circle towards input of joystick, considering rotation
        //     // h,k is the position of the player
        //     var h = PlayerManager.Instance.transform.position.x;
        //     var k = PlayerManager.Instance.transform.position.y;
        //     // r is the radius of the circle
        //     var r = 1;



        // }
    }

    private void LateUpdate()
    {
        // Debug.Log(cA);
        // Debug.Log(sA);
        // cA = Mathf.Lerp(cA, sA, 0.1f);
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            
            Debug.Log(angle);
            //angle *= Mathf.PI / 180;
            positionOffset.Set(Mathf.Cos(angle) * CircleRadius, Mathf.Sin(angle) * CircleRadius, ElevationOffset);
            Debug.Log(positionOffset);
            transform.position = positionOffset + PlayerManager.Instance.transform.position;
            // TBI: angle approaches angle of input
            var stickAngle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
            Debug.Log(stickAngle);
            // vector in relation to the player (local position)
            var vector1 = transform.position - PlayerManager.Instance.transform.position;
            vector1.Normalize();
            //Debug.Log(vector1);
            //var currentAngle = Mathf.Atan(vector1.y/vector1.x) * Mathf.Rad2Deg;
            //angle += Time.deltaTime * RotationSpeed;
            // lerp? the angle to approach the stick angle
            //Debug.Log(currentAngle);
            //angle = currentAngle;
            angle = Mathf.Lerp(angle, stickAngle, 0.1f);
            //angle = 135;
            

            // rotation
            // rotate that vector by 90 degrees around the Z axis
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * (transform.position - PlayerManager.Instance.transform.position);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        }



    }

    public void BeamSetup()
    {
        // set position and rotation same as cursor
        var reticle = GameObject.Find("Reticle");
        transform.position = reticle.transform.position;
        transform.rotation = reticle.transform.rotation;
        
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