using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public float speed = 0.8f;
    void Start()
    {
        iTween.MoveTo(this.gameObject, iTween.Hash("x", 35,
                    "islocal", true, "easetype", iTween.EaseType.linear,
                   "time", speed, "oncomplete", "DestroyScene"));
    }
    void DestroyScene()
    {
        SceneManager.UnloadSceneAsync("Transition");
    }
}
