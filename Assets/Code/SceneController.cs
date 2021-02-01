using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /// <summary>
    /// número de la escena
    /// </summary>
    static int sceneNumber;
    Scene scene;
    public Transform endingBox;
    public Transform hand;
    static Transform endingBoxSt;
    static TextMesh[] texts;
    static BoxCollider2D[] boxColls;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        endingBoxSt = endingBox;
        SetScene();
    }

    /// <summary>
    /// Actualiza el número de la escena actual en la variable sceneNumber
    /// </summary>
    /// <param name="level"></param>
    public void OnLevelWasLoaded()
    {
        SetScene();
        
        if (GPlayclass.GetIsFirstTime() && SceneManager.GetActiveScene().buildIndex == 1)
        {
            Instantiate(hand);
        }
    }

    void SetScene()
    {
        scene = SceneManager.GetActiveScene();
        sceneNumber = scene.buildIndex;
    }

    /// <summary>
    /// Devuelve el número de escena actual
    /// </summary>
    /// <returns></returns>
    public static int GetSceneNo()
    {
        return sceneNumber;
    }

    public static void LoadEndingBox()
    {
        DisableColliders();
        Instantiate(endingBoxSt.gameObject);
    }

    static void DisableColliders()
    {
        texts = FindObjectsOfType<TextMesh>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = Color.gray;
        }

        boxColls = FindObjectsOfType<BoxCollider2D>();
        for (int i = 0; i < boxColls.Length; i++)
        {
            boxColls[i].enabled = false;
        }
    }
}
