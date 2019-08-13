using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
    public GameObject Smoke;
    public GameObject LavaErrupt;
    public GameObject Rocks;
    public GameObject Steam;

    public float timeToWait;
    // Start is called before the first frame update
    void Start()
    {
        ExplorerEndSequence.StartFinalSequence += VolcanoSequence;
    }

    void VolcanoSequence()
    {
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence()
    {
        Smoke.SetActive(true);
        Steam.GetComponent<ParticleSystem>().Stop();

        yield return new WaitForSeconds(timeToWait);

        Smoke.GetComponent<ParticleSystem>().Stop();
        
        LavaErrupt.SetActive(true);
        // Rocks.SetActive(true);
    }
}
