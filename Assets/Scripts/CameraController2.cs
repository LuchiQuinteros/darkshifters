using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;
    public float orbitSpeed = 10f;
    public Vector3 offset;
    private Vector3 _velocity = Vector3.zero;
    private bool _isOrbiting = false;
    private float _horizontalInput = 0f;

    void Start()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(target.position);
    }

    void Update()
    {
        // Comprobamos si se está presionando el botón derecho del mouse
        if (Input.GetMouseButtonDown(1))
        {
            _isOrbiting = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _isOrbiting = false;
        }

        // Si se está orbitando, actualizamos la entrada horizontal del mouse
        if (_isOrbiting)
        {
            _horizontalInput = Input.GetAxis("Mouse X") * orbitSpeed;
        }
        else
        {
            _horizontalInput = 0f;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (_isOrbiting)
            {
                // Rotamos la posición de la cámara alrededor del objeto de destino
                Quaternion rotation = Quaternion.Euler(0, _horizontalInput, 0);
                offset = rotation * offset;
            }

            // Actualizamos la posición de la cámara
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
            transform.LookAt(target.position);
        }
    }
}
