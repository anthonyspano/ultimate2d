using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{

    // Camera transform
    private Transform transform;

    private float shakeDuration = 0f;

    private float shakeMagnitude = 0.3f;
    
    // how fast the shake goes away
    private float dampingSpeed = 2.0f;

    private Vector3 initialPosition;

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
        
        initialPosition = transform.position;

    }

    private void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = (Vector3)(initialPosition + Random.insideUnitSphere * shakeMagnitude);
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float d)
    {
        // set damping speed on call
        shakeMagnitude = d;
        
        shakeDuration = 0.6f;
    }
}
