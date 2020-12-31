using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuyControler: MonoBehaviour
{
    
    [SerializeField] float speed = 8;
 

    private Vector2 axisDirection;
    private Vector2 lastAxisDirection;
    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {


     
        GetControls();
        UpdateMovement();
        UpdateLookDirection();
       
    }

 
    void GetControls()
    {
        axisDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Mathf.Abs(axisDirection.x) > 0)
        {
            lastAxisDirection.x = axisDirection.x;
        }
        if (Mathf.Abs(axisDirection.y) > 0)
        {
            lastAxisDirection.y = axisDirection.y;
        }
    }
  
    void UpdateMovement()
    {
        _rigidBody2D.velocity = new Vector2(axisDirection.x * speed, _rigidBody2D.velocity.y);
      
    }

    void UpdateLookDirection()
    {
        if (lastAxisDirection.x >= 0)
        {
            this.transform.rotation = Quaternion.identity;
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }

 
}
