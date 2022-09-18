using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnDisable()
    {
        Invoke(nameof(EnableFood),5f);
    }

    private void EnableFood()//enable food after 5 secs when its collected
    {
        gameObject.SetActive(true);
    }
}
