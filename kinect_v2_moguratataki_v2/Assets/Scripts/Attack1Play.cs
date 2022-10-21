using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attack1Play : MonoBehaviour
{
    public ParticleSystem effect1; public ParticleSystem effect2; public ParticleSystem effect3; public ParticleSystem effect4;
    public ParticleSystem.MainModule main1; public ParticleSystem.MainModule main2; public ParticleSystem.MainModule main3; public ParticleSystem.MainModule main4;
    private void Start()
    {
        effect1 = this.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        effect2 = this.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        effect3 = this.gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
        effect4 = this.gameObject.transform.GetChild(3).GetComponent<ParticleSystem>();

        main1 = effect1.main;
        main2 = effect2.main;
        main3 = effect3.main;
        main4 = effect4.main;

        main1.playOnAwake = false;
        main2.playOnAwake = false;
        main3.playOnAwake = false;
        main4.playOnAwake = false;
    }

    public void PLAY()
    {
        Color c = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);

        main1.startColor = c;
        main2.startColor = c;
        main4.startColor = c;
        c.a = 0.2f;
        main3.startColor = c;
        effect1.Play();
        effect2.Play();
        effect3.Play();
        //effect4.Play();
    }
    
    public bool debug = false;
    private void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.Space))
            PLAY();
    }

}

