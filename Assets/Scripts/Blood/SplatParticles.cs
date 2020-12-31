using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject splatPrefab;
 GameObject splatHolder;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();


    private void Awake()
    {
         splatHolder = new GameObject("SplatHolder");

        //  Destroy(this.gameObject, 4);
    }
    private void OnParticleCollision(GameObject other)
    {

        ParticlePhysicsExtensions.GetCollisionEvents(particles, other, collisionEvents);

        Debug.Log("COLLISIONBLOOD");
        foreach (var particleEvent in collisionEvents) {
            var splat = Instantiate(splatPrefab, particleEvent.intersection, Quaternion.identity) as GameObject;
            splat.transform.SetParent(splatHolder.transform, true);
            splat.GetComponent<BloodSplat>().Initialize(BloodSplat.SplatLocation.Foreground);

        }
    }


}
