using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollector : MonoBehaviour
{
    private float weight;
    private Rigidbody rb;
    [SerializeField] private float foodValue;

    private delegate void FoodCollection();
    private FoodCollection _foodCollection;

    private void Start()
    {
        _foodCollection += FoodCollected;
        rb = GetComponent<Rigidbody>();
        if(TryGetComponent(out PlayerController playerController))//if this is player itself add score update for UI
        {
            _foodCollection += UpdateScoreWithFood;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            other.gameObject.SetActive(false);//close food on collect
            if(_foodCollection != null)
            {
                _foodCollection();
            }
        }
    }

    private void FoodCollected()
    {
        rb.mass += foodValue;//increase mass and scale on collect food 
        transform.localScale = Vector3.one* rb.mass;
    }
    private void UpdateScoreWithFood()
    {
        GameManager.Instance.AddScore(100);
    }
}
