using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject Door;
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        Door = GameObject.Find("Door");
        anim = Door.GetComponent<Animation>();
        GameObject.Find("Door").GetComponent<Animator>().SetBool("OpenDoor", false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            this.transform.gameObject.SetActive(false);
            GameObject.Find("Door").GetComponent<Animator>().SetBool("OpenDoor", true);
            GameObject.Find("Door").GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
