using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public GameObject Rock;

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
            Rigidbody RB = Rock.GetComponent<Rigidbody>();

            RB.useGravity = true;
        }
           
    }
}
