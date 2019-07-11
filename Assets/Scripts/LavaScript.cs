using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{

    public GameObject[] Explorers;
    private Transform _targetTransform;
    public float dampening;
    public float lavaOffset = 1f;
    public bool lavaAlwaysRises;
    // private float[] distance;

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
        // needs to be max distance so can find values that are less
        float minDistance = float.MaxValue;
        GameObject closestExplorer = Explorers[0];
        // int closestCharacter = 0;

        foreach (GameObject explorer in Explorers)
        {
            // get this distance
            float thisDistance = Mathf.Abs(explorer.transform.position.y - transform.position.y);

            // save distance
            // distance[x] = thisDistance;

            // compare and update minimum distance & closest character if required
            if (thisDistance < minDistance)
            {
                minDistance = thisDistance;
                closestExplorer = explorer;
            }
        }

        // Debug.Log(minDistance);
        if (minDistance < lavaOffset)
        {
            if (!lavaAlwaysRises) return;
            transform.Translate(Vector3.up * Time.deltaTime / dampening, Space.World);
            return;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, closestExplorer.transform.position.y, transform.position.z), Time.deltaTime / dampening);
    }

}
