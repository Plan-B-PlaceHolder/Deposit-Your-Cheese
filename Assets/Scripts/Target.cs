using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
