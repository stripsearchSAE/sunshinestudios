using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using FMOD.Studio;

public class ExplorerMovementScript : MonoBehaviour
{
    /*
    [Header("States")]
    public ExplorerIdleState Idle;
    public ExplorerMovingState Moving;
    */
    public bool isActive = false; // change by clicking on self. only active navagents will move - obsolete
    public bool isTravelling = false;
    public bool isJumping = false;
    public bool hasLanded = false;
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
    public float maxDistancePerTurn = 10f;
    public float waitForDenialAnimation = 0.5f;

    public bool onMoveable = false;
    public GameObject theMoveable;

    [Header("feedback")]
    public Animator animatingModel;
    PlayerAudio AudioPlayer;

    private bool jumpSoundEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        explorer = GetComponent<NavMeshAgent>();
        // _rend.material.color = Color.black;
        explorer.stoppingDistance = explorerStoppingDistance;
        AudioPlayer = GetComponent<PlayerAudio>();
        animatingModel = GetComponent<Animator>();
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

        if (animatingModel)
        {
            animatingModel.SetBool("Moving",isTravelling);
            animatingModel.SetBool("Jumping",isJumping);
            animatingModel.SetBool("Landing",hasLanded);
            if (hasLanded) hasLanded = false;
        }
    
        if(explorer.isOnOffMeshLink && jumpSoundEnabled)
        {
            jumpSoundEnabled = false;
            AudioPlayer.PlayVoice("Jump");
            StartCoroutine(ResetJumpSound());
        }
        /*
        if (Vector3.Distance(transform.position, explorer.destination) < endPointTolerance)
        {
            explorer.isStopped=true;
        }
        
        if (isActive) { _rend.material.color = Color.green; }
        else
        {
            _rend.material.color = Color.black;
            explorer.isStopped = true;
            isTravelling = false;
        } */
    }

    public IEnumerator Denial()
    {
        _isEnabled = false;
        // put denial sound and animation here
        AudioPlayer.PlayVoice("Denial");

        yield return new WaitForSeconds(waitForDenialAnimation);
        _isEnabled = true;

        //goIdle();
    }

    public IEnumerator ResetJumpSound()
    {
        yield return new WaitForSeconds(1f);
        jumpSoundEnabled = true;
    }

}
