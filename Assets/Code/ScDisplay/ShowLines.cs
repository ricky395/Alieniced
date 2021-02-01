using UnityEngine;

[ExecuteInEditMode]
public class ShowLines : MonoBehaviour
{
    public bool showLine = true;
    public Color color;

    public void Show(Transform[] couple)
    {
        if (showLine && couple[1] != null && couple[2] != null)
            Debug.DrawLine(couple[1].transform.position, couple[2].transform.position, color);
    }
}
