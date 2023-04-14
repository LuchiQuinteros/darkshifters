using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float vidaEnemigo = 100;
    [SerializeField]
    //private GameObject spawn;
    private ParticleSystem _particleSystem;
    [SerializeField]
    Transform[] waypoints;
    Vector3 siguientePosicion;
    float velocidad = 2.0f;
    float distanciaChange = 0.5f;
    int numeroSiguientePosition = 0;
   
    private void Start()
    {
        siguientePosicion = waypoints[0].position;
    }

    private void OnTriggerEnter(Collider other) // Cuando el enemigo toca la layer 6 (cubo) (Layer llamada Attack) el enemigo pierde vida.
    {
        if (other.gameObject.layer == 6)
        {
            _particleSystem.Play();
            vidaEnemigo -= 10;
            int fraseGod = Random.Range(0, 3);

            switch (fraseGod)
            {
                case 0:
                    Debug.Log("Ahh, filho da puta");
                    //  StartCoroutine(spawnParticles());
                    break;

                case 1:
                    Debug.Log("Concha, me la pusiste");
                    //  StartCoroutine(spawnParticles());
                    break;

                case 2:
                    Debug.Log("Otia tio que dolor ahhh *Inserte frase generica de europeo*");
                    //  StartCoroutine(spawnParticles());
                    break;
            }
        }
    }

    private void Update()
    {
        if (vidaEnemigo <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, siguientePosicion, velocidad * Time.deltaTime);
        if (Vector3.Distance(transform.position, siguientePosicion) < distanciaChange)
        {
            numeroSiguientePosition++;
            if (numeroSiguientePosition >= waypoints.Length)
            {
                numeroSiguientePosition = 0;
            }
            siguientePosicion = waypoints[numeroSiguientePosition].position;
        }
    }
    // IEnumerator spawnParticles() //Esta funcion spawnea la cajita durante poco tiempo y luego la apaga.
    // {
    //     spawn.SetActive(true);
    //    yield return new WaitForSeconds(0.5f);

    //    spawn.SetActive(false);
 } 
