using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointCtrler : MonoBehaviour
{
    private AudioClip Punch_middle;
    private AudioClip Punch_high;
    private AudioSource PlaySE;
    const int Damage_Low = 1;
    const int Damage_High = 2;
    private int ThisCount;
    // Start is called before the first frame update
    void Start()
    {
        PlaySE = this.gameObject.AddComponent<AudioSource>();
        this.gameObject.AddComponent<AudioDistortionFilter>().distortionLevel = 0.75f;
        PlaySE.pitch = 0.55f;
        PlaySE.playOnAwake = false;

        Punch_middle = Resources.Load("punch-middle2", typeof(AudioClip)) as AudioClip;
        Punch_high = Resources.Load("punch-high1", typeof(AudioClip)) as AudioClip;

        ThisCount = bodyCtrler.JointCount; bodyCtrler.JointCount++;
        if (bodyCtrler.JointCount > 24)//bodyCtrler.Joints.Length - 1
            bodyCtrler.JointCount = 0;
        /*BoxCollider col = this.gameObject.AddComponent<BoxCollider>();
        if (this.gameObject.name != "empty:SpineMid")
        {
            col.size = Vector3.one * 2.5f;

        }*/
            
        this.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
    }
    
    public float Caluculatemagnitude(Vector3 vec)
    {
        return vec.x * vec.x + vec.y * vec.y + vec.z * vec.z;
    }

    private int LowDamage()
    {
        PlaySE.clip = Punch_middle;
        return Damage_Low;
    }

    private int HighDamage()
    {
        PlaySE.clip = Punch_high;
        return Damage_High;
    }
    private void OnCollisionEnter(Collision collision)
    {
        MiniBag _minibug =  collision.gameObject.GetComponent<MiniBag>() ;
        if (_minibug == null)
            return;
        else
        {
            Debug.Log("hitted");
            if (bodyCtrler.Attackable[ThisCount])
            {
                _minibug.Damaged(Caluculatemagnitude(bodyCtrler.JointMoveVector[ThisCount]) < 10 ? LowDamage() : HighDamage());
                this.gameObject.transform.GetChild(0).GetComponent<Attack1Play>().PLAY();
                PlaySE.Play();
            }
                
            
            
        }
        /*var mole = collision.gameObject.GetComponent<MoleCtrl>();
        if (mole != null)
        {
            if (bodyCtrler.Attackable[ThisCount] && !mole.Isdead)
            {
                mole.IsReflect = !mole.IsReflect;
                
                int damage;
                if (Caluculatemagnitude(bodyCtrler.JointMoveVector[ThisCount]) < 10)
                {
                    damage = 1;
                    PlaySE.clip = Punch_middle;
                }
                else
                {
                    damage = 2;
                    PlaySE.clip = Punch_high;
                }
                this.gameObject.transform.GetChild(0).GetComponent<Attack1Play>().PLAY();
                HPbarCtrler.HP -= damage;
                mole.MoleHP -= damage;
                PlaySE.Play();

                if (mole.MoleHP <= 0)
                {
                    if (collision.gameObject.GetComponent<Rigidbody>() == null)
                    {
                        Rigidbody rig;
                        rig = collision.gameObject.AddComponent<Rigidbody>();
                        collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                    mole.Isdead = true;
                    Destroy(collision.gameObject, 2.0f);
                }
            }
        }*/
    }
}

