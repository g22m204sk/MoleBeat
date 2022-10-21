using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public DefMole def;
    public WaveMole wave;
    public DiagonalMole dia;
    public UpDownMole updown;
    public MoleAttack attack;


    // Start is called before the first frame update
    void Start()
    {
        /*
        def = this.gameObject.AddComponent<DefMole>();
        def.Setup();
        wave = this.gameObject.AddComponent<WaveMole>();
        wave.Setup();
        dia = this.gameObject.AddComponent<DiagonalMole>();
        dia.Setup();
        updown = this.gameObject.AddComponent<UpDownMole>();
        updown.Setup();
        
        attack = this.gameObject.AddComponent<MoleAttack>();
        attack.set();
        */
    }

    public void movementSet()
    {
        def = this.gameObject.AddComponent<DefMole>();
        def.Setup();
        wave = this.gameObject.AddComponent<WaveMole>();
        wave.Setup();
        dia = this.gameObject.AddComponent<DiagonalMole>();
        dia.Setup();
        updown = this.gameObject.AddComponent<UpDownMole>();
        updown.Setup();

        attack = this.gameObject.AddComponent<MoleAttack>();
        attack.set();
    }
}


public class MoleAttack : MonoBehaviour
{
    public GameObject Pre_Berm, Pre_Bullet;
    public GameObject Beam, Bullet;
    public bool Beam_Playing;
    public bool Bullet_Playing;

    public GameObject target;
    public void set()
    {
        Pre_Berm = Resources.Load("Beam") as GameObject;
        Pre_Bullet = Resources.Load("Bullet") as GameObject;

        Beam = Instantiate(Pre_Berm, this.gameObject.transform.position, Quaternion.identity);
        Bullet = Instantiate(Pre_Bullet, this.gameObject.transform.position, Quaternion.identity);
        Beam.transform.parent = this.gameObject.transform;
        Bullet.transform.parent = this.gameObject.transform;
        Beam_Playing = false;
        Bullet_Playing = false;
        
    }

    public void Attack_Beam()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        if(target != null && (target.transform.position  - this.gameObject.transform.position).sqrMagnitude > 3)//一定距離離れていたら
        {
            Beam.transform.LookAt(target.transform.position);
            Beam_Playing = true;
            Beam.GetComponent<AttackParticle>().Attack();
            if (!Beam.GetComponent<AttackParticle>().PS2.isPlaying && !Beam.GetComponent<AttackParticle>().PS1.isPlaying)
                Beam_Playing = false;
        }
        
    }

    public void Attack_Bullet()
    {
        if(target != null &&(target.transform.position - transform.position).sqrMagnitude > 5)
        {
            Bullet.transform.LookAt(target.transform.position);
            Bullet_Playing = true;
            Bullet.GetComponent<AttackParticle>().Attack();
            if (!Bullet.GetComponent<AttackParticle>().PS2.isPlaying && !Bullet.GetComponent<AttackParticle>().PS1.isPlaying)
                Bullet_Playing = false;
        }
    }
}
public class MoleCommon : MonoBehaviour
{
    public float MaxX = 9, MinX = -9, MaxY = 5, MinY = -5, FixedZ = -7;
    public bool LStart, UpStart;
    public bool IsReflect;
    public float speed = 2;
    public Vector3 TORight = new Vector3(-90, 0, 0);
    public Vector3 TOLeft = new Vector3(90, 180, 0);
    public void WarpX()
    {
        if (this.gameObject.transform.position.x > MaxX)
            this.gameObject.transform.position = new Vector3(MinX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        if (this.gameObject.transform.position.x < MinX)
            this.gameObject.transform.position = new Vector3(MaxX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
    public void WarpY()
    {
        if (this.gameObject.transform.position.y > MaxY)
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, MinY, this.gameObject.transform.position.z);

        if (this.gameObject.transform.position.y < MinY)
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, MaxY, this.gameObject.transform.position.z);
    }
    public void Reflect()
    {
        IsReflect = !IsReflect;
    }
}

public class DefMole : MoleCommon
{

