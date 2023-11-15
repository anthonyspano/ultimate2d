using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ShootPlayer : State
    {
        BlockBattleSystem bbs;
        public ShootPlayer(BlockBattleSystem BlockBattleSystem) : base(BlockBattleSystem)
        {
            bbs = BlockBattleSystem;
        }

        public override IEnumerator Start()
        {
            if(bbs.CanAttack)
            {
                // spawn bullet
                var bulletPrefab = Resources.Load("Bullet") as GameObject;
                var bulletClone = GameObject.Instantiate(bulletPrefab, bbs.transform.transform.position, Quaternion.identity);
                // gain invulnerability from bullet
                //Debug.Log(bs.Enemy.GetComponent<BoxCollider2D>());
                Physics2D.IgnoreCollision(bulletClone.GetComponent<BoxCollider2D>(), bbs.transform.GetComponent<BoxCollider2D>());
                // wait a couple seconds to fire
                yield return new WaitForSeconds(1.8f);		
                // add force
                Rigidbody2D bulletRB = bulletClone.GetComponent<Rigidbody2D>();
                Vector2 dir = PlayerManager.Instance.transform.position - bbs.transform.transform.position;
                bulletRB.AddForce(dir * 91f);

                // cd
                yield return new WaitForSeconds(4f);

                BlockBattleSystem.SetState(new PursuePlayer(BlockBattleSystem));
            }

            

        }
 
    }


}
