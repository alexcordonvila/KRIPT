using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("toco puerta " + other.name);
        if (other.tag == "Player")
        {
            Debug.Log("toco puerta");
            other.GetComponent<Linker>().LoadNextScene();
        }
    }
}
