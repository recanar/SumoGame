using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private Rigidbody rb;
    public DynamicJoystick dynamicJoystick;
    public bool isStunned;

    private void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MovePlayer();
        CheckFallPlatform();
    }

    private void FixedUpdate()
    {
        if (isStunned)
        {
            return;
        }
        rb.AddForce(transform.forward*speed,ForceMode.Acceleration);//move with physic
    }

    private void MovePlayer()
    {
        float horizontalInput = dynamicJoystick.Horizontal;//get input from joystick
        float verticalInput = dynamicJoystick.Vertical;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();//movement vector
        if (movementDirection != Vector3.zero) //look direction of move  
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);         
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sumo"))//stun sumo when collides with another sumo
        {
            isStunned = true;
            Invoke(nameof(ExitStun),1f);
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRigidbody.AddForce(awayFromPlayer * 3, ForceMode.Impulse);
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
