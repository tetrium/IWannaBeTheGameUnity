using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDeliver : MonoBehaviour
{

    [SerializeField] int damagePoints = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyDamage(collision.gameObject);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ApplyDamage(collision.gameObject);

    }
    private void ApplyDamage(GameObject target) {
        var implementation = target.GetComponent<IDamageReporter>();
        if (implementation != null) {
            implementation.TakeDamage(damagePoints);
        }
    }
}
