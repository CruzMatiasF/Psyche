using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider; // Referencia al CapsuleCollider
    public float movementSpeed = 2.0f;
    private float x, y;
    private float speedI, speedCrouch; //velocidad inicial y de agacharse

    public float crouchHeight = 0.5f; // Altura del collider al agacharse
    public float standHeight = 1.0f;  // Altura del collider de pie
    private Vector3 standCenter; // Centro del collider de pie
    private Vector3 crouchCenter; // Centro del collider agachado

    public Transform cameraTransform; // Referencia al Transform de la camara
    public Vector3 crouchCameraOffset = new Vector3(0, -0.3f, 0.25f); // Ajuste del desplazamiento 
    private Vector3 initialCameraPosition; // Pos inicial de la camara

    //cosas del wwise//
    [Header("Wwise Events")]
    public AK.Wwise.Event myFootstep;
    private float footstepTimer = 0f; // Temporizador para controlar la frecuencia de pasos
    public float footstepInterval = 0.5f; // Intervalo mínimo entre pasos
    private bool isFootstepPlaying = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        speedI = movementSpeed; //la vel inicial es igual al la vel de movimiento
        speedCrouch = movementSpeed * 0.5f; // la vel de agacharse es igual al 50% de la vel de mov

        standHeight = capsuleCollider.height;
        standCenter = capsuleCollider.center;
        crouchHeight = standHeight * 0.5f;
        crouchCenter = new Vector3(0, crouchHeight / 2, 0);

        if (cameraTransform != null)
        {
            initialCameraPosition = cameraTransform.localPosition;
        }
    }

    public void Move(Vector3 direction) //
    {
        //aplicamos l a velocidad s/la direccion
        Vector3 velocity = direction * movementSpeed;
        velocity.y = rigidbody.velocity.y;
        rigidbody.velocity = velocity;

        //comparacion para los sonidos del paso
        // Reproducir el sonido de pasos solo si hay movimiento
        if (direction.magnitude > 0)
        {
            if (!isFootstepPlaying)
            {
                myFootstep.Post(gameObject); // Reproduce el sonido de pasos
                isFootstepPlaying = true; // Marcar que el sonido se está reproduciendo
            }

            footstepTimer += Time.deltaTime; // Aumentar el temporizador

            // Controlar la frecuencia de pasos
            if (footstepTimer >= footstepInterval)
            {
                footstepTimer = 0f; // Reiniciar el temporizador
                isFootstepPlaying = false; // Permitir que el sonido se reproduzca nuevamente
            }
        }
        else
        {
            isFootstepPlaying = false; // Reiniciar si no hay movimiento
        }
    }

    public void Crouch(bool isCrouching)
    {
        movementSpeed = isCrouching ? speedCrouch : speedI;  //cambio de velocidad cuando nos agachamos

        // Ajustar la pos de la camara al agacharse
        if (cameraTransform != null)
        {
            cameraTransform.localPosition = isCrouching ? initialCameraPosition + crouchCameraOffset : initialCameraPosition;
        }

        if (isCrouching)
        {
            capsuleCollider.height = crouchHeight;  // Cambia la altura del collider al agacharse
            capsuleCollider.center = crouchCenter;  // Cambia el centro del collider
        }
        else
        {
            capsuleCollider.height = standHeight;  // Vuelve a la altura original al estar de pie
            capsuleCollider.center = standCenter;  // Vuelve al centro original
        }
    }
}
