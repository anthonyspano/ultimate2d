using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float followSpeed;
    
    void Update()
    {
        if(Vector2.Distance(transform.position, PlayerManager.Instance.transform.position) > 2f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.Instance.transform.position.x, PlayerManager.Instance.transform.position.y, transform.position.z), followSpeed * Time.deltaTime);
        }
    }
}
