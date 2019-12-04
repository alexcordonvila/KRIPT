using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject dot;
    public int numMovements;
    public int MAXMOVEMENTS;
    private GameObject dottemp;
    public bool jumpFlag;

    // Start is called before the first frame update
    void Start()
    {
        numMovements = player.GetComponent<PlayerBehaviour>().numMovements;
        
        //for (int i = 0; i < MAXMOVEMENTS; i++)
        //{
        //    //object1.transform.parent = object2;
        //    dottemp = Instantiate(dot, new Vector2(this.transform.position.x + i, this.transform.position.y), Quaternion.identity);
        //    dottemp.transform.parent = this.transform;
        //}
        jumpFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        numMovements = player.GetComponent<PlayerBehaviour>().numMovements;
       // Debug.Log(numMovements + " , "+ MAXMOVEMENTS);
        if (Input.GetButtonDown("Jump") && numMovements<MAXMOVEMENTS)
        {
            //dottemp = this.gameObject.transform.GetChild(this.transform.childCount - 1).gameObject;
            //Destroy(dottemp);
        }
        if (numMovements >= MAXMOVEMENTS)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
