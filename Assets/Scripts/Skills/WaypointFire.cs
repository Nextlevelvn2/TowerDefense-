using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFire : MonoBehaviour
{
    public int waypointHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "fire")
        {
            gameObject.GetComponent<Animator>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject, .7f);
        }
    }
}
