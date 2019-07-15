using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform Exit;
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
        Debug.Log("Hit something");
        if (other.gameObject.CompareTag("Explorer"))
        {
            Debug.Log("Hit explorer");
            other.transform.position = Exit.position;
        }
    }
}
