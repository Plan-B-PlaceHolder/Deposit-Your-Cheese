
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
    [SerializeField] private float climbingspeed = 2f;

    private Rigidbody rb;

    // Crouch
    private Vector3 standColliderCenter;
    private float slideforce = 5f;
    private CapsuleCollider playerCollider;
    private float standingSize;

    public float fallmulti = 3f;
    private float currentspeed = 0f;
    
    private bool isClimbing = false;
    private bool doubleJump = false;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        standingSize = playerCollider.height;
        standColliderCenter = playerCollider.center;
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsGrounded() == false && doubleJump == true)
        {
            Jump();
            doubleJump = false;
        }

        if(IsGrounded())
        {
            Jump();
            doubleJump = true;
        }

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
        if (isClimbing==false)
        {
            Move();
        }
        if (isClimbing==true)
        {
            Climb();
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(hAxis, 0, vAxis).normalized ;

        Vector3 newPosition = rb.position + rb.transform.TransformDirection(movement * currentspeed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);
    }

    private bool IsGrounded()
    {
        //Debug.DrawRay(transform.position, Vector3.down * jumpraycastDistance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, jumpraycastDistance);
    }

    private void crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            playerCollider.height = standingSize / 1.5f;
            playerCollider.center = standColliderCenter / 2;


            slide();
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            playerCollider.height = standingSize;
            playerCollider.center = standColliderCenter;
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
    //to be actually done (or not idk :( )
    private void Climb()
    {
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector3 upforce = new Vector3(0, vAxis, 0);
        Vector3 newPos = rb.position + rb.transform.TransformDirection(upforce * climbingspeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }
    //wtf is wrong with this
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Climable")
        {
            isClimbing = true;
            Debug.Log("Gay");
        }
    }
    void OnCollisionExit(Collider col)
    {
        if (col.gameObject.tag == "Climbable")
        {
            isClimbing = false;
            Debug.Log("Shit");
        }
    }
}
