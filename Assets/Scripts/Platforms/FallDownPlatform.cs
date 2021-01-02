using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDownPlatform : MonoBehaviour
{
    [SerializeField] float speed = 12;

    private bool active = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !active) {
            StartCoroutine(StartActivation());
        }
    }

    IEnumerator StartActivation() {
        yield return new WaitForSeconds(0.4f);
        active = true;
    }
    void Update()
    {
        if (active) {
            this.transform.Translate(Time.deltaTime * speed * Vector2.down, Space.World);
        }
    }
}
