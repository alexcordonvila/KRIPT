using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class setactiveScene : MonoBehaviour
{
    public enum Scenes {Main, Scene1, Scene2, Scene3}

    public Scenes Setactivescene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Setactivescene == Scenes.Scene1) SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Scenes.Scene1));
        if (Setactivescene == Scenes.Scene2) SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Scenes.Scene2));
        if (Setactivescene == Scenes.Scene3) SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)Scenes.Scene3));
    }
}
