using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaguParent : MonoBehaviour
{

    public float GizmoSize =  0.3f;
    Color GizmoColor = Color.yellow;

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(transform.position, GizmoSize);
    }

    
}
