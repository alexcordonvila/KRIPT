using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloader : MonoBehaviour
{
    public int CurrentScene;
    public int NextScene;
    public int previousScene;

    // Update is called once per frame

    private void Awake()
    {
        
    }

    void Update()
    {

        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCountInBuildSettings)
            NextScene = 1;
        else NextScene = SceneManager.GetActiveScene().buildIndex +1;

        if (SceneManager.GetActiveScene().buildIndex + 1 <= 0)
            previousScene = SceneManager.sceneCountInBuildSettings;
        else
            previousScene = SceneManager.GetActiveScene().buildIndex - 1;




        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextScene, LoadSceneMode.Additive);
        
       // SceneManager.SetActiveScene()
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
