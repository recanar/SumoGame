using UnityEngine;

public class HitForce : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sumo"))//check collision with other sumos
        {
            
        }
    }
}
