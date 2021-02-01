using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelsManager : MonoBehaviour
{
    static lvlManager lvlManag;
    static int lvlCount;

    public static void UpdateLevel()
    {
        GameObject go = GameObject.Find("SceneManager(Clone)");
        lvlManag = go.GetComponent<lvlManager>();

        Int32.TryParse(SceneManager.GetActiveScene().name, out lvlCount);
        lvlManag.UpdateLevel(lvlCount);

        GPlayclass.UpdateCloudLevel(lvlManag.lastUnlockedlvl); //almacena el numero del nivel en una variable que es guardada en la nube
    }
}
