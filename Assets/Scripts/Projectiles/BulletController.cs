using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{


 
    public void Shoot(Vector2 force) {
        float angle = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);

        GetComponent<Rigidbody2D>().velocity = force;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    void HandleCollision(GameObject collisionObject) {
        if (collisionObject.gameObject.tag.Equals("Destructible"))
        {
            var implementation = collisionObject.GetComponent<IDamageReporter>();
            if (implementation != null)
            {
                implementation.TakeDamage(1);
                Destroy(this.gameObject);
            }

        }
        else if (!collisionObject.gameObject.tag.Equals("Player"))
        {
            var velocity = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.normalized.x * -1, 1) * velocity.magnitude;

        }
    }


}
