using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using Trollpants.CloudOnce;

public class GPlayclass : MonoBehaviour
{
    lvlManager lvlmanager;
    public GameObject loader;
    Object loaderInst;
    BoxCollider2D[] boxColliders;
    int lastLoaded;

    public static bool GetIsFirstTime()
    {
        return CloudVariables.isFirstTime;
    }

    public static void DisableFirstTime()
    {
        CloudVariables.isFirstTime = false;
    }

    public static void UpdateCloudLevel(int level)
    {
        CloudVariables.Level = level;
    }

    public static int GetLevel()
    {
        return CloudVariables.Level;
    }

    public static void UpdateTimeAndDeaths(string data)
    {
        CloudVariables.TimeAndDeaths = data;
    }

    public static string GetTimeAndDeaths()
    {
        return CloudVariables.TimeAndDeaths;
    }

    public static void UpdateEvent(string eventId, uint steps)
    {
        PlayGamesPlatform.Instance.Events.IncrementEvent(eventId, steps);
    }

    static void UpdateEndGameData()
    {
        EndGameVariables.UpdateData(GetTimeAndDeaths());
    }

    void Start()
    {
        loaderInst = Instantiate(loader);

        Cloud.Initialize();

        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        .Build();
        PlayGamesPlatform.InitializeInstance(config);

        StartCoroutine(Connect());

        lvlmanager = GameObject.Find("SceneManager(Clone)").GetComponent<lvlManager>();
        UpdateEndGameData();
    }

    void InitBoxColliders()
    {
        boxColliders = FindObjectsOfType<BoxCollider2D>();
    }

    void SetBoxCollidersActive(bool state)
    {
        for (int i = 0; i < boxColliders.Length; i++)
        {
            boxColliders[i].enabled = state;
        }
    }

    public IEnumerator Connect()
    {
        Social.Active.Authenticate(Social.localUser, (bool success) =>
        {
            // handle success or failure
            if (success)
            {
                // if we signed in successfully, load data from cloud
                //Cloud.Storage.Load();
                lvlmanager.lastUnlockedlvl = CloudVariables.Level;
                lastLoaded = CloudVariables.Level;
            }
            else
            {
                // no need to show error message (error messages are shown automatically by plugin)
                Debug.LogWarning("Failed to sign in with Google Play Games.");
            }
            Destroy(loaderInst);
            InitBoxColliders();
            SetBoxCollidersActive(true);
        });

        yield return null;
    }

    public IEnumerator Disconnect()
    {
        PlayGamesPlatform.Instance.SignOut();

        yield return null;
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            InitBoxColliders();
            if (loaderInst == null)
                SetBoxCollidersActive(true);
        }
    }

    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }

    public static void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100.0f, (bool success) =>
        {
            // handle success or failure
        });
    }

    public bool GetConectionSucceed()
    {
        return Social.localUser.authenticated;
    }

    public void OnApplicationQuit()
    {
        SaveToCloud();
    }

    public void OnApplicationPause(bool pause)
    {
        SaveToCloud();
    }

    void SaveToCloud()
    {
        Cloud.Storage.Save();
    }
}