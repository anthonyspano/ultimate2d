using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// old?
namespace com.ultimate2d.combat
{
[RequireComponent(typeof(PlayerAim))]
public class PlayerAttack : MonoBehaviour
{
    // temp
    private UltimateAim ultAim;
    
    // hitbox
    public float radius;
    public LayerMask enemyLayer;
    public float range;
    private Vector2 lastMove;

    // debug
    private Color hitboxColor;

    // ultimate
    private UltimateBar ultBar;
    [Tooltip("This charges the ultimate bar")]
    public int ultChargeAmt;

    // animation
    private Animator anim;
    public string AtkAnimName;
    
    [SerializeField] public float cooldownRate;
    private float cooldownTimer;
    //[SerializeField] public float CooldownTimer { get; set; }
    //[SerializeField] public float CooldownRate { get; set; }

    // Player Aim
    private PlayerAim myAim;

    public int bossDamage;

    public Text playerCDText;

    private void Start()
    {
        // hitbox
        lastMove = new Vector2(0,1);
        myAim = GetComponent<PlayerAim>();
        ultAim = GetComponent<UltimateAim>();
        //ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>(); // not implemented in scene
        
        anim = GetComponent<Animator>();
        cooldownTimer = cooldownRate;
    }

    private void Update()
    {
        if (PlayerInput.Slash() && IsReady())
        {
            StartCoroutine(Strike(AtkAnimName, 20));
        }

        cooldownTimer -= Time.deltaTime;
        playerCDText.text = cooldownTimer.ToString();
    }

    private bool IsReady() => cooldownTimer <= 0;

    public IEnumerator Strike(string stateName, int ultChargeAmt)
    {
        // cooldown
        cooldownTimer = cooldownRate;
        
        yield return new WaitForSeconds(0.2f);

        // play anim
        anim.Play(stateName);
        
        // hitbox
        //var center = transform.XandY() + lastMove.normalized * range;
        var hits = Physics2D.OverlapCircleAll(myAim.center, radius, enemyLayer);
        foreach (var col in hits)
        {
            // remember to check tags and layer!!
            //Debug.Log(col.gameObject.tag);
            // if (col.gameObject.CompareTag("Enemy"))
            // {
            //     col.gameObject.GetComponent<EnemyManager>().hSystem.Damage(20);
            //     //ultBar.AddUlt(ultChargeAmt);
            //     hitboxColor = Color.green;
            // }

            col.gameObject.GetComponent<EnemyTakeDamage>().healthSystem.Damage(70);

        }
        

    }
    
    public void EndAnim()
    {
        anim.Play("Neutral");
    }


}

}