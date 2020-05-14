using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePackController : MonoBehaviour
{
    GameObject Player;

    GameObject[] Animals;

    [SerializeField]
    float MovementSpeed = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Animals = GameObject.FindGameObjectsWithTag("Pack");

    }

    private void Update()
    {
        CycleAttacks();
    }

    void CycleAttacks()
    {
        // When x number of enemies are in range, attack one by one
        foreach (GameObject animal in Animals)
        {
            if (Vector3.Distance(Player.transform.position, animal.transform.position) <= 8 && !animal.GetComponent<BasePackBehavController>().Attacked)
            {
                animal.GetComponent<BasePackBehavController>().Attack(Player.transform);

            }
            else if (AttackInProgress())
            {
                animal.GetComponent<BasePackBehavController>().CirclePlayer(Player.transform);
            }
            else
            {
                animal.transform.position = Vector3.MoveTowards(animal.transform.position, Player.transform.position, MovementSpeed * Time.deltaTime);
            }

        }

    }


    bool AttackInProgress()
    {
        foreach (GameObject animal in Animals)
        {
            if (animal.GetComponent<BasePackBehavController>().Attacking)
            {
                return true;
            }
        }
        return false;
    }

}
