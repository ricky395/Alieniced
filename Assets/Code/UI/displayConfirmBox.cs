using UnityEngine;
using System.Collections;

public class displayConfirmBox : MonoBehaviour
{
    public GameObject boxPrefab;
    GameObject prefabCopy;
    string boxDescription = "Do you really\nwant to delete\nall saved data?";
    BoxCollider2D[] boxCollArray;
    Transform[] boxTexts;
    
    public void OnMouseUp()
    {
        boxCollArray = FindObjectsOfType<BoxCollider2D>();//recoge todos los componentes BC2D de la escena
        CollidersStatus(false);
        prefabCopy = Instantiate(boxPrefab);
        boxTexts = prefabCopy.GetComponentsInChildren<Transform>();
        boxTexts[1].GetComponent<TextMesh>().text = boxDescription;
        for (int i = 1; i < 4; i++)
        {
            boxTexts[i].GetComponent<MeshRenderer>().sortingOrder = 6;
        }

        MenuAchievement.AddElement(gameObject.name);
    }

    void CollidersStatus(bool state)
    {
        foreach (BoxCollider2D bC in boxCollArray)
        {
            bC.enabled = state;
        }
    }

    public void DeleteBox()
    {
        GameObject.Destroy(prefabCopy);
        CollidersStatus(true);
    }
}
