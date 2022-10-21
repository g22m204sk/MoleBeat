using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inversecamera : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void OnPreCull()
    {
        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    void OnPreRender()
    {
        GL.invertCulling = true;
    }
    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}
