using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private string Player = "Player";
    private bool IsActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Player))
        {
            Debug.Log("Trapped");
            if (!IsActivated)
            {
                BasicMovement Movement = other.gameObject.GetComponent<BasicMovement>();
                Movement.GetStagger();
                IsActivated = true;
            }
        }
    }
    
}
