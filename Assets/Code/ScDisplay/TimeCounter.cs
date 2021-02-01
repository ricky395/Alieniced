using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeCounter : MonoBehaviour
{

    public int timeInSec = 0;
    TextMesh timeText;
    static string timeStringFormat;
    SpriteRenderer watch;
    bool countDown;
    static bool timerActive = true;
    Color blue = new Color(0.796f, 0.929f, 1f);
    Color red = new Color(0.8588f, 0.227f, 0.227f);

    IEnumerator Start()
    {
        watch = GameObject.Find("Watch").GetComponent<SpriteRenderer>();
        timeText = GameObject.Find("Time").GetComponent<TextMesh>();

        if (timeInSec != 0)
            countDown = true;
        else
        {
            timeText.text = "00:00";
            countDown = false;
        }

        while (timerActive)
        {
            UpdateTimer();
            yield return new WaitForSeconds(1);
        }
    }

    public void OnLevelWasLoaded()
    {
        ToggleTimerActive(true);
    }

    public static void ToggleTimerActive(bool state)
    {
        timerActive = state;
    }

    public int GetTimeSeconds()
    {
        return timeInSec;
    }

    public static string ConvertSecsToString(int secs)
    {
        timeStringFormat = "";

        if (secs / 60 < 10)
            timeStringFormat += "0" + secs / 60;

        else timeStringFormat += "" + secs / 60;

        if (secs % 60 < 10)
            timeStringFormat += ":0" + secs % 60;
        else
            timeStringFormat += ":" + secs % 60;

        return timeStringFormat;
    }

    void UpdateTimer()
    {

        timeText.text = ConvertSecsToString(timeInSec);

        if (countDown)
        {
            timeInSec--;
        }

        else
        {
            timeInSec++;
        }

        if (countDown && timeInSec == 9)
        {
            watch.color = Color.red;
            timeText.color = Color.red;
            ChangeToColor(red);
        }

        if (countDown && timeInSec < 0)
        {
            timerActive = false;
            GPlayclass.UnlockAchievement("CgkI-Meyi84DEAIQCA");
            StartCoroutine(DeathScript.Reset(false));
        }

        if (timeInSec > 3599)
        {
            StartCoroutine(DeathScript.Reset(false));
        }

        if (timeInSec == 180)
            GPlayclass.UpdateEvent("CgkI-Meyi84DEAIQCQ", 1);

        else if (timeInSec == 360)
            GPlayclass.UpdateEvent("CgkI-Meyi84DEAIQCQ", 2);
    }

    float lerpValue = 0.3f;
    float timeToWait = 0.005f;

    void ChangeToColor(Color color)
    {
        print("CAMERA " + Camera.main.backgroundColor);
        StartCoroutine(BackgroundColor(color));
    }

    IEnumerator BackgroundColor(Color color)
    {
        yield return new WaitForSeconds(timeToWait);
        if (!Mathf.Approximately(Camera.main.backgroundColor.b, color.b))
        {
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, color, lerpValue);
            StartCoroutine(BackgroundColor(color));
        }
        else
        {
            //valores para cambiar a azul
            if (color.Equals(red))
            {
                lerpValue = 0.2f;
                timeToWait = 0.05f;
                ChangeToColor(blue);
            }
            else //valores para cambiar a rojo
            {
                lerpValue = 0.3f;
                timeToWait = 0.005f;
                ChangeToColor(red);
            }

            StopCoroutine(BackgroundColor(color));
        }
    }

    /*void ChangeColor()
    {
        Camera.main.backgroundColor = blue;
        print("CAMERA " + Camera.main.backgroundColor);
        StartCoroutine(BackgroundColor());
    }

    IEnumerator BackgroundColor()
    {
        yield return new WaitForSeconds(0.02f);
        if (!Mathf.Approximately(Camera.main.backgroundColor.b, 0.333f))
        {
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, red, 0.3f);
            //print(Camera.main.backgroundColor);
            StartCoroutine(BackgroundColor());
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            ChangeColor();
            StopCoroutine(BackgroundColor());
        }
    }*/
}
