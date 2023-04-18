using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKuHa : MonoBehaviour
{
    [SerializeField] private float speed = 16f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private float damage = 10f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
            
        }
        if(collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
