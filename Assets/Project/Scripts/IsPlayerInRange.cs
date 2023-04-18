using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInRange : MonoBehaviour
{
    public bool inRange = false;
    public Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        playerTransform = other.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
