using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaController : EnemyBehaviour
{
    [SerializeField] float moveSpeed=12;
    [SerializeField] float  waveSpeed=12;

    [SerializeField] float amplitude;

    public override void OnEnemyUpdate()
    {


        this.transform.Translate(new Vector2(this.transform.right.normalized.x * moveSpeed * Time.deltaTime, Mathf.Cos(Time.time * waveSpeed) * amplitude * Time.deltaTime), Space.World);



    }
}
