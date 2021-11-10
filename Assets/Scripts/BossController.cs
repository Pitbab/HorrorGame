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
    private int DestIndex = 0;
    private const float TimeIdle = 2.0f;

    private void Start()
    {
        CurrentDest = PatrolPoint[DestIndex];
        SetDest(CurrentDest.transform.position);
    }

    private void Update()
    {
        CheckIfAtDest();
    }

    private void CheckIfAtDest()
    {
        //distance

    }


    private void SetDest(Vector3 newDest)
    {
        NavMesh.destination = newDest;
    }

    private void AtDest()
    {
        if (DestIndex == PatrolPoint.Count - 1)
        {
            DestIndex = 0;
        }
        else
        {
            DestIndex++;
        }
        StartCoroutine(Stopping());

    }

    private IEnumerator Stopping()
    {
        yield return new WaitForSeconds(TimeIdle);
        CurrentDest = PatrolPoint[DestIndex];
        SetDest(CurrentDest.transform.position);
    }


}
