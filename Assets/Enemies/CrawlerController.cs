using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// event driven ai script as the alternative to the behavior tree

public class CrawlerController : MonoBehaviour
{
    private enum ExecuteAction { GetCover,
                                 PursuePlayer,
                                 Patrol,
                                 Attack};

    private ExecuteAction crawlerAction = ExecuteAction.Patrol;
    
    private void Update()
    {
        /*
        switch(crawlerAction)
        {
            case ExecuteAction.GetCover:
                // get cover
                Debug.Log("here");
            break;

            case ExecuteAction.PursuePlayer:
                // get into attack range
                Debug.Log("getting into position");
            break;

            case ExecuteAction.Patrol:
                // patrol between two points
                Debug.Log("patrolling");
            break;

            case ExecuteAction.Attack:
                // commit to attack on player
                Debug.Log("attacking");
            break;

            default:
                Debug.Log("enum null: no actions taken");
            break;

        }

        */


    }







}
