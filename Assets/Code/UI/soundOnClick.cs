using UnityEngine;
using System.Collections;

public class soundOnClick : MonoBehaviour
{
    AudioController ac;

    void Start()
    {
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
    }

    public void OnMouseUp()
    {
        ac.PlayClickBip();
    }
}
