using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItweens : MonoBehaviour
{


    public Vector3 currentposition;
    public iTween.EaseType easyType;
    public GameObject startgo, exitgo, optionsgo, optionsPanel, exitpanel, scenemanager;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnclickStartGame()
    {

        iTween.MoveTo(startgo, iTween.Hash("x", Screen.width +200, "easeType", easyType,  "delay", .1));
        iTween.MoveTo(optionsgo, iTween.Hash("x", Screen.width + 200, "easeType", easyType, "delay", .3));
        iTween.MoveTo(exitgo, iTween.Hash("x", Screen.width +200, "easeType", easyType,  "delay", .6, "oncomplete", "NextScene",
        "oncompletetarget", scenemanager));
        

    }

    public void OnclickOptions()
    {

        iTween.MoveTo(startgo, iTween.Hash("x", Screen.width + 200, "easeType", easyType, "delay", .3));
        iTween.MoveTo(optionsgo, iTween.Hash("x", Screen.width + 200, "easeType", easyType, "delay", .1));
        iTween.MoveTo(exitgo, iTween.Hash("x", Screen.width + 200, "easeType", easyType, "delay", .6));
        iTween.MoveTo(optionsPanel, iTween.Hash("y", Screen.height/2  , "easeType", easyType, "delay", .6));

    }

    public void OnclickExit()
    {
        iTween.MoveTo(startgo, iTween.Hash("x", Screen.width + 200, "easeType", easyType, "delay", .6));
        iTween.MoveTo(optionsgo, iTween.Hash("x", Screen.width + 200, "easeType", easyType, "delay", .3));
        iTween.MoveTo(exitgo, iTween.Hash("x", Screen.width + 200, "easeType", easyType, "delay", .1));
        iTween.MoveTo(exitpanel, iTween.Hash("y", Screen.height / 2, "easeType", easyType, "delay", .6));

    }

    public void OnclickBackexitmenu()
    {
        iTween.MoveTo(startgo, iTween.Hash("x", Screen.width / 2, "easeType", easyType, "delay", .6));
        iTween.MoveTo(optionsgo, iTween.Hash("x", Screen.width / 2, "easeType", easyType, "delay", .3));
        iTween.MoveTo(exitgo, iTween.Hash("x", Screen.width / 2, "easeType", easyType, "delay", .1));
        iTween.MoveTo(exitpanel, iTween.Hash("y", -150, "easeType", easyType, "delay", .6));
    }

    public void OnclickBackoptionsmenu()
    {

        iTween.MoveTo(startgo, iTween.Hash("x", Screen.width /2, "easeType", easyType, "delay", .6));
        iTween.MoveTo(optionsgo, iTween.Hash("x", Screen.width /2, "easeType", easyType, "delay", .3));
        iTween.MoveTo(exitgo, iTween.Hash("x", Screen.width /2, "easeType", easyType, "delay", .1));
        iTween.MoveTo(optionsPanel, iTween.Hash("y", -450, "easeType", easyType, "delay", .6));

    }
}
