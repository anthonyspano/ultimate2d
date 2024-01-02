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

    // sound
    private AudioSource audioSource;
    public AudioClip soundEffect;


    private void Start()
    {
        anim = GetComponent<Animator>();
        ultimateCharge = PlayerManager.Instance.GetComponent<UltimateBar>();
        powerIcon.color = new Color(0,0,0, .80f);

        audioSource = GetComponent<AudioSource>();
        
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
            //Debug.Log("ultimate");
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

    }

    private void LateUpdate()
    {

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {

            // get the angle of stick input
            var stickAngle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            var vector1 = transform.position - PlayerManager.Instance.transform.position;
            vector1.Normalize();
            //Debug.Log(vector1);
            //var currentAngle = Mathf.Atan(vector1.y/vector1.x) * Mathf.Rad2Deg;
            //angle += Time.deltaTime * RotationSpeed;
            
            // convert Unity angles to range [0, 2pi]
            if(angle < 0)
            {
                angle = NegToPosRad(angle);
            }
            if(stickAngle < 0)
            {
                stickAngle = NegToPosRad(stickAngle);
            }
            else if(stickAngle == 0)
                stickAngle = Mathf.PI * 2;

            // if the difference between the angle and the stick angle > pi, adjust input angle
            if(Mathf.Abs(angle - stickAngle) > Mathf.PI)
                stickAngle -= (Mathf.PI * 2);


            angle = Mathf.Lerp(angle, stickAngle, 0.01f); // 0.01f
            //Debug.Log("ca: " + angle * Mathf.Rad2Deg + ", sa: " + stickAngle * Mathf.Rad2Deg);
            // convert appropriately
            if(angle > Mathf.PI * 2)
                angle = angle - (Mathf.PI * 2);
            angle = PosToNegRad(angle);

            // set the position of the beam object to the new angle
            positionOffset.Set(Mathf.Cos(angle) * CircleRadius, Mathf.Sin(angle) * CircleRadius, ElevationOffset);
            transform.position = positionOffset + PlayerManager.Instance.transform.position;
            

            // rotation
            // rotate that vector by 90 degrees around the Z axis
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * (transform.position - PlayerManager.Instance.transform.position);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        }



    }

    private float NegToPosRad(float a)
    {
        return Mathf.PI - Mathf.Abs(a) + Mathf.PI;
    }
    
    private float PosToNegRad(float a)
    {
        return a - Mathf.PI * 2;
    }

    public void BeamSetup()
    {
        // set position and rotation same as cursor
        var reticle = GameObject.Find("Reticle");
        var r_vector = reticle.transform.position - PlayerManager.Instance.transform.position;
        r_vector.Normalize();
        angle = Mathf.Atan2(r_vector.y, r_vector.x);
        r_vector *= 5;
        transform.position = reticle.transform.position + r_vector;
        transform.rotation = reticle.transform.rotation;

        // play beam sound
        audioSource.PlayOneShot(soundEffect);
        
        
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