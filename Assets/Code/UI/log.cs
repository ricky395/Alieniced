using System.Collections;
using UnityEngine;

public class log : MonoBehaviour
{

    GPlayclass gPlay;
    BoxCollider2D thisBox;
    Transform[] logObjects;
    ShowAchievements achievements;
    bool conected;

    void Start()
    {
        logObjects = GetComponentsInChildren<Transform>();
        gPlay = GameObject.Find("SceneManager(Clone)").GetComponent<GPlayclass>();
        thisBox = GetComponent<BoxCollider2D>();
        achievements = GameObject.Find("achievements").GetComponent<ShowAchievements>();
    }

    void OnMouseUp()
    {
        CheckState();
        StartCoroutine(Lock());
        ConectOrDisconnectThatsIsTheQuestion();
    }

    void ConectOrDisconnectThatsIsTheQuestion()
    {
        if (conected)
        {
            StartCoroutine(gPlay.Disconnect());
            achievements.Disable();
        }

        else
        {
            StartCoroutine(gPlay.Connect());
            achievements.Enable();
        }
    }

    void Update()
    {
        CheckState();
    }

    void CheckState()
    {
        if (gPlay.GetConectionSucceed()) //conectado correctamente
            TriggerState(false);

        else //desconectado o conexión fallida
            TriggerState(true);
    }

    void TriggerState(bool state)
    {
        if (state)
        {
            logObjects[1].GetComponent<TextMesh>().text = "log in";
            logObjects[4].GetComponent<SpriteRenderer>().enabled = false;
            conected = false;
        }

        else
        {
            logObjects[1].GetComponent<TextMesh>().text = "log out";
            logObjects[4].GetComponent<SpriteRenderer>().enabled = true;
            conected = true;
        }
    }

    IEnumerator Lock()
    {
        thisBox.enabled = false;
        SwitchColor(Color.gray);

        yield return new WaitForSeconds(2);

        TriggerState(!gPlay.GetConectionSucceed());
        thisBox.enabled = true;
        SwitchColor(Color.white);
    }

    void SwitchColor(Color color)
    {
        logObjects[1].GetComponent<TextMesh>().color = color;
        logObjects[2].GetComponent<TextMesh>().color = color;
        for (int i = 3; i < logObjects.Length; i++)
        {
            logObjects[i].GetComponent<SpriteRenderer>().color = color;
        }
    }
}
