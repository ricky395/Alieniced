using UnityEngine;
using System.Collections;

public class ShowAchievements : MonoBehaviour
{
    GPlayclass gplay;
    Transform[] childrenTr;
    TextMesh textColor;
    BoxCollider2D thisBox;

    void Start()
    {
        gplay = GameObject.Find("SceneManager(Clone)").GetComponent<GPlayclass>();
        childrenTr = GetComponentsInChildren<Transform>();
        thisBox = GetComponent<BoxCollider2D>();
        textColor = childrenTr[1].GetComponent<TextMesh>();
    }

    void Update()
    {
        if (!gplay.GetConectionSucceed())
        {
            Disable();
        }

        else Enable();
    }

    public void OnMouseUp()
    {
        MenuAchievement.AddElement(gameObject.name);
        gplay.ShowAchievements();
    }

    public void Enable()
    {
        SwitchColor(Color.white, true);
    }

    public void Disable()
    {
        SwitchColor(Color.gray, false);
    }

    void SwitchColor(Color color, bool state)
    {
        thisBox.enabled = state;
        for (int i = 2; i < childrenTr.Length; i++)
        {
            childrenTr[i].GetComponent<SpriteRenderer>().color = color;
        }
        textColor.color = color;
    }
}
