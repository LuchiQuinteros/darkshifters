using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScript2 : MonoBehaviour
{
    [SerializeField] 
    private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject spawnAttack;

    private bool canDash = true;

    [SerializeField] private float dashSpeed = 30f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 2f;


    public Rigidbody rb;
    private Animator anim;
    Vector3 movement = Vector3.zero;

    [SerializeField] private float attackCooldown = 1f;
    private bool canAttack = true;




    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        BasicMovement();
        MouseTracking();

        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(spawnAttackBox());
            StartCoroutine(AttackCooldown());
            //BasicAttack();
        }
        if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());

        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * _movementSpeed * Time.deltaTime);
    }

    IEnumerator Dash()
    {
        canDash = false;
        rb.AddForce(movement.normalized * dashSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector3.zero;
        print("Cooldown Iniciado\n");
        yield return new WaitForSeconds(dashCooldown);
        print("Cooldown Finalizado\n");
        canDash = true;
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

    IEnumerator spawnAttackBox()
    {
        spawnAttack.SetActive(true);
        _rotationSpeed = 0;
        _movementSpeed = 0;
        anim.SetTrigger("isAttacking");
        yield return new WaitForSeconds(0.5f);

        spawnAttack.SetActive(false);
        _rotationSpeed = 6;
        _movementSpeed = 5.6f;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            spawnAttack.SetActive(false);
        }
    }

    // private void BasicAttack()
    //{
    //  Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //  RaycastHit hit;

    // if (Physics.Raycast(ray, out hit))
    // {
    //     Vector3 targetPosition = hit.point;
    //   targetPosition.y = transform.position.y;

    // transform.LookAt(targetPosition);
    // anim.SetTrigger("isAttacking");
    // }
    //  }

}
