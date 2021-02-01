using UnityEngine;
using System.Collections;

public class optionController : MonoBehaviour
{
    displayConfirmBox dCB;
    public bool option;
    lvlManager lvlmanager;
    
    void Start()
    {
        lvlmanager = GameObject.Find("SceneManager(Clone)").GetComponent<lvlManager>();
    }

    public void OnMouseUp()
    {
        dCB = FindObjectOfType<displayConfirmBox>();
        dCB.DeleteBox();
        if (option)
        {
            deleteSavedData();
        }
    }
    
    void deleteSavedData()
    {
        lvlmanager.lastUnlockedlvl = 0;
        EndGameVariables.UpdateData("");
        GPlayclass.UpdateCloudLevel(0);
        GPlayclass.UpdateTimeAndDeaths("");
    }
}
