using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSphre : MonoBehaviour
{
    public float MaxX, MaxY, MinX, MinY,Emission;
    public Material mat;
    void Start()
    {
        GameObject HPParent = new GameObject("HPparent");
        for (float j = MinX; j <= MaxX; j+=Emission)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.3f;
            sphere.transform.position = new Vector3(j, MaxY, -9);

            sphere.GetComponent<MeshRenderer>().material = mat;

            sphere.transform.parent = HPParent.transform;
        }
        for (float j = MaxY; j >= MinY; j-=Emission)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.3f;
            sphere.transform.position = new Vector3(MaxX,j, -9);

            sphere.GetComponent<MeshRenderer>().material = mat;

            sphere.transform.parent = HPParent.transform;
        }
        for (float j = MaxX; j >= MinX; j-=Emission)
        {
            Debug.Log(j);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.3f;
            sphere.transform.position = new Vector3(j, MinY, -9);

            sphere.GetComponent<MeshRenderer>().material = mat;

            sphere.transform.parent = HPParent.transform;
        }
        for (float j = MinY; j <= MaxY; j+=Emission)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.localScale = Vector3.one * 0.3f;
            sphere.transform.position = new Vector3(MinX,  j, -9);

            sphere.GetComponent<MeshRenderer>().material = mat;

            sphere.transform.parent = HPParent.transform;
        }

    }
}
