using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_ : MonoBehaviour
{
    public float Now;
    public float TH = 1.2f;
    public static float TH_,Now_;
    public float debughp;

    public int _debug_int;
    public bool _debug_bool;
    void Update()
    {
        //HPbarCtrler.HP = debughp;
        TH_ = TH;
        Now = Now_;
        DEBUG_INT = _debug_int;
        DEBUG_BOOL = _debug_bool;
    }
    public static int DEBUG_INT;
    public static bool DEBUG_BOOL;
}
