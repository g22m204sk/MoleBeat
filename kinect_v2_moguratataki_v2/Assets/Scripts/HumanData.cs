using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
public class HumanData : MonoBehaviour
{

    //BodySourceManagerで作られた一連の人間の体の情報がDicionaryで保存されているのでそれらを
    //public static Dictionary<int , Dictionary<ulong,GameObject>> Humans = new  Dictionary<int , Dictionary<ulong ,GameObject>>();
    public static List<Dictionary<ulong, GameObject>> Humans = new List<Dictionary<ulong, GameObject>>();
    public static int  HumanID = 0;
    
         
        
    public static Dictionary<ulong , GameObject> GetHumanData(int humanid)
    {
        if (Humans.Count > humanid)
        {
            return Humans[humanid];
        }
        else return null;
    }
    /*
    private void Update()
    {
        for(ulong i=  0; i < (ulong)Humans[0].Count; i++)
        {
            Debug.Log(Humans[0][i].gameObject.name);
        }
            
        
    }*/
}
