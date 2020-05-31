using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TGController : MonoBehaviour
{
    public TemperedGlass glass;
    private Collider2D coll;
    private Transform controllerT;
    public GameObject healthBarGameObject;
    public GameObject BgMusic;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        controllerT = GetComponent<Transform>();
        glass.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        healthBarGameObject.SetActive(true);
        BgMusic.SetActive(false);
        SoundManager.PlaySound("bossMusic");
        if (collision.tag == "Avatar" && collision.gameObject.transform.position.x > controllerT.position.x)
        {
            coll.isTrigger = false;
            glass.enabled = true;
        }
    }
}
