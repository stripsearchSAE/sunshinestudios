using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{

    public GameObject[] Explorers;
    // private Transform _targetTransform;
    public float dampening;
    public float lavaOffset = 1f;
    public bool lavaAlwaysRises;
    public Transform maxHeight;

    private Vector3 velocity = Vector3.zero; // reference for smoothdamp

    // Start is called before the first frame update
    void Start()
    {
        Explorers = GameObject.FindGameObjectsWithTag("Explorer"); // find all explorers in the scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // Vector3 _prevPosition;
        float minDistance = float.MaxValue;
        GameObject closestExplorer = Explorers[0];

        foreach (GameObject explorer in Explorers)
        {
            // get this distance
            float thisDistance = Mathf.Abs(explorer.transform.position.y - transform.position.y);

            // compare and update minimum distance & closest character if required
            if (thisDistance < minDistance)
            {
                minDistance = thisDistance;
                closestExplorer = explorer;
            }
        }

        // Debug.Log(minDistance);
        if (minDistance <= lavaOffset + 0.5f)
        {
            if (!lavaAlwaysRises) return;
            transform.Translate(Vector3.up * Time.deltaTime / dampening, Space.World);
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, closestExplorer.transform.position.y - lavaOffset, transform.position.z), ref velocity, dampening);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, float.MinValue , maxHeight.position.y), transform.position.z); // clamp position to not go over nolegsplat position

    }

}
