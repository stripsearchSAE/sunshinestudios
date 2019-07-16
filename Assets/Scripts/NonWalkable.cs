using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonWalkable : MonoBehaviour
{
    public GameObject BoxBoi;
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
            BoxBoi.SetActive(true);
        }
    }
}
