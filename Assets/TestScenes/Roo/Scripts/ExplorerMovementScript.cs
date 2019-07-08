using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class ExplorerMovementScript : MonoBehaviour
{
    public enum states { IDLE, MOVING};
    public states ExplorerStates;

    private NavMeshAgent _explorer;
    private bool _isActive = false; // change by clicking on self. only active navagents will move
    public Renderer _rend;

    public Rigidbody Rigidbody; // ridgid body of explorer
    private Transform _target;
    public float ReachedStartPointDistance = 0.5f;
    public Transform DummyAgent;
    public Vector3 EndJumpPosition;
    public float MaxJumpableDistance = 80f;
    public float JumpTime = 0.6f;
    public float AddToJumpHeight;

    Transform _dummyAgent;
    public Vector3 JumpStartPoint;
    Vector3 JumpMidPoint;
    Vector3 JumpEndPoint;
    bool checkForStartPointReached;
    Transform _transform;
    List<Vector3> Path = new List<Vector3>();
    float JumpDistance;
    Vector3[] _jumpPath;
    bool previousRigidBodyState;
    private bool _isTravelling = false;
    private bool _isEnabled = true; // set this to false to stop player clicking on explorer to active ie. during an animation.
    private Vector3 _hit; // fill this with oculus pointer hit

    public float explorerStoppingDistance;

    public float explorerTimeOut = 5;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _explorer = GetComponent<NavMeshAgent>();
        _rend.material.color = Color.black;
        _explorer.stoppingDistance = explorerStoppingDistance;
        ExplorerStates = states.IDLE;
    }

    private void Update()
    {
        switch (ExplorerStates)
        {

            case states.IDLE:

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // set ray from camera to mouse position

                RaycastHit hit;
                NavMeshHit navHit;

                bool hitSomething = Physics.Raycast(ray, out hit, 100f);
                if (!hitSomething) return;

                bool blocked = NavMesh.Raycast(transform.position, hit.point, out navHit, NavMesh.AllAreas);
                Debug.DrawLine(transform.position, hit.point, blocked ? Color.red : Color.green);

                if (Input.GetMouseButtonDown(0))
                {
                    if (hitSomething && _isEnabled)
                    {
                        if (hit.collider.gameObject == this.gameObject) // check to see if ray hit self
                        {
                            _isActive = !_isActive; // swap to opposite bool value
                            return;
                        }
                        else if (_isActive && hit.collider.tag == "Walkable") // only proceeds if active an directed to a walkable surface
                        {
                            _isTravelling = true;
                            _explorer.isStopped = false;
                            checkForStartPointReached = false;
                            previousRigidBodyState = false;
                            _dummyAgent = null;
                            _hit = hit.point;

                            Path.Clear();

                            ExplorerStates = states.MOVING;
                            return;
                        }
                        else if(_isActive && hit.collider.tag != "Explorer")
                        {
                            StartCoroutine(Denial());
                            return;
                        }

                    }
                }

                break;

            case states.MOVING:
                GetStartPointAndMoveToPosition();

                if (_isTravelling)
                {
                    float distance = Vector3.Distance(transform.position, _hit);

                    if (distance < 1f) // add timeout for explorers later
                    {
                        _explorer.isStopped = true;
                        _isTravelling = false;
                        _isActive = false;
                        ExplorerStates = states.IDLE;
                    }
                }

                break;
        }


        if (checkForStartPointReached)
        {
            var distance = (_transform.position - JumpStartPoint).sqrMagnitude; // originally _tranform.position

            if (distance <= ReachedStartPointDistance * ReachedStartPointDistance)
            {
                ReadyToJump();

                if (_explorer.isOnNavMesh)
                {
                    _explorer.isStopped = true;
                }

                checkForStartPointReached = false;
            }
        }

        if (_isActive) { _rend.material.color = Color.green; }
        else
        {
            _rend.material.color = Color.black;
            _explorer.isStopped = true;
            _isTravelling = false;
        }

    }

    public void GetStartPointAndMoveToPosition()
    {
        JumpStartPoint = GetJumpStartPoint();
        MoveToStartPoint();
    }

    public void PerformJump()
    {
        SpawnAgentAndGetPoint();
    }

    void MoveToStartPoint()
    {

        checkForStartPointReached = true;
        _explorer.isStopped = false;
        _explorer.SetDestination(JumpStartPoint);

    }

    void ReadyToJump()
    {
        float distance = Vector3.Distance(transform.position, _hit);

        if (distance < 1.1f)
        {
            _explorer.isStopped = true;
            _isTravelling = false;
            return;
        }
        //Do your pre_jump animation
        PerformJump();
    }

    void SpawnAgentAndGetPoint()
    {
        // If using Pooling Spawn here instead
        //_dummyAgent = Instantiate(DummyAgent, Target.transform.position, Quaternion.identity);
        _dummyAgent = Instantiate(DummyAgent, _hit, Quaternion.identity);
        var info = _dummyAgent.GetComponent<ReturnNavmeshInfo>();
        EndJumpPosition = info.ReturnClosestPointBackToAgent(transform.position);
        JumpEndPoint = EndJumpPosition;

        MakeJumpPath();

    }

    void MakeJumpPath()
    {
        Path.Add(JumpStartPoint);

        var tempMid = Vector3.Lerp(JumpStartPoint, JumpEndPoint, 0.5f);
        tempMid.y = tempMid.y + _explorer.height + AddToJumpHeight;

        Path.Add(tempMid);

        Path.Add(JumpEndPoint);

        JumpDistance = Vector3.Distance(JumpStartPoint, JumpEndPoint);
        Debug.Log(JumpDistance);

        if (JumpDistance <= MaxJumpableDistance)
        {
            DoJump();
        }
        else
        {
            Debug.Log("Too far to jump");

            _explorer.isStopped = true;
            _isTravelling = false;
            _isActive = false;
            ExplorerStates = states.IDLE;
            // can put a denial animation here
        }
    }

    void DoJump()
    {
        previousRigidBodyState = Rigidbody.isKinematic;
        _explorer.enabled = false;
        Rigidbody.isKinematic = true;

        _jumpPath = Path.ToArray();

        Rigidbody.DOLocalPath(_jumpPath, JumpTime, PathType.CatmullRom).OnComplete(JumpFinished);
    }

    void JumpFinished()
    {
        _explorer.enabled = true;
        Rigidbody.isKinematic = previousRigidBodyState;
        _explorer.destination = _hit;

        // If using Pooling DeSpawn here instead
        Destroy(_dummyAgent.gameObject);
    }

    private void OnEnable()
    {
        checkForStartPointReached = false;
        _transform = transform;
    }

    Vector3 GetJumpStartPoint()
    {
        NavMeshPath hostAgentPath = new NavMeshPath();
       
        _explorer.CalculatePath(_hit, hostAgentPath);
        var endPointIndex = hostAgentPath.corners.Length - 1;
        return hostAgentPath.corners[endPointIndex];

        // Improvement to make- get the jump distance using the start and end point
        // use that to set the Jump Time
    }

    IEnumerator Denial()
    {
        _isEnabled = false;
        _isActive = false;
        // put denial sound and animation here
        yield return new WaitForSeconds(1f);
        _isEnabled = true;
        ExplorerStates = states.IDLE;
    }

}
