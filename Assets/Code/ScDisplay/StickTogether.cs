using UnityEngine;

[ExecuteInEditMode]
public class StickTogether : MonoBehaviour
{
    public Transform otherObject;

    public void OnDrawGizmosSelected()
    {
        otherObject.position = this.transform.position;
    }
}
