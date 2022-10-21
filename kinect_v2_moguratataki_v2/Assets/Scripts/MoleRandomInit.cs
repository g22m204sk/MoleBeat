using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleRandomInit : MonoBehaviour
{
    public GameObject MolePre;
    public static List<GameObject> DefMoles = new List<GameObject>();
    float timer;
    public float WaitTime;
    public const int MaxMole = 10;
    public static int MoleKind;

    public void SpawnMole()
    {

        MoleKind = (int)UnityEngine.Random.Range(0, 4);
        GameObject pre = Instantiate(MolePre, this.gameObject.transform);
        pre.AddComponent<MoleCtrl>();
        pre.tag = "MOLE";
        DefMoles.Add(pre);
    }
    public void MoleGene()
    {

        timer += Time.deltaTime;
        if (timer > WaitTime)
        {
            timer = 0;
            WaitTime = UnityEngine.Random.Range(1.0f, 2.0f);
            if (DefMoles.Count < MaxMole)
            {
                SpawnMole();
            }
        }
    }



    private void Update()
    {

        if (GameCtrler.InPlay() /*&& !(GameCtrler.StageCounnt >= GameCtrler.Stage.Length)*/)
        {
            MoleGene();
            foreach (GameObject mole in DefMoles)
            {
                var m = mole.GetComponent<MoleCtrl>();
                if (!m.Isdead)
                {

                    m.moleMove(m.MoveFuncSelect());//各モグラの番号に応じて移動用の関数を設定している
                    m.MoleAttack();
                }
                    
                else
                    DefMoles.Remove(mole);
                //mole.GetComponent<MoleCtrl>().MoleHit();
            }
        }

    }
}
//m.molemove();

