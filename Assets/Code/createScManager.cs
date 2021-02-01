using UnityEngine;
using System.Collections;

public class createScManager : MonoBehaviour {

    public GameObject ScManager;
    GameObject sceneObj;

	void Start () {
        
        sceneObj = GameObject.Find("SceneManager(Clone)");

        if (sceneObj == null)
            sceneObj = Instantiate(ScManager);
        else this.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
