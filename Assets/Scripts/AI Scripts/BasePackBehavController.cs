using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePackBehavController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float DashSpeed = 0;

    [SerializeField]
    float MinAngle = 0;

    public bool Attacked = false;
    public bool Attacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Attack(Transform playerPos)
    {
        // Face player and dash towards them dealing damage
        Attacking = true;
        transform.LookAt(playerPos);
        rb.AddForce(transform.forward * DashSpeed, ForceMode.Impulse);
        Attacked = true;
        StartCoroutine("ResetAttack");


        }


    public void CirclePlayer(Transform playerPos)
    {
        // TO DO
    }


    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(2.0f);
        Attacked = false;
        Attacking = false;

    }

}
