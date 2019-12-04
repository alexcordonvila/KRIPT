using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    public uiManager uiM;
    public GameObject[] golist;


    // Start is called before the first frame update
    void Start()
    {
        golist = GameObject.FindGameObjectsWithTag("UIManager");
        uiM = golist[0].GetComponent<uiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActionDone()
    {
        uiM.SusbtractActionFromUi();

    }
}
