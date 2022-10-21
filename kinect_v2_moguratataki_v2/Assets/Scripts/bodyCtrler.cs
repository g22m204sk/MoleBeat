using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System;
using System.IO;
public class bodyCtrler : MonoBehaviour
{
    BodySourceView bodySourceView;
    public GameObject[] Joints;
    [SerializeField]
    public Vector3[] JointsPos;//各jointの位置
    public Vector3[] LJointsPos;//各jointの１フレーム前の位置
    public static Vector3[] JointMoveVector;//各jointの方向
    public float[] JointSize;
    public static bool[] Attackable;
    public static int JointCount = 0;

    public List<float> MoveData = new List<float>();
    public GameObject[] EmptyObjs;
    public GameObject EmptyParent;
    public GameObject Particle_damage;
    public GameObject Particle_fire;
    public ParticleSystem.MainModule firemain;

    public GameObject Heart_pre, Heart;

    JointType[] AttackPoint =
    {
        JointType.HandLeft,
        JointType.HandRight,
        JointType.FootLeft,
        JointType.FootRight,
    };
      public float[] weight = new float[]{
      1.2f,
      1.2f,
      0.5f,
      0.8f,
      1f,
      0.5f,
      0.4f,//elbow
      0.5f,
      1f,
      0.5f,
      0.4f,
      0.5f,
      1.6f,//hip
      1.5f,//knee
      0.8f,//ankle
      1.5f,//foot
      1.6f,//hip
      1.5f,//knee
      0.8f,//ankle
      1.5f,//foot
      1.2f,
      0.5f,
      0.5f,
      0.5f,
      0.5f,
      0.5f,
      0.5f,
  };
    private void Start()
    {
        Particle_damage = Resources.Load("Attack1") as GameObject;
        Particle_fire = Resources.Load("fire") as GameObject;
        Heart_pre = Resources.Load("Heart") as GameObject;
        //firemain = Particle_fire.GetComponent<ParticleSystem>().main;
        //Color c = new Color(UnityEngine.Random.Range(0f, 255f) / 255, UnityEngine.Random.Range(0f, 255f) / 255, UnityEngine.Random.Range(0f, 255f) / 255, 0.047f);
        //Debug.Log(c);
        //firemain.startColor = c;

        EmptyParent = new GameObject("Empties");
        EmptyObjs = new GameObject[Enum.GetNames(typeof(JointType)).Length];

        Joints = GetChildren(this.gameObject, Enum.GetNames(typeof(JointType)).Length);

        for (int i = 0; i < EmptyObjs.Length; i++) //emptyにいろいろ追加する
        {
            EmptyObjs[i] = new GameObject("empty:" + Joints[i].gameObject.name);

            EmptyObjs[i].AddComponent<JointCtrler>();
            foreach (JointType type in AttackPoint)
                if (Joints[i].name == type.ToString())
                {
                    EmptyObjs[i].AddComponent<Rigidbody>();
                    EmptyObjs[i].layer = 10;
                }


            EmptyObjs[i].transform.parent = EmptyParent.transform;
            GameObject pre = Instantiate(Particle_damage, Vector3.zero, Quaternion.identity);
            pre.transform.parent = EmptyObjs[i].transform;
            GameObject pre_fire = Instantiate(Particle_fire, Vector3.zero, Quaternion.identity);
            ParticleSystem.MainModule main = pre_fire.GetComponent<ParticleSystem>().main;
            main.startLifetime = main.startLifetime.constant * weight[i];


            pre_fire.transform.parent = EmptyObjs[i].transform;
            BoxCollider col = EmptyObjs[i].AddComponent<BoxCollider>();
            if (Joints[i].gameObject.name == "SpineMid")
            {
                GameObject Heart = Instantiate(Heart_pre, Vector3.zero, Quaternion.identity);
                Heart.transform.parent = EmptyObjs[i].transform;
                Heart_pre.GetComponent<ParticleSystem>().Play();
                EmptyObjs[i].layer = 9;
            }
            else
                col.size = Vector3.one * 3.25f;
            if (Joints[i].name == "ElbowRight" || Joints[i].name == "ElbowLeft")
                EmptyObjs[i].layer = 10;

        }
        JointsPos = new Vector3[Enum.GetNames(typeof(JointType)).Length];
        JointMoveVector = new Vector3[Enum.GetNames(typeof(JointType)).Length];
        LJointsPos = new Vector3[Enum.GetNames(typeof(JointType)).Length];
        Attackable = new bool[Enum.GetNames(typeof(JointType)).Length];
        JointSize = new float[Enum.GetNames(typeof(JointType)).Length];

        EmptyParent.transform.parent = this.gameObject.transform;
        AttackJoint_LAST = new Vector3[AttackPoint.Length];
    }

