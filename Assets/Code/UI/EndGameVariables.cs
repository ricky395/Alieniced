using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EndGameVariables : MonoBehaviour {

    static TextMesh title;
    static TextMesh currentTime;
    static TextMesh bestTime;
    static TextMesh currentDeaths;
    static TextMesh bestDeaths;
    static string data = "";
    static int currentLvl;
    static int currentPack;
    const int CharsPerLevel = 12;
    static lvlManager lvlmanager;

    public static string GetData()
    {
        return data;
    }

    public static void UpdateData(string d)
    {
        data = d;
    }

    /// <summary>
    /// Inicializo los textos en los que se muestran al usuario las puntuaciones
    /// </summary>
    static void InitializateVariables()
    {
        title = GameObject.Find("title").GetComponent<TextMesh>();
        currentTime = GameObject.Find("current time").GetComponent<TextMesh>();
        bestTime = GameObject.Find("best time").GetComponent<TextMesh>();
        currentDeaths = GameObject.Find("current deaths").GetComponent<TextMesh>();
        bestDeaths = GameObject.Find("best deaths").GetComponent<TextMesh>();
        lvlmanager = GameObject.Find("SceneManager(Clone)").GetComponent<lvlManager>();

        currentPack = lvlManager.GetCurrentPNo();
        /*if (GPlayclass.GetTimeAndDeaths().Length < 3)
            data = "";
        else
        {
            data = GPlayclass.GetTimeAndDeaths();
        }*/
    }

    /// <summary>
    /// Actualizo los datos mediante los números pasados
    /// </summary>
    /// <param name="timeInSec"></param>
    /// <param name="deaths"></param>
    public static void UpdateCurrentNumbers(int timeInSec, int deaths)
    {
        InitializateVariables();
        title.text = "Level " + SceneManager.GetActiveScene().name + " completed";
        currentTime.text = TimeCounter.ConvertSecsToString(timeInSec);
        currentDeaths.text = deaths.ToString();

        Int32.TryParse(SceneManager.GetActiveScene().name, out currentLvl);
        if (IsNewLevel())
        {
            bestTime.text = currentTime.text;
            bestDeaths.text = currentDeaths.text;
            SaveNewData(timeInSec, deaths);
        }

        else
        {
            CheckData(timeInSec, deaths);
        }
    }

    /// <summary>
    /// Comprueba si los datos pertenecen a un nuevo nivel o a uno antiguo
    /// </summary>
    /// <returns></returns>
    static bool IsNewLevel()
    {
        return ((lvlmanager.lastUnlockedlvl - 1) * CharsPerLevel >= data.Length && (GPlayclass.GetLevel() == (currentPack * lvlManager.lvlCount) + currentLvl));
    }


    /// <summary>
    /// Guarda datos de un nuevo nivel
    /// </summary>
    /// <param name="timeInSec"></param>
    /// <param name="deaths"></param>
    static void SaveNewData(int timeInSec, int deaths)
    {
        char? zero = null;
        if (currentLvl < 10)
            zero = '0';

        data += "/" + currentPack + zero + currentLvl + "-" + AddZeroes(timeInSec.ToString(), true) + AddZeroes(deaths.ToString(), false);
        UpdateCloudData();
    }


    /// <summary>
    /// Actualiza el string de datos en la variable de la nube
    /// </summary>
    static void UpdateCloudData()
    {
        GPlayclass.UpdateTimeAndDeaths(data);
    }

    /// <summary>
    /// Añade ceros delante de los números en caso de que no tengan una cierta cantidad de cifras
    /// </summary>
    /// <param name="stringNumbers"></param>
    /// <param name="isTime"></param>
    /// <returns></returns>
    static string AddZeroes(string stringNumbers, bool isTime)
    {
        int i, nLength;
        string zeroes = "";

        if (isTime)
            nLength = 4;
        else nLength = 3;

        for (i = 0; i < nLength - stringNumbers.Length; i++)
        {
            zeroes += "0";
        }

        return zeroes + stringNumbers;

    }

    /// <summary>
    /// Busca las variables guardadas del nivel actual
    /// </summary>
    /// <param name="timeInSec"></param>
    /// <param name="deaths"></param>
    static void CheckData(int timeInSec, int deaths)
    {
        int i;

        //nivel actual -1 + chars por nivel
        i = ((currentPack * lvlManager.lvlCount + currentLvl) - 1) * CharsPerLevel;
        LookForVariables(i, timeInSec, deaths);
    }

    /// <summary>
    /// Comprueba si hay un récord en las puntuaciones
    /// </summary>
    /// <param name="i"></param>
    /// <param name="timeInSec"></param>
    /// <param name="deaths"></param>
    static void LookForVariables(int i, int timeInSec, int deaths)
    {
        int j, savedTime, savedDeaths;

        for (j = i + 2; data[j] != '-'; j++) ;
        Int32.TryParse(data.Substring(j + 1, 4), out savedTime);
        Int32.TryParse(data.Substring(j + 5, 3), out savedDeaths);
        string dataRecord;

        if (timeInSec < savedTime)
        {
            dataRecord = AddZeroes(timeInSec.ToString(), true);
            data = data.Substring(0, j + 1) + dataRecord + data.Substring(j + 5);

            bestTime.text = TimeCounter.ConvertSecsToString(timeInSec);
            UpdateCloudData();
        }

        else
        {
            bestTime.text = TimeCounter.ConvertSecsToString(savedTime);
        }
        
        if (deaths < savedDeaths)
        {
            dataRecord = AddZeroes(deaths.ToString(), false);
            if (j + 8 < data.Length)
                data = data.Substring(0, j + 5) + dataRecord + data.Substring(j + 8);
            else data = data.Substring(0, j + 5) + dataRecord;

            bestDeaths.text = deaths.ToString();
            UpdateCloudData();
        }

        else
        {
            bestDeaths.text = savedDeaths.ToString();
        }
    }
}
