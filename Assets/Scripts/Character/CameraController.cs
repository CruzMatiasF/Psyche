using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public new Transform camera;
    public Vector2 sensitivity;

    public void RotateCamera(float hor, float ver)
    {
        //rotacion hor del pj
        if (hor != 0) {
            transform.Rotate(0, hor * sensitivity.x, 0);
        }
        //rotacion ver del pj
        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensitivity.y + 360) % 360;
            //restriccion de rotacion vertical por angulos
            if (rotation.x > 60 && rotation.x < 180) rotation.x = 60;
            else if (rotation.x < 300 && rotation.x > 180) rotation.x = 300;

            camera.localEulerAngles = rotation;
        }
    }
}

