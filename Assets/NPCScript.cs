using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public string[] sentence;
    private BoxCollider2D coll;
    public DialogueScript dialogue;
    public string[] sentence2;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Avatar"))
        {
            //start diologue here
            dialogue.sentence = this.sentence;
            dialogue.BeginDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogue.QuitDialogue();
    }
    
}
