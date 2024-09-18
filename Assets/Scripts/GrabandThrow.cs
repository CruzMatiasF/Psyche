using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabandThrow : MonoBehaviour
{
    
    public GameObject handPoint;
    private GameObject pickedObject = null;
    
    public float throwingForce = 5f; // Fuerza inicial de lanzamiento
    public float maxThrowingForce = 20f; // Max fuerza de lanzamiento
    public float forceIncreaseSpeed = 8f; // Velocidad fuerza
    private float currentThrowingForce = 0f; // Fuerza actual
    private bool isChargingThrow = false; 


    // Update is called once per frame
    void Update()
    {
        if (pickedObject != null)
        {
            // Si se presiona la tecla de espacio se empieza a cargar la fuerza para lanzar
            if (Input.GetKey(KeyCode.Space))
            {
                isChargingThrow = true;
                currentThrowingForce = Mathf.Min(currentThrowingForce + Time.deltaTime * forceIncreaseSpeed, maxThrowingForce);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                // Si se suelta la tecla se lanza el objeto
                if (isChargingThrow)
                {
                    ThrowObject();
                    isChargingThrow = false;
                    currentThrowingForce = throwingForce; // Restablece la fuerza a la inicial
                }
            }

            // si se presiona E y se esta sosteniendo un objeto y se suelta el objeto
            if (Input.GetKeyDown(KeyCode.E))
            {
                ReleaseObject();
            }
        }
        else
        {
            // Si no se esta sosteniendo nda se intenta agarrar uno
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickupObject();
            }
        }
    }

    void TryPickupObject()
    {
        // Detecta el objeto en el rango de colision
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Objetos"))
            {
                pickedObject = collider.gameObject;
                pickedObject.GetComponent<Rigidbody>().useGravity = false;
                pickedObject.GetComponent<Rigidbody>().isKinematic = true;

                pickedObject.transform.position = handPoint.transform.position;
                pickedObject.transform.SetParent(handPoint.transform);
                break;
            }
        }
    }

    void ReleaseObject()
    {
        pickedObject.GetComponent<Rigidbody>().useGravity = true;
        pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        pickedObject.transform.SetParent(null);

        if (isChargingThrow)
        {
            ThrowObject();
        }
        else
        {
            pickedObject = null;
        }
    }

    void ThrowObject()
    {
        Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * currentThrowingForce, ForceMode.Impulse);

        pickedObject.transform.SetParent(null);
        pickedObject = null;
    }
}


