using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathScript : MonoBehaviour
{
    static AudioController ac;
    float timer = 0;
    static int deaths;

    void Start()
    {
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
    }

    public static void ResetDeaths()
    {
        deaths = 0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Death")
        {
            StartCoroutine(Reset(true));
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            StartCoroutine(Reset(true));
            GPlayclass.UnlockAchievement("CgkI-Meyi84DEAIQBA");
        }
    }

    public static int GetDeaths()
    {
        return deaths;
    }

    public static IEnumerator Reset(bool state)
    {
        if (state && deaths < 999)
            deaths++;

        ac.PlayDeath();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
