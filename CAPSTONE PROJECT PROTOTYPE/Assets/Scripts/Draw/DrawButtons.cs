using UnityEngine;

public class DrawButtons : MonoBehaviour
{
    Draw _drawScript;
    private void Start()
    {
        _drawScript = GameObject.FindGameObjectWithTag("Notepad").GetComponent<Draw>();
    }

    public void Undo()
    {
        Debug.Log("Undo");
        _drawScript.UndoLine();
    }

    public void Clear()
    {
        Debug.Log("Clear");
        _drawScript.ClearAll();
    }

}
