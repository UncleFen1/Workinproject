using UnityEngine;

// TODO _j Andrey, seems like the same script is used for shooting Scripts/Player/PlayerBulletScript.cs, so effects applied incorrectly
public class EnemyBullet : MonoBehaviour
{
    public int damage = 20;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth PlayerHealth = other.GetComponent<PlayerHealth>();
            if (PlayerHealth != null)
            {
                PlayerHealth.TakePlayerDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}