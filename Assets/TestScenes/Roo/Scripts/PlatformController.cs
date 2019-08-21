using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Explorer;

    void Start()
    {
        Explorer = GameObject.Find("Explorer");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Ball.SetActive(false);
    }

    public void MakeBallActive(RaycastHit hit)
    {
        if (Vector3.Distance(Explorer.transform.position, transform.position) < Explorer.GetComponent<ExplorerMovementScript>().maxDistancePerTurn && Explorer.GetComponent<ExplorerMovementScript>().isActive) Ball.SetActive(true);
    }
}
