using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaticleHit : MonoBehaviour
{
    bool IsBeam;
    float damage;

    private void Start()
    {
        IsBeam = this.gameObject.name == "Beam" ? true : false;
        if (IsBeam)
            damage = 5;
        else damage =0.5f;
    }
    private void OnParticleCollision(GameObject other)
    {
        if (GameCtrler.InPlay())
        {
            if (other.name == "empty:SpineMid")
            {
                HPbarCtrler.HP += damage;
                if (HPbarCtrler.HP > 500)
                    HPbarCtrler.HP = 500;
            }
        }
        
        
    }
}

