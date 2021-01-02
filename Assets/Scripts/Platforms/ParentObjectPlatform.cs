using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObjectPlatform : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            collision.gameObject.transform.parent = this.transform;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.transform.parent = null;
            DontDestroyOnLoad(collision.gameObject);
        }
    }
}
