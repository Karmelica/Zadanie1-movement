using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform mech;

    void Update()
    {
         transform.position = mech.position;
         transform.rotation = mech.rotation;
    }
}
