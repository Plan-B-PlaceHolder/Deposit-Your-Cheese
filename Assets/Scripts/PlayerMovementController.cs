using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float jumpraycastDistance = 1.6f;
    [SerializeField] private float sprintspeed = 2f;


    private Rigidbody rb;

    // Crouch
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 standScale;
    private float slideforce = 5f;

    public LayerMask groundMask;
    public float fallmulti = 3f;
    private float currentspeed = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        standScale = transform.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        Jump();

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallmulti - 1) * Time.deltaTime;
        }
        crouch();



        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentspeed = sprintspeed;
        }
        else
        {
            currentspeed = speed;

        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);                
            }
        }
    }

    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * currentspeed * Time.fixedDeltaTime;

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement);

        rb.MovePosition(newPosition);
    }

    private bool IsGrounded()
    {
        //Debug.DrawRay(transform.position, Vector3.down * jumpraycastDistance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, jumpraycastDistance, groundMask);
    }

    private void crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            slide();
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = standScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }

    }

    private void slide()
    {
        if (rb.velocity.magnitude > 0.5f)
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector3.forward * slideforce);
            }
        }

    }

}