    void SetTransform()
    {
        for (int i = 0; i < Joints.Length; i++)
        {
            EmptyObjs[i].transform.position = new Vector3(Joints[i].transform.position.x, Joints[i].transform.position.y, -1);
        }

    }


    public GameObject[] GetChildren(GameObject ParentObj, int length)//直下の子オブジェクトを全部取得するGameObject配列を新たに作る。 Joints = GetChildren(this.gameobject, Joints.length);
    {
        if (ParentObj.transform.childCount == 0)
            return null;

        GameObject[] childObjs = new GameObject[length];
        foreach (Transform child in this.gameObject.transform)
        {
            var get = (JointType)Enum.Parse(typeof(JointType), child.transform.gameObject.name);//子オブジェクトの名前からEnum型に変換
            childObjs[(int)get] = child.transform.gameObject;                                   //enum型の要素番号を取得
            childObjs[(int)get].AddComponent<Rigidbody>();

        }
        return childObjs;
    }

    void CaluculateMoveVector()
    {
        for (int i = 0; i < JointSize.Length; i++)
        {
            JointSize[i] = JointsPos[i].x * JointsPos[i].x + JointsPos[i].y * JointsPos[i].y + JointsPos[i].z * JointsPos[i].z;
        }

        for (int i = 0; i < JointMoveVector.Length; i++)
        {
            if (Joints[i] != null)
            {
                JointsPos[i] = Joints[i].transform.position;
                Vector3 s = JointsPos[i] - LJointsPos[i];//前の位置からの差分　→　移動量
                                                         //Debug.Log(Joints[i].name+"　：　"+s.x * s.x + s.y * s.y + s.z * s.z);//直線的な移動距離
                                                         /*if (Joints[i].name == "HandRight")
                                                         {
                                                             //Debug.Log(Joints[i].name + "　：　" + (s.x * s.x + s.y * s.y + s.z * s.z));
                                                             //MoveData.Add(s.x * s.x + s.y * s.y + s.z * s.z);

                                                         }*/

                if (s.x * s.x + s.y * s.y + s.z * s.z > JointSensibility()) //jointが一定以上動いたら
                {


                    // Debug.Log(JointSensibility());
                    JointMoveVector[i] *= 0.7f;
                    JointMoveVector[i] += JointsPos[i] - LJointsPos[i];
                }

                LJointsPos[i] = JointsPos[i];
            }

        }
    }

    public float JointSensibility()
    {
        float sum = 0f;
        foreach (float size in JointSize)
            sum += size;

        sum /= JointSize.Length;
        //Debug.Log(sum);
        if (sum < 2)
            return 2f;
        if (sum > 4)
            return 4f;
        return sum;
    }
    void SettingCollider()
    {
        foreach (GameObject joint in Joints)
        {
            joint.GetComponent<BoxCollider>().center = new Vector3(joint.transform.position.x, joint.transform.position.y, -1);
            //Debug.Log(joint.GetComponent<BoxCollider>().center);
        }
    }

