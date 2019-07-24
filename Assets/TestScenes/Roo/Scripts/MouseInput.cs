using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.AI; 
 
public class MouseInput : BaseController
{ 
    // Update is called once per frame 
    void Update() 
    { 
        RaycastHit hit; 
        //NavMeshHit navHit; 
        bool hitSomething = false; 
         
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // set ray from camera to mouse position 
        hitSomething = Physics.Raycast(ray, out hit, 100f); 
     
        if (!hitSomething) return; 
     
        //bool blocked = NavMesh.Raycast(transform.position, hit.point, out navHit, NavMesh.AllAreas); 
        //Debug.DrawLine(transform.position, hit.point, blocked ? Color.red : Color.green); 
         
         
        if (Input.GetMouseButtonDown(0)) 
        { 
            clickControl(hit);
        }
    } 
} 
