using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovementScript : MonoBehaviour
{
    private NavMeshAgent _explorer;

    private bool _isActive = false;
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    _isActive = !_isActive;
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
