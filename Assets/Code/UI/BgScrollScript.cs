using UnityEngine;
using System.Collections;

public class BgScrollScript : MonoBehaviour {

    float tileSize = 1.601f;
	public float scrollSpdX;//velocidad en la que se mueve el eje x
	public float scrollSpdY;//velocidad en la que se mueve el eje y
    Vector2 tilePos;

	void Start () {
        tilePos = this.transform.position;
        InvokeRepeating("Move", 0, 0.01f);
	}

    void Move()
    {
        transform.Translate(this.transform.position.x * Time.deltaTime * scrollSpdX, this.transform.position.y * Time.deltaTime * scrollSpdY, 0);
    }

    void Update()
    {
        if (transform.position.x > tilePos.x + tileSize)
            transform.position = tilePos;
    }
}
