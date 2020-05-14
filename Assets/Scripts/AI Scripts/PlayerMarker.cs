using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour
{
    GameObject Player;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        GameObject[] To_Tag = GameObject.FindGameObjectsWithTag("AI");

        foreach (GameObject obj in To_Tag)
        {
            obj.GetComponent<PlayerHolder>().Player = Player;
        }
    }
}
