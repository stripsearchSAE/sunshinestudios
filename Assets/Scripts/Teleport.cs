using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            ExplorerMovementScript state = other.GetComponent<ExplorerMovementScript>();
            if (state == null) return;
            state.isActive = false;
            state.isTravelling = false;
            state.explorer.isStopped = false;
            state.goIdle();
            other.transform.position = Exit.position;
            
        }
    }
}
