using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayChildren : MonoBehaviour
{
    public ParticleSystem effect1; public ParticleSystem effect2; public ParticleSystem effect3;

    private void Start()
    {
        effect1 = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        effect2 = this.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        effect3 = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    public void PLAY()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            effect1.Play();
            effect2.Play();
            effect3.Play();

        }
    }

    // Update is called once per frame
    void Update()
    {
        PLAY();
    }
}