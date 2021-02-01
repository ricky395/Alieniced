using UnityEngine;
using System.Collections;

public class KeyController : MonoBehaviour
{
    Animator anim;
    BoxCollider2D collid;
    Transform[] keyAndDoor;
    AudioController ac;
    ShowLines lines;

    void Start()
    {
        InitKeyAndDoor();
        anim = keyAndDoor[2].GetComponent<Animator>();
        collid = keyAndDoor[2].GetComponent<BoxCollider2D>();
        ac = GameObject.Find("SceneManager(Clone)").GetComponent<AudioController>();
    }

    public IEnumerator OpenDoor(GameObject key)
    {
        yield return new WaitForSeconds(0.2f);

        ac.PlayKey();
        anim.SetTrigger("KeyUsed");
        Destroy(key);
        Destroy(collid);
    }

    void InitKeyAndDoor()
    {
        keyAndDoor = GetComponentsInChildren<Transform>();
    }

    public void OnDrawGizmos()
    {
        if (keyAndDoor == null)
            InitKeyAndDoor();
        if (lines == null)
            lines = GetComponent<ShowLines>();

        lines.Show(keyAndDoor);
    }
}
