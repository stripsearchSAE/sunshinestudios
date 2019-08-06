using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMovement : MonoBehaviour
{
    public float offset;
    public Transform Lava;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Lava.position.x, Lava.position.y + offset, Lava.position.z);
    }
}
