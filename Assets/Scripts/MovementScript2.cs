using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScript2 : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private GameObject spawnAttack; //Para declarar a la caja

    Vector3 movement = Vector3.zero;

    #region variablesDash
    // variables para el dash: duracion, velocidad, tiempo de cooldown y creacion del vector de direccion que se inicia en 0 en x,y,z.
    [SerializeField]
    public float dashDuration = 0.5f;
    [SerializeField]
    public float dashSpeed = 1f;
    [SerializeField]
    public float dashCooldown = 1f;
    private Vector3 dashDirection = Vector3.zero;
    private bool isDashing = false;
    private bool canDash = true;
    public Rigidbody rb;
    private bool canAttack = true;
    [SerializeField]
    private float attackCooldown = 1f;

    #endregion

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
     
       
        BasicMovement(); 
        MouseTracking();
        #region Dash
        // Chequea el input del dash y la direccion del mouse en ese momento, llama a las funciones para mover el personaje y activar el cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDirection = transform.forward;
        }
        if (!isDashing && canDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
            StartCoroutine(DashCooldown());
        }
        #endregion
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(spawnAttackBox());
            StartCoroutine(AttackCooldown());
            //BasicAttack();
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

    #region Dash
    // Mueve el personaje hasta que termine la duracion del dash.
    IEnumerator Dash()
    {
        isDashing = true;
        float dashTime = 0f;
        while (dashTime < dashDuration)
        {
            dashTime += Time.deltaTime;
            rb.AddForce(dashDirection * dashSpeed);
            yield return null;
        }
        isDashing = false;
        rb.velocity = Vector3.zero;
    }
    // Aplica el cooldown para que no puedas volver a usarlo hasta pasado un tiempo.
    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    #endregion

     IEnumerator spawnAttackBox() //Esta funcion spawnea la cajita durante poco tiempo y luego la apaga.
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

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.layer == 7)
       {
           spawnAttack.SetActive(false);
       }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
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
