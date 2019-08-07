﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplorerEndSequence : MonoBehaviour
{
    public delegate void FinalEvent();
    public static event FinalEvent StartFinalSequence;

    public Transform endPosition;
    private ExplorerMovementScript Movement;
    public GameObject Explorer1;
   // public GameObject Explorer2;
    public GameObject Platform;
    bool Final = false;

    public float distanceCheck = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

        Movement = Explorer1.GetComponent<ExplorerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Explorer1.transform.position, endPosition.position) < distanceCheck && !Final)
        {
            Final = true;
            StartCoroutine(LastPoint());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explorer"))
        {
            NavMeshAgent Expl = Movement.explorer;
            Expl.destination = endPosition.position;
           // StartCoroutine(LastPoint(Expl));
        }

    }

    IEnumerator LastPoint()
    {

            Debug.Log("Howdy");
            Explorer1.GetComponent<NavMeshAgent>().enabled = false;
            Explorer1.transform.parent = Platform.transform;
           // Explorer2.GetComponent<ExplorerMovementScript>().enabled = false;
           // Explorer2.transform.parent = Platform.transform;
           if (StartFinalSequence != null) StartFinalSequence();

        yield return null;
    }
}