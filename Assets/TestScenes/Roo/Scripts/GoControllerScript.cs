using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class GoControllerScript : BaseController
{
    public GameObject GoController; 
    LineRenderer _line;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool controllerCheck = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote) || OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);

        if (!controllerCheck)
        {
            _line.enabled = false;
            return;
        }
        _line.enabled = true;
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        
        RaycastHit hit; 
        NavMeshHit navHit; 
        bool hitSomething = false; 
         
        hitSomething = Physics.Raycast(GoController.transform.position, GoController.transform.forward, out hit, 100f); 
     
        if (!hitSomething) return; 
     
        bool blocked = NavMesh.Raycast(transform.position, hit.point, out navHit, NavMesh.AllAreas); 
        Debug.DrawLine(transform.position, hit.point, blocked ? Color.red : Color.green); 
         
         
     
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            clickControl(hit);
        } 
    } 
}
