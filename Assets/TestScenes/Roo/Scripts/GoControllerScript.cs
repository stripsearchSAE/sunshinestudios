using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class GoControllerScript : BaseController
{
    LineRenderer _line;
    public GameObject explorer;
    float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        maxDistance = explorer.GetComponent<ExplorerMovementScript>().maxDistancePerTurn;
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
        _line.material.color = Color.red;


        //transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote); 
        RaycastHit hit; 
        bool hitSomething = false; 
         
        hitSomething = Physics.Raycast(transform.position, transform.forward, out hit, 100f); 
     
        if (!hitSomething) return;
        Debug.Log("got parsed hit somehting");

        if(Vector3.Distance(explorer.transform.position, hit.transform.position) < maxDistance && hit.collider.tag == "Walkable") _line.material.color = Color.green;


        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            clickControl(hit);
        } 
    } 
}