    public void JudgeAttackable()
    {
        for (int i = 0; i < JointMoveVector.Length; i++)
        {
            if (JointSize[i] > 4)//1もともと2
            {
                Attackable[i] = true;
                // Debug.Log(JointSize[i]);
            }
            else
                Attackable[i] = false;
        }
    }

    void localRotationReset()
    {
        foreach (GameObject joint in Joints)
        {
            //if (joint.activeInHierarchy)
            joint.transform.localRotation = Quaternion.identity;
        }

    }



    Vector3 L_bc;
    Vector3[] AttackJoint_LAST;

    void CompareBody()
    {
        //腰骨の移動量------------------
        Vector3 bodycenter = transform.InverseTransformPoint(Joints[(int)JointType.SpineBase].transform.position);
        Vector3 Move_dc = bodycenter - L_bc;
        //各攻撃部位の移動量---------------------------
        //現在の座標----
        Vector3[] objs = new Vector3[AttackPoint.Length];
        for (int g = 0; g < AttackPoint.Length; g++)
            objs[g] = transform.InverseTransformPoint(Joints[(int)AttackPoint[g]].transform.position);
        //１フレーム前の差分------------
        Vector3[] AJ = new Vector3[AttackPoint.Length];
        for (int g = 0; g < AttackPoint.Length; g++)
            AJ[g] = objs[g] - AttackJoint_LAST[g];
        //次のフレームに備えてL_の値を更新---------------
        L_bc = bodycenter;
        for (int g = 0; g < 4; g++)
            AttackJoint_LAST[g] = objs[g];

        //各ボーンが攻撃中か速度で判定------------------
        for (int i = 0; i < objs.Length; i++)
        {
            // float f = (bodycenter.x - objs[i].x) * (bodycenter.x - objs[i].x) + bodycenter.y - objs[i].y; 
            Vector3 t = (AJ[i] - Move_dc);
            float f = t.sqrMagnitude; //部位単体の移動量
            DEBUG_.Now_ = f;
            //このフレームの各攻撃関節のみの移動量ががワールド座標での腰骨と各攻撃関節の距離より大きかったら
            Attackable[(int)AttackPoint[i]] = f > DEBUG_.TH_;

        }
    }
    
    public void looktargetobj()//つながっている先のjointの方向に向けたい
    {
        for (int i = 0; i < EmptyObjs.Length; i++)
        {
            if (EmptyObjs[i] != null)
            {
                //EmptyObjs[i].transform.localRotation = Quaternion.identity;
                for (int j = 0; j < EmptyObjs.Length; j++)
                {

                    switch (j)
                    {
                        case 3:
                            break;
                        case 7:
                            EmptyObjs[j].transform.LookAt(EmptyObjs[21].transform.position);
                            break;
                        case 11:
                            EmptyObjs[j].transform.LookAt(EmptyObjs[23].transform.position);
                            break;
                        case 15:
                        case 19:
                        case 22:
                        case 24:
                            break;
                        case 20:
                            EmptyObjs[j].transform.LookAt(EmptyObjs[1].transform.position);
                            break;
                        default:
                            EmptyObjs[j].transform.LookAt(EmptyObjs[j + 1].transform.position);
                            break;
                    }
                }
                //Debug.Log(EmptyObjs[i].gameObject.name + "  :  "
                /*EmptyObjs[(int)bodySourceView._BoneMap[(JointType)Enum.ToObject(typeof(JointType), i)]]);*/
                //Debug.Log(EmptyObjs[])
                //Debug.Log( EmptyObjs[i] +"   "+bodySourceView._BoneMap[(JointType)Enum.ToObject(typeof(JointType), i)].ToString());
            }

            //EmptyObjs[i].transform.LookAt(EmptyObjs[(int)bodySourceView._BoneMap[(JointType)Enum.ToObject(typeof(JointType), i)]].transform);
        }
    }
    private void Update()
    {
        if(MiniBagSpawn.LivingMiniBug() >= 28)
        {
            SetTransform();
            CaluculateMoveVector();
            CompareBody();

            looktargetobj();
        }
        
    }

}

