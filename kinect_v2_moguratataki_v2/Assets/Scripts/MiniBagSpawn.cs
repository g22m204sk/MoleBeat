using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBagSpawn : MonoBehaviour
{
    public static  GameObject[] _MiniBags = new GameObject[100];

    /// <summary>
    /// 生きているミニバグの数を取得
    /// </summary>
    /// <returns></returns>
    public static int LivingMiniBug()
    {
        int Count = 0;
        foreach (GameObject obj in _MiniBags)
            Count += obj.GetComponent<MiniBag>().IsDead() ? 0 : 1;
        return Count;
    }


    public void Spawn()
    {
        int x = 0 ;
        int y = 0 ;
        
        for (int i = 0; i < 100; i++)   //100匹を整列
        {
            _MiniBags[i] = Instantiate(Resources.Load("MiniBag") as GameObject, this.gameObject.transform);
            _MiniBags[i].transform.position = new Vector3(-8.5f + (x % 10)*1.8f, 4.5f - (y % 10f), -1);
            x += 1 ;
            if( x % 10  == 0 )
                y += 1 ;
        }
        
        for(int i = 0; i < 100; i++)    //微移動
        {
            _MiniBags[i].transform.position = new Vector3(_MiniBags[i].transform.position.x + UnityEngine.Random.Range(-1.0f, 1.0f), _MiniBags[i].transform.position.y + UnityEngine.Random.Range(-0.5f, 0.5f), _MiniBags[i].transform.position.z);
            _MiniBags[i].transform.rotation = Quaternion.Euler(UnityEngine.Random.Range(0, 360), -90, 90);
        }
    }
}
