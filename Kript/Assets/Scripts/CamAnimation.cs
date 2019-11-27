using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            iTween.ShakePosition(gameObject, iTween.Hash("x", 0.2f,"y", 0.2f, "time", 0.2f));
        }
    }
}
