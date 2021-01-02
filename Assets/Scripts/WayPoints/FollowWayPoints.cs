using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{

    [SerializeField] float speed = 1;
    [SerializeField] WayPointsManager wayPointsManager;
    private Vector2 currentPosition;


    private void Awake()
    {
        currentPosition = wayPointsManager.GetNextPoint();
    }

    void Update()
    {
        if (Vector2.Distance(this.transform.position, currentPosition) > 0.1f)
        {
            var direction =  currentPosition - (Vector2)this.transform.position;
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
        }
        else {
            currentPosition = wayPointsManager.GetNextPoint();

        }
    }
}
