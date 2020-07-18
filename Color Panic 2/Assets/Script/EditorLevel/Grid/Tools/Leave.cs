using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leave : MonoBehaviour
{
    [SerializeField] private ToolsHistory _toolsHistory = null;
    [SerializeField] private GameObject confirmQuit = null;

    public bool ModifUnsaved = false;

    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void CheckQuit() {
        if(!ModifUnsaved)
            LoadMenu();
        else
            confirmQuit.SetActive(true);
    }
}
