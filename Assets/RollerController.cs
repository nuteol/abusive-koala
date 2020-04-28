using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerController : MonoBehaviour
{
    public Roller roller;
    private Collider2D coll;
    private Transform controllerT;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        controllerT = GetComponent<Transform>();
        roller.enabled = false;
    }


    // Changed to fixed update for accuracy
    void FixedUpdate()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Avatar" && collision.gameObject.transform.position.x > controllerT.position.x)
        {
            coll.isTrigger = false;
            roller.enabled = true;
        }
    }
}
