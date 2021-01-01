using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : EnemyBehaviour
{
    [SerializeField] LayerMask blockMask;

    [SerializeField] float moveSpeed = 2;

    public override void OnEnemyUpdate()
    {

        this.transform.Translate(this.transform.right *Time.deltaTime* moveSpeed, Space.World);

        var blockTouched = Physics2D.Raycast(this.transform.position, this.transform.right, 0.5f, blockMask);

        if (blockTouched) {
            if (this.transform.rotation.eulerAngles.y == 0)
            {
                this.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                this.transform.rotation = Quaternion.Euler(0, 0, 0);

            }
        }
    }
}
