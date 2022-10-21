using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayParticle : MonoBehaviour
{
 

    ParticleSystem PS1;
    ParticleSystem.MainModule main1;
    // Start is called before the first frame update
    void Start()
    {
        PS1 = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        Debug.Log(this.gameObject.transform.GetChild(0).name);
        main1 = PS1.main;
        main1.playOnAwake = false;

    }

    
}

/*
public class PlayParticle : MonoBehaviour
{
    ParticleSystem PS1;
    ParticleSystem.MainModule main1;
    // Start is called before the first frame update
    void Start()
    {
        PS1 = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        Debug.Log(this.gameObject.transform.GetChild(0).name);
        main1 = PS1.main;
        main1.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PS1.Play();
        }
    }
}
*/
