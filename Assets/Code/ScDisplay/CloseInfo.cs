using System.Collections;
using UnityEngine;

public class CloseInfo : MonoBehaviour
{
    info inf;
    backFromGame backScript;

    void Start()
    {
        backScript = GameObject.Find("back").GetComponent<backFromGame>();
        backScript.backEnabled = false;
    }

    public void ClosePanel()
    {
        inf = GameObject.Find("info").GetComponent<info>();
        DestroyObject(transform.parent.parent.parent.gameObject);
        DestroyObject(GameObject.Find("EventSystem(Clone)"));
        inf.ToggleColliders(true);
        EnableBack();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    IEnumerator EnableBack()
    {
        yield return new WaitForSeconds(0.2f);
        backScript.backEnabled = true;
    }
}
