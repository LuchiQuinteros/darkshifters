using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuHa : MonoBehaviour
{

    [SerializeField]
    private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    public Rigidbody rb;
    private Animator anim;
    Vector3 movement = Vector3.zero;

    [SerializeField] private Transform basicSpawn;
    [SerializeField] GameObject basicPrefab;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float fireTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        fireTimer = fireRate;
    }

    void Update()
    {
        BasicMovement();
        MouseTracking();

        fireTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireTimer >= fireRate)
        {
            StartCoroutine(Shoot());
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * _movementSpeed * Time.deltaTime);
    }
    void BasicMovement()
    {
        movement.x = movement.z = 0;
        if (Input.GetKey(KeyCode.A)) movement.x = -1f;
        if (Input.GetKey(KeyCode.D)) movement.x = 1f;
        if (Input.GetKey(KeyCode.W)) movement.z = 1f;
        if (Input.GetKey(KeyCode.S)) movement.z = -1f;
        anim.SetBool("isRunning", movement.magnitude > 0);

        if (movement.x != 0 && movement.z != 0)
        {
            movement = movement.normalized;
        }
    }

    void MouseTracking()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0f;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator Shoot()
    {
        _rotationSpeed = 0;
        _movementSpeed = 0;
        Instantiate(basicPrefab, basicSpawn.transform.position, basicSpawn.transform.rotation);
        anim.SetTrigger("isAttacking");
        fireTimer = 0f;
        yield return new WaitForSeconds(0.5f);
        _rotationSpeed = 6;
        _movementSpeed = 5.6f;
    }

}
