using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriCtrler : MonoBehaviour
{
    const int MaxHP = 500;
    int dircount;
    float speed;
    public Vector3 StartPos;
    public Vector3 GoalPos;
    bool RightStart;
    private Vector3[] AntDirections = new Vector3[4]
    {
        new Vector3(-135,90,-90),
        new Vector3(-225,90,-90),
        new Vector3(-315,90,-90),
        new Vector3(-45, 90 ,-90)
    };

    public void Start()
    {
        Set();

        RightStart = true;
    }
    public void Set()
    {
        transform.localScale = Vector3.one;
        transform.position = new Vector3(13.5f, 0, -2);
        transform.rotation = Quaternion.Euler(AntDirections[dircount]);
        dircount = 0;
        speed = 2.0f;
    }
    void AntMove1()//画面４隅をかすめるように歩く
    {
        switch (dircount)
        {
            case 0:
                if (transform.position.x < 0)
                {
                    dircount++;
                    transform.rotation = Quaternion.Euler(AntDirections[dircount]);
                }

                else
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                break;
            case 1:
                if (transform.position.x < -12)
                {
                    dircount++;
                    transform.rotation = Quaternion.Euler(AntDirections[dircount]);
                }
                else
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                break;
            case 2:
                if (transform.position.x > 0)
                {
                    dircount++;
                    transform.rotation = Quaternion.Euler(AntDirections[dircount]);
                }
                else
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                break;

            case 3:
                if (transform.position.x > 12)
                {
                    dircount = 0;
                    transform.rotation = Quaternion.Euler(AntDirections[dircount]);
                }
                else
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                break;
        }
    }
    public void Set2()
    {

        transform.localScale = Vector3.one * 1.5f;
        if (RightStart)
        {
            StartPos = new Vector3(12f, UnityEngine.Random.Range(-4, 4), -2);
            GoalPos = new Vector3(-12f, UnityEngine.Random.Range(-4, 4), -2);
        }
        else
        {
            StartPos = new Vector3(-12f, UnityEngine.Random.Range(-4, 4), -2);
            GoalPos = new Vector3(12f, UnityEngine.Random.Range(-4, 4), -2);
        }
        transform.position = StartPos;
        //transform.LookAt(new Vector3(GoalPos.x, GoalPos.y, transform.position.z));
        transform.LookAt(GoalPos);

        if (!RightStart)
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z - 90));
        else
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z +90));
        //transform.rotatiaaon = Quaternion.Euler(new Vector3(transform.position.x, -90, 90));
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y + 90, transform.rotation.z ));




    }
    public void AntMove2()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (Mathf.Abs(transform.position.x - GoalPos.x) < 0.5 )
        {
            RightStart = !RightStart;
            Set2();
        }
    }


    public void Set3()
    {
        transform.localScale = Vector3.one * 0.01f;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(new Vector3(180, 0, -180));



    }
    public void AntMove3()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.localScale += Vector3.one * 0.3f * Time.deltaTime;
    }
    int _movestats = 0;
    private void Update()
    {
        if (GameCtrler.InPlay() )
        {
            switch (_movestats)
            {
                case 0:
                    if (MiniBagSpawn.LivingMiniBug() > 50/* || DEBUG_.DEBUG_INT > 50|| DEBUG_.DEBUG_BOOL*/)
                        AntMove1();
                    else
                        _movestats = 100;
                    break;

                case 1:
                    if (MiniBagSpawn.LivingMiniBug() > 35/* || DEBUG_.DEBUG_INT > 5*/)
                        AntMove2();
                    else
                        _movestats = 101;
                    break;

                case 2:
                    if (MiniBagSpawn.LivingMiniBug() >=28 /*|| DEBUG_.DEBUG_INT > 1*/)
                        AntMove3();
                    else
                    {
                        _movestats = 102;
                        foreach (GameObject g in MiniBagSpawn._MiniBags)
                            g.SetActive(false);
                    }
                        
                    break;
                case 100:
                    Set2();
                    _movestats = 1;
                    break;
                case 101:
                    Set3();
                    _movestats = 2;
                    break;

                case 102:

                    this.gameObject.SetActive(false);
                    break;
            }





            /*if (HPbarCtrler.HP > MaxHP * 0.5)
                AntMove1();
            else if (HPbarCtrler.HP > MaxHP * 0.1)
            {
                if (Set2flag)
                    Set2();
                AntMove2();
            }

            else if (HPbarCtrler.HP > 1)
            {
                if (Set3flag)
                    Set3();
                AntMove3();
            }
            else
            {
                if (transform.rotation.x <= 0)
                    transform.Rotate(-90 * Time.deltaTime, 0, 0);
            }*/

        }
    }
}