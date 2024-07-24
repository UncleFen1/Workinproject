using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float launchSpeed = 5f;
    public float minLaunchSpeed = 5f;
    public float maxFlightTime = 3;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            GameObject Bullet = Instantiate(bullet, transform.position, Quaternion.identity);

            Vector2 direction = (mousePosition - transform.position).normalized;

            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float speed = launchSpeed;
                if (direction.magnitude<minLaunchSpeed)
                {
                    direction = direction.normalized * minLaunchSpeed;
                }
                rb.velocity = direction * launchSpeed;
            }
            Destroy(Bullet, maxFlightTime);
        }
    }
}
   