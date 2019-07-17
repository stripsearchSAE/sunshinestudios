﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseController : MonoBehaviour
{
    public List<ExplorerMovementScript> controlled; 
    public void clickControl(RaycastHit hit)
    {
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
        else if (hit.collider.tag == "Walkable") // only proceeds if active an directed to a walkable surface 
        {
            List<ExplorerMovementScript> temp = new List<ExplorerMovementScript>(controlled);
            foreach (var explorer in temp) 
            {
                NavMeshHit tempHit; 
                NavMesh.SamplePosition(hit.point, out tempHit, 2.0f, NavMesh.AllAreas);
                explorer.goMoving(tempHit.position);
                controlled.Remove(explorer);
            } 
        } 
        else
        { 
            foreach (var explorer in controlled) 
            { 
                StartCoroutine(explorer.Denial()); 
            } 
        }
    }
}