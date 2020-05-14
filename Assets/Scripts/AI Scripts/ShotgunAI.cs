using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAI : MonoBehaviour
{
    [SerializeField] [Range(0, 30f)]
    float MaxDistance = 15f;

    [SerializeField] [Range(0f, 15f)]
    float Speed = 1f;

    GameObject player;

    void Start()
    {
        player = GetComponent<PlayerHolder>().Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > MaxDistance) MoveToPlayer();
    }


    void MoveToPlayer()
    {
        transform.LookAt(player.transform);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
    }
}
