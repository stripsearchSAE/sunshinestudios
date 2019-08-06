using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplorerEndSequence : MonoBehaviour
{
    public Vector3 EndingPosition;
    public ExplorerMovementScript Movement;
    public GameObject Explorer1;
    public GameObject Explorer2;
    public GameObject Platform;
    public bool Final;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explorer"))
        {
            NavMeshAgent Expl = Movement.explorer;
            Expl.destination = EndingPosition;
            StartCoroutine(LastPoint(Expl));
        }

    }

    IEnumerator LastPoint(NavMeshAgent a)
    {
        if (a.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Debug.Log("Howdy");
            Explorer1.GetComponent<ExplorerMovementScript>().enabled = false;
            Explorer1.transform.parent = Platform.transform;
            Explorer2.GetComponent<ExplorerMovementScript>().enabled = false;
            Explorer2.transform.parent = Platform.transform;
        }
        yield return null;
    }
}