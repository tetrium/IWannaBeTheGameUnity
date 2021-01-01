using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour,IDamageReporter
{
    [SerializeField] bool enemyIsActive = false;

    [SerializeField] int health = 1;
    [SerializeField] bool canBeDestroy;

    public void TakeDamage(int damagePoints)
    {
        health -= damagePoints;
        if (health <= 0) {

            if (canBeDestroy)
            {
                Destroy(this.gameObject);
            }
        }

    }

    void Update() {
        if (enemyIsActive) {
            OnEnemyUpdate();
        }
    }


    public abstract void OnEnemyUpdate();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !enemyIsActive) {
            enemyIsActive = true;
        }
    }


}
