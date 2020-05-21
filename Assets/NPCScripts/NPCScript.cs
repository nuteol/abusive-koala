using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public string[] sentence;
    private BoxCollider2D coll;
    public DialogueScript dialogue;
    public string[] sentence2;
    public int weapon;

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
            //For now we do this
            if(weapon == 1)
            {
                collision.gameObject.GetComponent<attackingscript>().UnlockWeapon1();
            }
            if (weapon == 2)
            {
                collision.gameObject.GetComponent<attackingscript>().UnlockWeapon2();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogue.QuitDialogue();
    }
}
