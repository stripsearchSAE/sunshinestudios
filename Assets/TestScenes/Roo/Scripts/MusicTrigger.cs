using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    private bool hasFired = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ExplorerMovementScript>() && !hasFired)
        {
            hasFired = true;
            BaseAudioManager.Playsound("Level1");
        }
    }
}
