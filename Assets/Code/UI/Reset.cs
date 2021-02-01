using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public void OnMouseUp()
    {
        StartCoroutine(DeathScript.Reset(false));
        if (gameObject.name == "try again")
            DeathScript.ResetDeaths();
    }
}
