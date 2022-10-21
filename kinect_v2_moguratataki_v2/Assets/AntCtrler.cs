using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntCtrler : MonoBehaviour
{
    public Animator anim;
    public bool[] x = new bool[4];
    int xCount  = 1;
    void Update()
    {

        if (IsWait)
        {
            GO(xCount);
            xCount++;
            if (xCount> x.Length)
                xCount = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.A)) GO(1);
        if (Input.GetKeyDown(KeyCode.S)) GO(2);
        if (Input.GetKeyDown(KeyCode.D)) GO(3);
        if (Input.GetKeyDown(KeyCode.F)) GO(4);
    }

    public void GO(int v)
    {
        GoWait();
        Set(v, true);
    }



    public bool IsWait
    {
        get
        {
            foreach (bool xv in x)
                if (xv)
                    return false;
            return true;
        }
    }

    void Set(int g, bool v)
    { 
        x[g-1] = v;
        anim.SetBool("x"+(g), v);  
    }

    void GoWait()
    { 
        for (int y = 1; y <= 4; y++)
        {
            if (x[y-1])
            {
                anim.SetBool("x" + (y), false);
                x[y - 1] = false;
            }
        }
        Debug.Log("goWait"); 
    }

    public void end1() { Debug.Log("end1");  GoWait(); }
    public void end2() { Debug.Log("end2");  GoWait(); }
    public void end3() { Debug.Log("end3");  GoWait(); }
    public void end4() { Debug.Log("end4");  GoWait(); }

}
