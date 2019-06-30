using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovementScript : MonoBehaviour
{
    private NavMeshAgent _explorer; 
    private bool _isActive = false; // change by clicking on self. only active navagents will move
    private Renderer _rend;

    // Start is called before the first frame update
    void Start()
    {
        _explorer = GetComponent<NavMeshAgent>();
        _rend = GetComponent<Renderer>();
        _rend.material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // set ray from camera to mouse position

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject == this.gameObject) // check to see if ray hit self
                {
                    _isActive = !_isActive; // swap to opposite bool value
                    if (_isActive) { _rend.material.color = Color.green; }
                    else
                    {
                        _rend.material.color = Color.black;
                        _explorer.isStopped = true;
                    }
                        

                }
                else
                    if (_isActive && hit.collider.tag != "Explorer") { _explorer.destination = hit.point; }
                
            }
        }
    }
}
