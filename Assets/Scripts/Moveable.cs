using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Moveable : MonoBehaviour
{
    
    //the movable object
    
    public List<GameObject> controlledHere; //if the controlled objects are on the box
    public GameObject destination;  //the end point of the platform
    public Vector3 end; //the end for the explorerers - possibly temp

    void Start()
    {
        controlledHere = new List<GameObject>();
        end = new Vector3(destination.transform.position.x, destination.transform.position.y + 2, destination.transform.position.z);
    }
    
    
    

    public void Enter(ExplorerMovementScript other)
    {
        Debug.Log("Added");
        controlledHere.Add(other.gameObject);
    }

    public void Exit(ExplorerMovementScript other)
    {
        Debug.Log("left");
        controlledHere.Remove(other.gameObject);
    }


    public void Move()
    {
        foreach (var explorer in controlledHere)
        {
            Debug.Log("moved");
            NavMeshAgent agent = explorer.GetComponent<NavMeshAgent>();
            agent.Warp(end);
        }
    }
}
