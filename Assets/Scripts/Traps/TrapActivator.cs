using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    [SerializeField] float returnDelay = -1;

    [SerializeField] MoveTrapDirection moveTrapDirection;
    [SerializeField] float speed=12;
    [SerializeField] float returnSpeed = 12;

    [SerializeField] MoveTrap moveTrap;

    private bool active = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")&&!active) {

            active = true;
            moveTrap.StartMove(moveTrapDirection, speed);
            if (returnDelay > 0) {
                moveTrap.ReturnMove(returnDelay,returnSpeed);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        var direction = Vector2.zero;
        if (moveTrapDirection == MoveTrapDirection.Down)
        {
            direction = Vector2.down;

        }
        if (moveTrapDirection == MoveTrapDirection.Up)
        {
            direction = Vector2.up;

        }
        if (moveTrapDirection == MoveTrapDirection.Right)
        {
            direction = Vector2.right;

        }
        if (moveTrapDirection == MoveTrapDirection.Left)
        {
            direction = Vector2.left;

        }
        Gizmos.color = Color.red;
        Gizmos.DrawRay(moveTrap.transform.position, direction * speed);

    }
#endif
}
