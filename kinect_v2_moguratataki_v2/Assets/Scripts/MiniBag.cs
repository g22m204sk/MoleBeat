using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBag :MiniBugSuper
{
    private void Start()
    {
        _hp = 20;
        _speed = 1f;
        _damageSE = Resources.Load("punch-middle2") as AudioClip;
        _audiosource = this.gameObject.AddComponent<AudioSource>();
        _damage = 1;
        _attack1 = Instantiate(Resources.Load("Attack1") as GameObject, this.gameObject.transform);
        _attack1.transform.parent = this.gameObject.transform;
        _attack1.transform.position = this.gameObject.transform.position;
        _attack1play = _attack1.GetComponent<Attack1Play>();
        _MiniBagCount++;
    }
    /*public override void MoleUpdate()
    {
        base.MoleUpdate();
        if (_isdead)
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
    }*/
    


    public override void Damaged(int _damagePoint)
    {
        base.Damaged(_damagePoint);
        Debug.Log("ヒットしました");
        _hp -= _damage;
        _attack1play.PLAY();
        if(_isdead)
        {
            _MiniBagCount--;
            if(this.gameObject.GetComponent<Rigidbody>()==null)
            this.gameObject.AddComponent<Rigidbody>().useGravity = false;
            this.gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (_isdead)
        {
            if (collision.gameObject.GetComponent<MiniBag>() != null)
            {
                MiniBag _target = collision.gameObject.GetComponent<MiniBag>();
                _target.Damaged(_damage);
                Damaged(_damage);
                _attack1.GetComponent<Attack1Play>().PLAY();//被弾エフェクト


            }
        }
        
        
    }

}
