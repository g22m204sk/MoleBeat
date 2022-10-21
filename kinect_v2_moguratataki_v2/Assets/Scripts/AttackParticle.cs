using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackParticle : MonoBehaviour
{

    public ParticleSystem PS1 , PS2;
    public AudioSource AS;
    public AudioClip AC1, AC2;
    public float Chargetime;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PS1Play()
    {
        PS1.Play();
        AS.clip = AC1;
        AS.Play();
    }
    public void PS2Play()
    {
        PS2.Play();
        AS.clip = AC2;
        AS.Play();
    }
    public void Attack()
    {
        PS1Play();
        Invoke("PS2Play", Chargetime);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }
}
