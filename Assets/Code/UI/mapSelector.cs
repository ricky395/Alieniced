using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class mapSelector : MonoBehaviour
{
    public TextMesh txtObj;
    //lvlManager lvlm;
    string pack;
    int n;
    int lvlCount;

    void Start()
    {
        lvlCount = 20;
        pack = txtObj.text;
        //lvlm = GameObject.Find("SceneController(Clone)").GetComponent<lvlManager>();        
    }

    public void OnLevelWasLoaded()
    {
        n = Int32.Parse(gameObject.name);
    }

    public void OnMouseUp()
    {
        if (pack == "Pack 1")
        {
            SceneManager.LoadScene(n);
        }

        else if (pack == "Pack 2")
        {
            SceneManager.LoadScene(n + lvlCount * 1);
        }

        else if (pack == "Pack 3")
        {
            SceneManager.LoadScene(n + lvlCount * 2);
        }

        DeathScript.ResetDeaths();
    }
}
