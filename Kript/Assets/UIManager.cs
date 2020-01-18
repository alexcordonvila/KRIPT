using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public iTween.EaseType easyType;
    public GameObject[] ActionsGameobject;
    public int NumberofActions = 0;
    public int numberOfActionsLeft;
    public KeyCode DebugActionKey;
    public bool ispaused;


    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")) SusbtractActionFromUi();
    }

    public void SusbtractActionFromUi()
    {
        if (numberOfActionsLeft > 0)
        {
            ActionsGameobject[numberOfActionsLeft - 1].SetActive(false);
            numberOfActionsLeft--;
        }

        if (numberOfActionsLeft < 0)
        {

        }
    }

    public void settinguiActions()
    {
        for (int i = 0; i < ActionsGameobject.Length; i++)
        {
            ActionsGameobject[i].SetActive(true);
            //Debug.Log("ponemos todo a true");
        }

        for (int i = NumberofActions; i < ActionsGameobject.Length; i++)
        {
            ActionsGameobject[i].SetActive(false);
            // Debug.Log("ponemos todo a false menos actions");

        }
    }

    public void openPausemenu()
    {

        if (ispaused)
            iTween.MoveTo(pauseMenu, iTween.Hash("x", 0, "easeType", easyType, "delay", .1));

        if (!ispaused)
            iTween.MoveTo(pauseMenu, iTween.Hash("x", -Screen.width / 2, "easeType", easyType, "delay", .1));

    }

    public void onlcikccontinuebutton()
    {

    }
}
