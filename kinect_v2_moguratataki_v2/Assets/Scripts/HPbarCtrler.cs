using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarCtrler : MonoBehaviour
{
    public static float HP;
    const float MaxHP = 500;
    public GameObject HPparent;
    public GameObject[] childs;
    public  float oncehp;
    // Start is called before the first frame update
    void Start()
    {
        HP = MaxHP;
        childs = new GameObject[HPparent.transform.childCount];
        for (int i = 0; i < childs.Length; i++)
            childs[i] = HPparent.transform.GetChild(i).gameObject;
        oncehp =(float)(MaxHP / childs.Length);
        Debug.Log((float)(MaxHP / childs.Length));
    }

    // Update is called once per frame
    void Update()
    {
        if (GameCtrler.InPlay())
        {
            if(HP > 1)
            {
                for (int i = 0; i < childs.Length; i++)
                {
                    if (i < HP / oncehp)//体力UIの表示非表示
                        childs[i].GetComponent<Renderer>().material.color = Color.green;
                    else
                        childs[i].GetComponent<Renderer>().material.color = Color.red;

                }
                HP += 0.5f * Time.deltaTime;
                if (HP > 500)
                    HP = 500;
                if (HP < 1)
                    HP = 0;
            }
            
        }
        
    }
}
