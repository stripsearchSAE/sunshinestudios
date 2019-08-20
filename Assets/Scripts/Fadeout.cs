using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    public Image fadebitch;
    public bool Trig;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    if(Trig == true)
        {

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Explorer"))
        {
            fadebitch.CrossFadeAlpha(4f, 4f, false);
            Trig = true;
        }
    }
}
