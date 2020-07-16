using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leave : MonoBehaviour
{
    [SerializeField] private ToolsHistory _toolsHistory = null;
    [SerializeField] private GameObject confirmQuit = null;

    public void LoadMenu(){
        SceneManager.LoadScene("Menu");
    }

    public void CheckQuit() {
        if(_toolsHistory.IsHistoryEmpty())
            LoadMenu();
        else
            confirmQuit.SetActive(true);
    }
}
