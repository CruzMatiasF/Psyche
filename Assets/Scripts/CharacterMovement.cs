using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float movementSpeed = 2.0f;
    private float x, y;
    private float speedI, speedCrouch; //velocidad inicial y de agacharse

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        speedI = movementSpeed; //la vel inicial es igual al la vel de movimiento
        speedCrouch = movementSpeed * 0.5f; // la vel de agacharse es igual al 50% de la vel de mov
    }

    public void Move(Vector3 direction) //
    {
        //aplicamos l a velocidad s/la direccion
        Vector3 velocity = direction * movementSpeed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;
    }

    public void Crouch(bool isCrouching)
    {
        movementSpeed = isCrouching ? speedCrouch : speedI;  //cambio de velocidad cuando nos agachamos
    }
}
