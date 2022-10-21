using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource AS;
    public AudioClip BGM;
    // Start is called before the first frame update
    void Start()
    {
        AS.clip = BGM;
        AS.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
            if (obj.name.StartsWith("Body"))
                AS.Play();
    }
}
