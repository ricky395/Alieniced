using UnityEngine;
using UnityEngine.SceneManagement;

public class lvlManager : MonoBehaviour
{
    public const int packCount = 3;
    public const int lvlCount = 20;
    BoxCollider2D[] currentLvlCollider;
    TextMesh[] currentLvlText;
    int[][] packLvlStatus; //0 locked, 1 unlocked, 2 completed
    bool[] packEnabled;
    public int lastUnlockedlvl;
    static int currentPNo = 0;
    int lastPack;

    int[] playedMaps;
    int playedCount = 0;
    
    void Start()
    {
        playedMaps = new int[packCount];
        packEnabled = new bool[packCount];
        packEnabled[0] = true;
        
        currentLvlCollider = new BoxCollider2D[lvlCount];
        currentLvlText = new TextMesh[lvlCount];

        FillLvlStatus();
    }

    /// <summary>
    /// Inicializo el array de estado de niveles
    /// </summary>
    void FillLvlStatus()
    {
        packLvlStatus = new int[packCount][];
        for (int i = 0; i < packCount; i++)
        {
            packLvlStatus[i] = new int[lvlCount];
            for (int j = 0; j < lvlCount; j++)
            {
                if (j == 0)
                    packLvlStatus[i][j] = 1; //unlocked lvl
                else packLvlStatus[i][j] = 0; //locked lvl
            }
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "LvlPack1")
        {
            currentPNo = 0;
            PackLoader();
        }
        else if (SceneManager.GetActiveScene().name == "LvlPack2")
        {
            currentPNo = 1;
            PackLoader();
        }
        else if (SceneManager.GetActiveScene().name == "LvlPack3")
        {
            currentPNo = 2;
            PackLoader();
        }

        else if (SceneManager.GetActiveScene().name == "packSelector")
        {
            UpdateSelectorScene();
        }
    }

    /// <summary>
    /// Inicializa los objetos del menú del selección y el último nivel activo
    /// </summary>
    void PackLoader()
    {
        for (int i = 0; i < lvlCount; i++)
        {
            currentLvlCollider[i] = GameObject.Find((i + 1).ToString()).GetComponent<BoxCollider2D>();
            currentLvlText[i] = currentLvlCollider[i].gameObject.GetComponentInChildren<TextMesh>();
            
        }

        DrawLvlStatus();
    }
    
    /// <summary>
    /// Controla el color y el estado de los niveles en el menú de selección
    /// </summary>
    void DrawLvlStatus()
    {
        int i;
        
        for (i = 0; i < lvlCount && i < lastUnlockedlvl - (lvlCount * currentPNo); i++)
        {
            packLvlStatus[currentPNo][i] = 2;
            ActivateLevel(i, Color.green);
        }
        
        if(i < lvlCount && (lastUnlockedlvl % 20) != 0)
            ActivateLevel(i, Color.white);
    }

    void ActivateLevel(int i, Color color)
    {
        currentLvlCollider[i].enabled = true;
        currentLvlText[i].color = color;
    }

    /// <summary>
    /// Actualiza el color de los packs de niveles y su acceso
    /// </summary>
    void UpdateSelectorScene()
    {
        lastPack = lastUnlockedlvl / lvlCount;
        int lastLevel = 0;

        if (lastPack == packCount)
        {
            lastLevel = 1;
        }

        ActivatePacks(GameObject.Find((lastPack - lastLevel).ToString()).GetComponent<Transform>());
        GameObject.Find((lastPack - lastLevel).ToString()).GetComponent<Transform>().Find("Levels").GetComponent<TextMesh>().text = lastUnlockedlvl % lvlCount + "/20 Lvl";

        for (int i = lastPack - 1; i >= 0; i--)
        {
            GameObject.Find(i.ToString()).GetComponent<Transform>().Find("Levels").GetComponent<TextMesh>().text = "20/20 Lvl";
            Transform tr = GameObject.Find(i.ToString()).GetComponent<Transform>();
            tr.GetComponentInChildren<MeshRenderer>().enabled = true;
            ActivatePacks(tr);
        }
    }

    /// <summary>
    /// Controla los niveles activos mediante una llamada externa en cada nivel
    /// </summary>
    /// <param name="pack"></param>
    /// <param name="lvl"></param>
    public void UpdateLevel(int lvl)
    {
        if (lvl + (currentPNo * lvlCount) > lastUnlockedlvl)
            lastUnlockedlvl = (currentPNo * lvlCount) + lvl; //número global de nivel
    }

    public void UnlockNextPack(int sceneNo)
    {
        bool exists = false;
        
        for (int i = 0; i < playedCount; i++)
        {
            if (playedMaps[i] == sceneNo)
                exists = true;
        }
        if (playedCount + 1 != playedMaps.Length)
            playedMaps[playedCount++] = sceneNo;

        if (!exists && (currentPNo * lvlCount) + sceneNo > lastUnlockedlvl)
            lastUnlockedlvl++;

        if (lastUnlockedlvl >= 20)
            GPlayclass.UnlockAchievement("CgkI-Meyi84DEAIQAA");
        if (lastUnlockedlvl >= 40)
            GPlayclass.UnlockAchievement("CgkI-Meyi84DEAIQAQ");
        if (lastUnlockedlvl >= 60)
            GPlayclass.UnlockAchievement("CgkI-Meyi84DEAIQAg");

        GPlayclass.UpdateCloudLevel(lastUnlockedlvl); //almacena el numero de nivel en una variable de la nube
    }

    public static int GetCurrentPNo()
    {
        return currentPNo;
    }

    void ActivatePacks(Transform packTr)
    {
        packTr.GetComponent<BoxCollider2D>().enabled = true;
        packTr.Find("Levels").GetComponent<TextMesh>().color = Color.white;
        packTr.Find("LevelPack").GetComponent<TextMesh>().color = Color.white;
    }
}
