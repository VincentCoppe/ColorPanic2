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
        else if(SceneManager.GetActiveScene().name == "LevelTest")
        {
           
            LoadScenes.setLevelTested(false);
        } else
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
