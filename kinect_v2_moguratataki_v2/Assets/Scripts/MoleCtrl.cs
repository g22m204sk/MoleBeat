using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public delegate void Move();
public delegate void SET();
public delegate void Attack(GameCtrler target);
public class MoleCtrl : MonoBehaviour
{

    public bool Isdead;
    public bool Upping;

    public float WaitTime;
    public int MoleHP;
    public GameObject Particle_damage;
    public AudioSource AS;
    public AudioClip SE;
    public int ThisKind;
    public bool LStart;
    public bool UpStart;
    public float Speed;
    bool IsBeam;
    GameObject Particle_Bullet_pre;
    GameObject Particle_Beam_pre;
    GameObject Beam, Bullet;
    GameObject pre_Attacked;
    public bool IsReflect;

    public movement move;
    float Attacktimer;
    private void Start()
    {
        move = this.gameObject.AddComponent<movement>();
        move.attack = this.gameObject.AddComponent<MoleAttack>();
        move.attack.set();
        MoleHP = 20;
        Particle_damage = Resources.Load("Attack1") as GameObject;
        
        SE = Resources.Load("punch-middle2") as AudioClip;
        AS = this.gameObject.AddComponent<AudioSource>();
        AS.clip = SE;
        ThisKind = MoleRandomInit.MoleKind;

        Settransform();
        pre_Attacked = Instantiate(Particle_damage, this.gameObject.transform);
        pre_Attacked.transform.parent = this.gameObject.transform;
        Attacktimer = UnityEngine.Random.Range(5, 10);
    }

    public void Settransform()
    {
        switch (ThisKind)
        {
            case 0:
                move.def = this.gameObject.AddComponent<DefMole>();
               IsReflect =  move.def.Setup();
                break;
            case 1:
                move.wave = this.gameObject.AddComponent<WaveMole>();
               IsReflect= move.wave.Setup();
                break;
            case 2:
                move.dia = this.gameObject.AddComponent<DiagonalMole>();
                IsReflect=  move.dia.Setup();
                break;
            case 3:
                move.updown = this.gameObject.AddComponent<UpDownMole>();
                IsReflect = move.updown.Setup();
                break;
        }
    }

    public Move MoveFuncSelect()
    {
        switch (ThisKind)
        {
            case 0:
                return DefMove;
            case 1:
                return WaveMove;
            case 2:
                return DiagonalMove;
            case 3:
                return UpDownMove;
            default: return null;
        }

    }
    public void DefMove()
    {
        if (!move.attack.Beam_Playing  && !move.attack.Bullet_Playing)
            move.def.Move(IsReflect);
    }

    public void WaveMove()
    {
        if (!move.attack.Beam_Playing && !move.attack.Bullet_Playing)
            move.wave.Move(IsReflect);
    }
    public void DiagonalMove()
    {
        if (!move.attack.Beam_Playing && !move.attack.Bullet_Playing)
            move.dia.Move(IsReflect);
    }
    public void UpDownMove()
    {
        if (!move.attack.Beam_Playing && !move.attack.Bullet_Playing)
            move.updown.Move(IsReflect);
    }

    public void moleMove(Move move)//一連の動作
    {
        if (!Isdead)
        {
            move();
        }
    }
    
    public void SelectAttack( )
    {
        bool Which = UnityEngine.Random.Range(0, 2) % 2 == 0;
        move.attack.target = GameCtrler.SpineBasePos[(int)UnityEngine.Random.Range(0, GameCtrler.Human.Count)];
        if (Which)
             move.attack.Attack_Beam() ;
        else
             move.attack.Attack_Bullet();
    }

    public void MoleAttack()
    {
        Attacktimer -= Time.deltaTime;
        if (Attacktimer < 0)
        {
            SelectAttack();
            Attacktimer = UnityEngine.Random.Range(10, 15);
        }
            
    }
    
    private void Update()
    {
        if (Isdead)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.gameObject.transform.position.x, this.gameObject.gameObject.transform.position.y, -1);
            this.gameObject.transform.localRotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y, this.gameObject.transform.localRotation.z, Quaternion.identity.w);
            if (this.gameObject.transform.position.x > 10 || this.gameObject.transform.position.x < -10)
                this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-this.gameObject.GetComponent<Rigidbody>().velocity.x, this.gameObject.GetComponent<Rigidbody>().velocity.y, this.gameObject.GetComponent<Rigidbody>().velocity.z);
            if (this.gameObject.transform.position.y > 5 || this.gameObject.transform.position.y < -5)
                this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(this.gameObject.GetComponent<Rigidbody>().velocity.x, -this.gameObject.GetComponent<Rigidbody>().velocity.y, this.gameObject.GetComponent<Rigidbody>().velocity.z);
        }
        else
        {
            if (!move.attack.Bullet.GetComponent<AttackParticle>().PS2.isPlaying && !move.attack.Bullet.GetComponent<AttackParticle>().PS1.isPlaying)
                move.attack.Bullet_Playing = false;
            else
                move.attack.Bullet_Playing = true;

            if (!move.attack.Beam.GetComponent<AttackParticle>().PS2.isPlaying && !move.attack.Beam.GetComponent<AttackParticle>().PS1.isPlaying)
                move.attack.Beam_Playing = false;
            else
                move.attack.Beam_Playing = true;
        }
    }
    private void OnCollisionEnter(Collision collision)//反射して他のバグにぶつかったとき
    {
        if (collision.gameObject.tag == "MOLE")
        {
            var mole = collision.gameObject.GetComponent<MoleCtrl>();
            if (!mole.Isdead)
            {
                mole.MoleHP -= 1;
                HPbarCtrler.HP -= 1;
                if (mole.MoleHP <= 0)//ほかの敵を倒したら
                {
                    if (collision.gameObject.GetComponent<Rigidbody>() == null)
                    {
                        collision.gameObject.AddComponent<Rigidbody>();
                        collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                    AS = this.gameObject.AddComponent<AudioSource>();

                    AS.clip = SE;
                    mole.Isdead = true;
                    Destroy(collision.gameObject, 2.0f);
                }
                if (Isdead)//自分が死んでいたら
                {
                    pre_Attacked.GetComponent<Attack1Play>().PLAY();
                    AS.Play();
                }
            }
        }
    }
}
