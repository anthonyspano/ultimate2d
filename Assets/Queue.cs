using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.ultimate2d.combat
{
    public class Queue : MonoBehaviour
    {

        private List<string> myQueue;
        private bool isBusy;
        private EnemyManager _enemyManager;

        void Start()
        {
            myQueue = new List<string>();
            _enemyManager = GetComponent<EnemyManager>();
        }
        


        // void Update()
        // {
            
        //     if(!isBusy && myQueue.Count > 0)
        //     {
        //         StartCoroutine(myQueue[1]);
        //     }
        //     else if (!isBusy)
        //     {
        //         //Debug.Log("waiting");
        //     }

        //     if(PlayerIsInRange(_enemyManager.pursuitRange))
        //     {
        //         isBusy = true;
        //         myQueue.Add("Pursue");

        //     }
            
            
            
            
        //     if(myQueue.Count > 0)
        //         Debug.Log(myQueue.Count);

        // }


        IEnumerator Pursue()
        {


            Debug.Log("Pursuing");
            yield return new WaitForSeconds(1.5f);
            Debug.Log("Pursued");
            myQueue.Remove("Pursue");
        }

        public bool PlayerIsInRange(float range)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, _enemyManager.enemyLayerMask);
            if(colliders.Length > 0) 
                return true;
            
            return false;
        }
    }
}