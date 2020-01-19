using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public bool horizontal;
    public bool vertical;
    public iTween.EaseType EaseType;
        public
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
            transform.position = new Vector3(Mathf.PingPong(Time.time, 3f), this.transform.position.y, this.transform.position.z);
 
        
        
    }
    public void MoveDown()
    {
        //iTween.MoveTo(this.gameObject, iTween.Hash("x", 3,"y", 1,
        //       "islocal", true, "easetype", EaseType,
        //      "time", 5f));
        //iTween.MoveTo(this.gameObject, iTween.Hash("x", 3,"y", 4,
        //     "islocal", true, "easetype", EaseType,
        //            "time", 4f, "oncomplete", "MoveDown", "oncompletetarget", this.gameObject));
    }
    public void MoveUp()
    {
        //iTween.MoveTo(this.gameObject, iTween.Hash("y", 1,
        //    "islocal", true, "easetype", EaseType,
        //           "time", 4f, "oncomplete", "MoveDown", "oncompletetarget", this.gameObject));
    }

}
