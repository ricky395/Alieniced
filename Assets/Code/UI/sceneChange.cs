using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    public string scn;
    string sceneName;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == "packSelector")
                StartCoroutine(ChangeScene("main"));
            else if (sceneName == "main")
                Application.Quit();
            else if (sceneName != "main")
                StartCoroutine(ChangeScene(scn));
        }
    }

    public void OnMouseUp()
    {
        MenuAchievement.AddElement(gameObject.name);
        StartCoroutine(ChangeScene(scn));
    }

    IEnumerator ChangeScene(string scene)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(scene);
    }
}
