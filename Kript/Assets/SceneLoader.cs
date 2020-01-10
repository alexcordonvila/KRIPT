using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [Header("Scene state")]
    public int backScene;
    public int currentScene;
    public int nextScene;
    public Transition tr;
    private int _managerScene;
    private int _sceneCountInBuildSettings;


    // Start is called before the first frame update
    void Start()
    {
        UpdateSceneState();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.AltGr))
        {
            if (Input.GetKeyDown(KeyCode.N)) NextScene();
            if (Input.GetKeyDown(KeyCode.B)) BackScene();
            if (Input.GetKeyDown(KeyCode.V)) ResetScene();
        }

    }
    public void UpdateSceneState()
    {
        
        _sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;

        _managerScene = 0;
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0) backScene = _sceneCountInBuildSettings - 1;
        else backScene = currentScene - 1;

        if (currentScene >= _sceneCountInBuildSettings - 1) nextScene = 1;
        else nextScene = currentScene + 1;
    }
    public void NextScene()
    {
        //if next scene is Not loaded, load next scene
        if (!SceneManager.GetSceneByBuildIndex(nextScene).isLoaded)
        {
            tr.startTransition();
            SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
        }

        if (SceneManager.GetSceneByBuildIndex(backScene).isLoaded)
        {
            tr.startTransition();
            SceneManager.UnloadSceneAsync(backScene);
        }

 

        nextScene++;
        backScene++;
        if (backScene >= _sceneCountInBuildSettings) backScene = 1;
        if (nextScene >= _sceneCountInBuildSettings) UpdateSceneState();




        }
    public void BackScene()
    {
        //if back scene is Not loaded, load next scene
        if (!SceneManager.GetSceneByBuildIndex(backScene).isLoaded) SceneManager.LoadScene(backScene, LoadSceneMode.Additive);

        currentScene = backScene;


        if (SceneManager.GetSceneByBuildIndex(nextScene).isLoaded) SceneManager.UnloadSceneAsync(nextScene);
        backScene--;
        nextScene = backScene + 1;
        if (backScene == 0) UpdateSceneState();

    }
    public void ResetScene()
    {
        NextScene();
        BackScene();
        
    }
    public void QuitGame()
    {
        SceneManager.LoadScene(-1);
    }

    public bool IsLastScene()
    {
        if (currentScene == _managerScene) return true;
        else return false;
    }
    public int GetSceneCount()
    {
        _sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
        return _sceneCountInBuildSettings;
    }
}
