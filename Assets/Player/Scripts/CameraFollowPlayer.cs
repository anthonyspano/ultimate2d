using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    //public float followSpeed;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        
        
        // if(Vector2.Distance(transform.position, PlayerManager.Instance.transform.position) > 2f)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerManager.Instance.transform.position.x, PlayerManager.Instance.transform.position.y, transform.position.z), followSpeed * Time.deltaTime);
        // }
    }
}
