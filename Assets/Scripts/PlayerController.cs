using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CameraController cameraController;
    public CharacterAnimation characterAnimation;

    public Transform cameraTransform;


    void Start()
    {
        // Asignar el transform de la camara al CharacterMovement
        characterMovement.cameraTransform = cameraTransform;
    }
    void Update()
    {
        // Movimiento del personaje
        // Inputs para los movimientos
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 direction = (transform.forward * y + transform.right * x).normalized;
        characterMovement.Move(direction); //pj se mueve

        // Crouch
        bool isCrouching = Input.GetKey(KeyCode.LeftShift);
        characterMovement.Crouch(isCrouching);

        // Animaciones
        characterAnimation.UpdateAnimation(x, y, isCrouching);

        // Movimiento de la camara
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");
        cameraController.RotateCamera(hor, ver);
    }
}

