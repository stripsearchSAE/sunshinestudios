﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.UIElements;

public class BaseController : MonoBehaviour
{
    public GameObject explorer;
    ExplorerMovementScript controlledExplorer;
    // public List<ExplorerMovementScript> controlled;


    void Start()
    {
        explorer = GameObject.Find("Explorer");
        controlledExplorer = explorer.GetComponent<ExplorerMovementScript>();
    }
    public void clickControl(RaycastHit hit)
    {

        NavMeshHit tempHit;
        NavMesh.SamplePosition(hit.point, out tempHit, 2.0f, NavMesh.AllAreas);

        float dist = Vector3.Distance(tempHit.position, explorer.transform.position);
        if (dist > controlledExplorer.maxDistancePerTurn)
        {
            StartCoroutine(controlledExplorer.Denial());
            return;
        }

        controlledExplorer.goMoving(tempHit.position);

        /*
                if (hit.collider.tag == "Explorer") // check to see if ray hit self 
                {
                    ExplorerMovementScript temp = hit.collider.GetComponent<ExplorerMovementScript>(); 
                    if (!controlled.Contains(temp))
                    {
                        controlled.Add(temp);
                        temp.isActive = true;
                    }
                    else
                    {
                        controlled.Remove(temp);
                        temp.isActive = false;
                    }
                } 

                else if (hit.collider.tag == "Moves")
                {
                    Moveable tempa = hit.collider.GetComponent<Moveable>();

                    if (controlled.Count == 0)
                    {
                        tempa.Move();
                    }

                    else
                    {
                        foreach (var explorer in controlled) 
                        {
                            //tell the explorer its on a moveable
                            explorer.onMoveable = true;
                            explorer.theMoveable = hit.collider.gameObject;
                            //tell the moveable which explorer it has
                            tempa.Enter(explorer);

                            //moves
                            NavMeshHit tempHit; 
                            NavMesh.SamplePosition(hit.point, out tempHit, 2.0f, NavMesh.AllAreas);
                            explorer.goMoving(tempHit.position);
                            controlled.Remove(explorer);
                        } 
                    }
                }

                else if (hit.collider.tag == "Walkable") // only proceeds if active an directed to a walkable surface 
                {


                    List<ExplorerMovementScript> temp = new List<ExplorerMovementScript>(controlled);
                    foreach (var explorer in temp)
                    {
                        if (explorer.onMoveable == true)
                        {
                         //goto its moveable, tell its inot on there anymore
                         explorer.onMoveable = false;
                         explorer.theMoveable.GetComponent<Moveable>().Exit(explorer);
                        }
                        NavMeshHit tempHit; 
                        NavMesh.SamplePosition(hit.point, out tempHit, 2.0f, NavMesh.AllAreas);

                        // josh is this in the correct spot?
                        float dist = Vector3.Distance(tempHit.position, explorer.transform.position);
                        if(dist > explorer.maxDistancePerTurn)
                        {
                            StartCoroutine(explorer.Denial());
                            controlled.Remove(explorer);
                            return;
                        } 
                        // debug.log(dist);
                        explorer.goMoving(tempHit.position);
                        controlled.Remove(explorer);
                    } 
                } 
                else
                { 
                    foreach (var explorer in controlled) 
                    { 
                        StartCoroutine(explorer.Denial());
                        controlled.Remove(explorer);
                    } 
                }*/
    }
}
