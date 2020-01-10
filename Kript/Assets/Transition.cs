using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public float speed = 0.8f;
    public Vector3 initpos;


    void Start()
    {
        initpos = transform.position;
    }


    void DestroyScene()
    {
       
    }

    public void startTransition()
    {
       GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);


        iTween.MoveTo(this.gameObject, iTween.Hash("x", 35,
            "islocal", true, "easetype", iTween.EaseType.linear,
           "time", speed, "oncomplete", "ResetTrans"));
    }

    public void ResetTrans()
    {
        transform.position = initpos;
        GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);


    }
}