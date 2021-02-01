using UnityEngine;
using System.Collections;

public class RandomAnimScript : MonoBehaviour {

    Animator anim;
    public float minValue;
    public float maxValue;

	void Start () {

        anim = GetComponent<Animator>();
        InvokeRepeating("SetUp", 0, 3f);
	}
	
	void SetUp()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        yield return new WaitForSeconds(Random.Range(minValue, maxValue));
        anim.SetTrigger("animate");
    }
}
