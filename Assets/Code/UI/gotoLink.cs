using UnityEngine;
using System.Collections;

public class gotoLink : MonoBehaviour
{
    public string url;

    public void OnMouseUp()
    {
        MenuAchievement.AddElement(gameObject.name);
        Application.OpenURL(url);
    }
}
