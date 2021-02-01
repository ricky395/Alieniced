using UnityEngine;
using System.Collections;

public class KeyTrigger : MonoBehaviour {

    KeyController kc;
    Transform playertr;

	void Start ()
    {
        kc = GetComponentInParent<KeyController>();
        playertr = GameObject.Find("Player").GetComponent<Transform>();
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == playertr)
            StartCoroutine(kc.OpenDoor(this.gameObject));
    }

    public void OnTriggerStay2D(Collider2D collision)
    {

    }
}
