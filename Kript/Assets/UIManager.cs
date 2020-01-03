using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject ActionParentPanel;
    public GameObject[] ActionsGameobject;
    public int NumberofActions = 0;
    public int numberOfActionsLeft;
    public KeyCode DebugActionKey;


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
    }

    public void settinguiActions()
    {
        for (int i = 0; i < ActionsGameobject.Length; i++)
        {
            ActionsGameobject[i].SetActive(true);
            Debug.Log("ponemos todo a true");
        }

        for (int i = NumberofActions; i < ActionsGameobject.Length; i++)
        {
            ActionsGameobject[i].SetActive(false);
            Debug.Log("ponemos todo a false menos actions");

        }
    }
}
