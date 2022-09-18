using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform targetTransform;
    private Rigidbody rb;
    [SerializeField] private float speed;
    private bool isStunned;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(GetClosestSumo),0f,1f);//check closest sumo every second
    }
    private void Update()
    {
        transform.LookAt(targetTransform);//look to target
        CheckFallPlatform();
    }

    private void FixedUpdate()
    {
        if (isStunned)
        {
            return;
        }
        rb.AddForce(transform.forward*speed,ForceMode.Acceleration);
    }
    private void GetClosestSumo()
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(var sumo in GameManager.Instance.sumos)//get all sumos' transform from manager and find  the closest one 
        {
            if (sumo==gameObject || !sumo.activeSelf) continue;//if sumo same with this object or disabled sumo go next step
            Transform potentialTarget = sumo.transform;
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        if (bestTarget==null)
        {
            return;
        }//if have dont have new target dont change old target
        targetTransform=bestTarget;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sumo"))//stun sumo when collides with another sumo
        {
            isStunned = true;
            Invoke(nameof(ExitStun),1f);
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRigidbody.AddForce(awayFromPlayer * 7, ForceMode.Impulse);
        }
    }

    private void ExitStun()
    {
        isStunned = false;
    }
    private void CheckFallPlatform()
    {
        if (transform.position.y<-3)
        {
            gameObject.SetActive(false);
            GameManager.Instance.ReduceSumoCount();//reduce sumo count
        }
    }
}