    public bool Setup()
    {
        LStart = UnityEngine.Random.Range(0, 2) % 2 == 0;
        speed = 2f;
        if (LStart)
        {
            this.gameObject.transform.position = new Vector3(MinX, UnityEngine.Random.Range(MinY, MaxX), FixedZ);
            this.gameObject.transform.localRotation = Quaternion.Euler(TORight);
            return true;
        }
        else
        {
            this.gameObject.transform.position = new Vector3(MaxX, UnityEngine.Random.Range(MinY, MaxY), FixedZ);
            this.gameObject.transform.localRotation = Quaternion.Euler(TOLeft);

            return false;
        }
    }
    public void Move( bool reflect)
    {
        WarpX();
        this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.Q))
            IsReflect = !IsReflect;
        if (reflect)
            this.gameObject.transform.localRotation = Quaternion.Euler(TORight);
        else
            this.gameObject.transform.localRotation = Quaternion.Euler(TOLeft);
    }
}

public class WaveMole : MoleCommon
{
    public bool Setup()
    {
        LStart = UnityEngine.Random.Range(0, 2) % 2 == 0;
        //LStart = true;
        if (LStart)
        {
            this.gameObject.transform.position = new Vector3(MinX, UnityEngine.Random.Range(MinY, MaxY), FixedZ);
            this.gameObject.transform.localRotation = Quaternion.Euler(TORight);
            return true;
        }
        else
        {
            this.gameObject.transform.position = new Vector3(MaxX, UnityEngine.Random.Range(MinY, MaxY), FixedZ);
            this.gameObject.transform.localRotation = Quaternion.Euler(TOLeft);
            return false;
        }
    }

    float seta;
    public void Move(bool reflect)
    {
        seta += 0.1f;
        float y = Mathf.Sin(seta);
        WarpX();
        WarpY();
        

        if (reflect)
            this.gameObject.transform.localRotation = Quaternion.Euler(TORight);
        else
            this.gameObject.transform.localRotation = Quaternion.Euler(TOLeft);
        this.gameObject.transform.Translate(new Vector3(1, 0, y* 4 ) * Time.deltaTime * speed);
    }
}

public class DiagonalMole : MoleCommon
{
    Vector3 dia_right = new Vector3(-135, -90, 90);
    Vector3 dia_left = new Vector3(45, -90, 90);
    public bool Setup()
    {
        LStart = UnityEngine.Random.Range(0, 2) % 2 == 0;
        UpStart = UnityEngine.Random.Range(0, 2) % 2 == 0;
        if (LStart)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(dia_right);
            
            if (UpStart)
                this.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), MaxY, FixedZ);
            else
                this.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), MinY, FixedZ);
            return true;
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(dia_left);
            if (UpStart)
                this.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), MaxY, FixedZ);
            else
                this.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), MinY, FixedZ);
            return false;

        }
    }
    public void Move(bool reflect)
    {
        WarpX();
        WarpY();
        this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (reflect)
            this.gameObject.transform.rotation = Quaternion.Euler(dia_right);
        else
            this.gameObject.transform.rotation = Quaternion.Euler(dia_left);
    }
}

public class UpDownMole : MoleCommon
{
    Vector3 up = new Vector3(0, -90, 90);
    Vector3 down = new Vector3(0, 90, -90);
    public bool Setup()
    {
        UpStart = UnityEngine.Random.Range(0, 2) % 2 == 0;
        if (UpStart)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(down);
            this.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), MaxY, FixedZ);
            return true;
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(up);
            this.gameObject.transform.position = new Vector3(UnityEngine.Random.Range(MinX, MaxX), MinY, FixedZ);
            return false;
        }

    }

    public void Move(bool reflect)
    {
        WarpY();
        this.gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.R))
            IsReflect = !IsReflect;
        if (reflect)
            this.gameObject.transform.rotation = Quaternion.Euler(down);
        else
            this.gameObject.transform.rotation = Quaternion.Euler(up);

    }
}

