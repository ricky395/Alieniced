using UnityEngine;
using System.Collections;

public class soundController : MonoBehaviour
{
    public Sprite sndOn;
    public Sprite sndOff;
    SpriteRenderer sound;
    AudioSource sceneManager;
    AudioController ac;

    void Start()
    {
        PreLoad();
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
    }

    public void OnMouseUp()
    {
        if (sound.sprite == sndOn)
        {
            sceneManager.enabled = false;
        }

        else
        {
            sceneManager.enabled = true;
            ac.PlayClickBip();
        }
        
        UpdateIcon();
        MenuAchievement.AddElement(gameObject.name);
    }

    public void OnLevelWasLoaded()
    {
        PreLoad();
        UpdateIcon();
    }

    void PreLoad()
    {
        sound = GetComponentsInChildren<SpriteRenderer>()[1];
        sceneManager = GameObject.Find("SceneManager(Clone)").GetComponent<AudioSource>();
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
    }

    void UpdateIcon()
    {
        if (!sceneManager.enabled)
            sound.sprite = sndOff;
        else
        {
            sound.sprite = sndOn;
        }
    }
}
