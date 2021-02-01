using UnityEngine;
using UnityEngine.SceneManagement;

public class backFromGame : MonoBehaviour
{
    int currentPack;
    GameObject infoPanel;
    info inf;
    public bool backEnabled = true;

    void Start()
    {
        currentPack = lvlManager.GetCurrentPNo();
    }

    public void Update()
    {
        if (backEnabled && Input.GetKeyUp(KeyCode.Escape))
            goBack();
    }

    public void OnLevelWasLoaded()
    {
        currentPack = lvlManager.GetCurrentPNo();
    }

    public void OnMouseUp()
    {
        goBack();
    }

    void goBack()
    {
        if (gameObject.name == "backtomenu")
            DeathScript.ResetDeaths();

        infoPanel = GameObject.Find("infoui(Clone)");
        
        SceneManager.LoadScene("LvlPack" + (currentPack + 1).ToString());
    }
}
