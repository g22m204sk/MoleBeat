using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrler : MonoBehaviour
{
    public static float ClearAfterTimer;

    public static List<GameObject> Human = new List<GameObject>();
    public static GameObject[] SpineBasePos = new GameObject[4];
    public GameObject ant;
    private MiniBugSuper _minibugsuper;
    private MiniBagSpawn _minibagspawn;
    
    // Start is called before the first frame update
    void Start()
    {
        _minibugsuper = this.gameObject.GetComponent<MiniBugSuper>();
        _minibagspawn = this.gameObject.GetComponent<MiniBagSpawn>();
        _minibagspawn.Spawn();
    }
    public static bool InPlay()
    {
        bool bodyserch = false;
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj.name.StartsWith("Body"))
            {
                bodyserch = true;
            }
        }
        
        return bodyserch ;
    }
    

    public void UpdateHuman()
    {
        foreach (GameObject obj in Object.FindObjectsOfType<GameObject>())
        {
            if (obj.name.StartsWith("Body"))
            {
                if (Human.Contains(obj))
                {
                    break;
                }
                else
                {
                    Human.Add(obj);
                }

            }
        }
    }
    public void FindSpineBase()
    {
        for (int i = 0; i < SpineBasePos.Length; i++)
        {
            if (Human[i] != null)
            {
                SpineBasePos[i] = Human[i].transform.Find("Empties").transform.Find("empty:SpineMid").gameObject;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MiniBagSpawn.LivingMiniBug() >= 28)
        {
            if (InPlay()) //ゲーム続行条件
            {
                UpdateHuman();
                FindSpineBase();
            }
        }
        else
        {
        }
        
    }
    private void Reset()
    {
        _minibagspawn.Spawn();
    }
}


/*
 * public void Reset()
    {
        
        GameCtrler.ClearAfterTimer =300;
        GameCtrler.Human.Clear();
        GameCtrler.SpineBasePos = new GameObject[4];
        MoleRandomInit.DefMoles.Clear();
        MoleRandomInit.MoleKind = 0;
        Marker.transform.localScale = Vector3.zero;
        HPbarCtrler.HP = 500;
        ant.GetComponent<AriCtrler>().Set();
        ant.GetComponent<AriCtrler>().Set2flag = true;
        ant.GetComponent<AriCtrler>().Set3flag = true;

    }
 * 
 * 
 * if(HPbarCtrler.HP > 0)
        {
            if (InPlay())
            {
                UpdateHuman();
                FindSpineBase();
            }
        }
        else
        {
           
            ClearAfterTimer -= Time.deltaTime;
            foreach (GameObject mole in MoleRandomInit.DefMoles)
            {
                Destroy(mole);
            }
            MoleRandomInit.DefMoles.Clear();
            if (ClearAfterTimer < 0)
            {
                Reset();
            }
        }*/
