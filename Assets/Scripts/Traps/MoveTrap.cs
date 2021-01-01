using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveTrapDirection { 
    Right=1,
    Left=2,
    Up=3,
    Down=4
}
public class MoveTrap : MonoBehaviour
{

    
    MoveTrapDirection moveTrapDirection;

    private bool ableToMove = false;
    private float returnDelay;

    private float speed;
    private float returnSpeed;
    Vector2 direction;

    private bool returnToOriginalPosition = false;

    Vector2 initPos;
    
    private void Awake()
    {
        initPos = this.transform.position;
    }
    public void ReturnMove(float returnDelay,float returnSpeed) {
        this.returnDelay = returnDelay;
        this.returnSpeed = returnSpeed;
        StartCoroutine(_ReturnMove());
    }

    IEnumerator _ReturnMove() {
        yield return new WaitForSeconds(this.returnDelay);
        direction *= -1;
        speed = returnSpeed;
        returnToOriginalPosition = true;

    }
    public void StartMove(MoveTrapDirection moveTrapDirection,float speed) {
        if (moveTrapDirection == MoveTrapDirection.Down) {
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
        ableToMove = true;
        this.speed = speed;
    }
    void Update()
    {
        if (ableToMove) {
            if (returnToOriginalPosition && Vector2.Distance(initPos, this.transform.position)<0.1f) {
                ableToMove = false;
            }
            this.transform.Translate(Time.deltaTime * speed * direction, Space.World);
        }
        
    }
}
