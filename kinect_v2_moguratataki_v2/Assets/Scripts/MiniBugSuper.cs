using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBugSuper : MonoBehaviour
{
    private protected int _hp;
    private protected float _speed;
    private protected bool _isdead { get { return _hp < 0; } }
    private protected AudioSource _audiosource;
    private protected AudioClip _damageSE;
    private protected int _damage;
    private protected GameObject _attack1;
    private protected Attack1Play _attack1play;

    private protected int _MiniBagCount;

    public bool IsDead() { return _isdead; }

    public virtual void Damaged(int _damagePoint)
    {
    }
}
