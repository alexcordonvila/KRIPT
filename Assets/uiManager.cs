using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiManager : MonoBehaviour
{
    public GameObject ActionParentPanel;
    public GameObject[] ActionsGameobject;
    public int NumberofActions = 0;
    public int numberOfActionsLeft;


    void Start()
    {
        for (int i = NumberofActions; i < ActionsGameobject.Length; i++)
        {
            ActionsGameobject[i].SetActive(false);
        }

        numberOfActionsLeft = NumberofActions;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SusbtractActionFromUi();
    }

    public void SusbtractActionFromUi()
    {
        if (numberOfActionsLeft > 0)
        {
            ActionsGameobject[numberOfActionsLeft-1].SetActive(false);
            numberOfActionsLeft--;
        }
    }
}
