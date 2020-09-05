using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player variables")]
    [SerializeField] float speed = 10f;
    [SerializeField] float dashForce = 8f;
    [SerializeField] float timeBetweenDashes = 2f;
    Vector3 mouseWorldPositon;

    Rigidbody playerRB;
    Vector3 moveDirection;
    Animator playerAnim;
    int layerMask = 1 << 8;
    float dashTimer = Mathf.Infinity;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
      //  ControlRotation();
        SideDash();
    }
    private void Update()
    {
        dashTimer += Time.deltaTime;
    }


    private void Move()
    {

        moveDirection = new Vector3(MertInput.horizontal, 0.0f, MertInput.vertical);
        //Sikre sig at diagonal movement ikke er hurtigere
        if (moveDirection.magnitude >= 1)
        {
            moveDirection = moveDirection.normalized;
        }
        //Sætter base speed
        moveDirection *= speed;
        //Move
        playerRB.MovePosition(playerRB.position + moveDirection * Time.deltaTime);
    }

    private void ControlRotation()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        mouseWorldPositon = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
        transform.rotation = Quaternion.LookRotation(mouseWorldPositon, Vector3.up);
    }

    void SideDash()
    {
        if (Input.GetKeyDown(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) && CanDash())
        {
            dashTimer = 0f;
            playerRB.AddForce(transform.TransformDirection(Vector3.left) * dashForce, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftShift) && CanDash())
        {
            dashTimer = 0f;
            playerRB.AddForce(transform.TransformDirection(Vector3.right) * dashForce, ForceMode.Impulse);
        }
    }
    bool CanDash()
    {
        return dashTimer >= timeBetweenDashes;
    }
}
