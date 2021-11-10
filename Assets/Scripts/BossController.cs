using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent NavMesh;
    [SerializeField] private List<GameObject> PatrolPoint;
    private GameObject CurrentDest;
    private float TimeIdle;

    private void Start()
    {
        CurrentDest = PatrolPoint[0];
        SetDest(CurrentDest.transform.position);
    }

    private void Update()
    {
        
    }

    private void CheckIfAtDest()
    {
        //distance
        //if (transform.position - NavMesh.destination)
    }


    private void SetDest(Vector3 newDest)
    {
        NavMesh.destination = newDest;
    }

    private void AtDest()
    {
        StartCoroutine(Stopping());
    }

    private IEnumerator Stopping()
    {
        yield return new WaitForSeconds(2f);
    }


}
