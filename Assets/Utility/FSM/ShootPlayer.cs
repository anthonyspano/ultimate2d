using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ShootPlayer : State
    {
        BattleSystem bs;
        public ShootPlayer(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
        }

        public override IEnumerator Start()
        {
            // shoot
            // spawn bullet
            var bulletPrefab = Resources.Load("Bullet") as GameObject;
            var bulletClone = GameObject.Instantiate(bulletPrefab, bs.Enemy.transform.position, Quaternion.identity);
            // gain invulnerability from bullet
            //Debug.Log(bs.Enemy.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(bulletClone.GetComponent<BoxCollider2D>(), bs.Enemy.GetComponent<BoxCollider2D>());		
            // add force
            Rigidbody2D bulletRB = bulletClone.GetComponent<Rigidbody2D>();
            Vector2 dir = PlayerManager.Instance.transform.position - bs.Enemy.transform.position;
            bulletRB.AddForce(dir * 100f);

            // cd
            yield return new WaitForSeconds(5f);

            BattleSystem.SetState(new Begin(BattleSystem));
        }
 
    }


}
