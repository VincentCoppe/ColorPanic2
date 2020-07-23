using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leave : MonoBehaviour
{
    [SerializeField] private ToolsHistory _toolsHistory = null;
    [SerializeField] private GameObject confirmQuit = null;

    public bool ModifUnsaved = false;

    public void LoadMenu(){
        LoadScenes LoadScenes = FindObjectOfType<LoadScenes>();
        if (LoadScenes.folder == "GameLevels")
            SceneManager.LoadScene("LevelSelection");
        
        else if (LoadScenes.TestingLevel)
        {
            LoadScenes.setLevelTested(false);
            LoadScenes.LoadEditor();
        }
         else
        {
            SceneManager.LoadScene("Menu");

        }
    }

    public void CheckQuit() {
        if(!ModifUnsaved)
            LoadMenu();
        else

            confirmQuit.SetActive(true);
    }
}
