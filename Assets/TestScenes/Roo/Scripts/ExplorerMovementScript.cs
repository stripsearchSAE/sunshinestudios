using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class ExplorerMovementScript : MonoBehaviour
{
    /*
    [Header("States")]
    public ExplorerIdleState Idle;
    public ExplorerMovingState Moving;
    */
    public bool isActive = false; // change by clicking on self. only active navagents will move
    public bool isTravelling = false;
    private bool _isEnabled = true; // set this to false to stop player clicking on explorer to active ie. during an animation.
    public float explorerTimeOut = 5;
    public Renderer _rend;

    [Header("purely old jump method related")]
    private Transform _target;
    public float ReachedStartPointDistance = 0.5f;
    public float AddToJumpHeight;
    
    [Header("Navmesh Agent")]
    public NavMeshAgent explorer;
    public float explorerStoppingDistance;
    public float endPointTolerance = 0.2f;

    public bool onMoveable = false;
    public GameObject theMoveable;
    
    
    // Start is called before the first frame update
    void Start()
    {
        explorer = GetComponent<NavMeshAgent>();
        _rend.material.color = Color.black;
        explorer.stoppingDistance = explorerStoppingDistance;
    }

    public void goMoving(Vector3 target)
    {
        isTravelling = true;
        explorer.isStopped = false; 
        explorer.SetDestination(target);
    }
    public void goIdle()
    {
        isActive = false;
        isTravelling = false;
        explorer.isStopped = true; 
    }

    private void Update()
    {
        /*
        if (checkForStartPointReached)
        {
            var distance = (_transform.position - JumpStartPoint).sqrMagnitude; // originally _tranform.position

            if (distance <= ReachedStartPointDistance * ReachedStartPointDistance)
            {
                checkForStartPointReached = false;
            }
        }
        */
        if (isTravelling && Vector3.Distance(transform.position, explorer.destination) < endPointTolerance)
        {
            goIdle();
        }

        if (isActive) { _rend.material.color = Color.green; }
        else
        {
            _rend.material.color = Color.black;
            explorer.isStopped = true;
            isTravelling = false;
        }

    }

    public IEnumerator Denial()
    {
        _isEnabled = false;
        isActive = false;
        // put denial sound and animation here
        yield return new WaitForSeconds(1f);
        _isEnabled = true;
    }

}
