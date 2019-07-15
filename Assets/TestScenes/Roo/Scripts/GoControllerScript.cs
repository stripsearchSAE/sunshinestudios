using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoControllerScript : MonoBehaviour
{
    LineRenderer _line;
    // Start is called before the first frame update
    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool controllerCheck = OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote) || OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote);

        if (!controllerCheck)
        {
            _line.enabled = false;
            return;
        }
        _line.enabled = true;
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
    }
}
