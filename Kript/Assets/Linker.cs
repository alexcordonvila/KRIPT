using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Linker : MonoBehaviour
{
    public UIManager uiM;
    public GameObject[] golist;
    public bool hasactionleft;
    public int numberofactionlevel;
    public int NumberActionsleft;
    public TextMeshProUGUI textActions;
    public Vector3 initposition;
    public bool ispaused;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("seteamos las actiones del nivel");
        golist = GameObject.FindGameObjectsWithTag("UIManager");

        initposition = transform.position;

        uiM = golist[0].GetComponent<UIManager>();

        uiM.NumberofActions = numberofactionlevel;
        uiM.numberOfActionsLeft = numberofactionlevel;

        uiM.settinguiActions();
    }

    // Update is called once per frame
    void Update()
    {

        uiM.ispaused = ispaused;


        if (uiM.numberOfActionsLeft > 0) hasactionleft = true;
        else hasactionleft = false;

        NumberActionsleft = uiM.numberOfActionsLeft;
        //textActions.text = NumberActionsleft.ToString();


        

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            ispaused = !ispaused;
            uiM.openPausemenu();
        }

    }

    public void ActionDone()
    {
        uiM.SusbtractActionFromUi();

    }

    public void ResetUI()
    {
        transform.position = initposition;
        uiM.NumberofActions = numberofactionlevel;
        uiM.numberOfActionsLeft = numberofactionlevel;

        uiM.settinguiActions();

    }
}
