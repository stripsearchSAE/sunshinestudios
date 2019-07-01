using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class AgentJumpToTarget : MonoBehaviour
{   
    private NavMeshAgent _explorer;
    private bool _isActive = false; // change by clicking on self. only active navagents will move
    public Renderer _rend;

    public Rigidbody Rigidbody;
    //public GameObject Target;
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

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        _explorer = GetComponent<NavMeshAgent>();
        // _rend = GetComponent<Renderer>();
        _rend.material.color = Color.black;
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

    private void OnEnable()
    {
        checkForStartPointReached = false;
        _transform = transform;
    }

    Vector3 GetJumpStartPoint()
    {
        NavMeshPath hostAgentPath = new NavMeshPath();
        //_explorer.CalculatePath(Target.transform.position, hostAgentPath);
        _explorer.CalculatePath(hit.point, hostAgentPath);
        var endPointIndex = hostAgentPath.corners.Length - 1;
        return hostAgentPath.corners[endPointIndex];

        //Improvement to make- get the jump distance using the start and end point
        // use that to set the Jump Time
    }

    void MoveToStartPoint()
    {
        checkForStartPointReached = true;
        _explorer.isStopped = false;
        _explorer.SetDestination(JumpStartPoint);
    }

    void ReadyToJump()
    {
        //Do your pre_jump animation
        PerformJump();
    }

    void SpawnAgentAndGetPoint()
    {
        // If using Pooling Spawn here instead
        //_dummyAgent = Instantiate(DummyAgent, Target.transform.position, Quaternion.identity);
        _dummyAgent = Instantiate(DummyAgent, hit.point, Quaternion.identity);
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

        if (JumpDistance <= MaxJumpableDistance)
        {
            DoJump();
        }
        else
        {
            Debug.Log("Too far to jump");
        }
    }

    void DoJump()
    {
        previousRigidBodyState = Rigidbody.isKinematic;
        _explorer.enabled = false;
        Rigidbody.isKinematic = true;

        _jumpPath = Path.ToArray();

        // if you don't want to use a RigidBody change this to
        //transform.DoLocalPath per the DoTween doc's
        Rigidbody.DOLocalPath(_jumpPath, JumpTime, PathType.CatmullRom).OnComplete(JumpFinished);
    }

    void JumpFinished()
    {
        _explorer.enabled = true;
        Rigidbody.isKinematic = previousRigidBodyState;
        _explorer.destination = hit.point;

        // If using Pooling DeSpawn here instead
        Destroy(_dummyAgent.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
         {
             GetStartPointAndMoveToPosition();
         }

         if (Input.GetKeyDown(KeyCode.J))
         {
             PerformJump();
         }
         

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // set ray from camera to mouse position

            //RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100f))
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
                        if (_isActive && hit.collider.tag != "Explorer")
                    {
                        _explorer.isStopped = false;
                        //_explorer.destination = hit.point;
                        // Target.transform.position = hit.point;
                        GetStartPointAndMoveToPosition();
                    }

                }
            }
        

        if (checkForStartPointReached)
        {
            var distance = (_transform.position - JumpStartPoint).sqrMagnitude;

            if (distance <= ReachedStartPointDistance * ReachedStartPointDistance)
            {
                ReadyToJump();

                if(_explorer.isOnNavMesh)
                {
                    _explorer.isStopped = true;
                }
               
                checkForStartPointReached = false;               
            }           
        }
    }
}
