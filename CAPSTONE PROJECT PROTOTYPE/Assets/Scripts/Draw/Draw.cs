using UnityEngine;

public class Draw : MonoBehaviour
{
    public Camera _mCamera;
    public GameObject _brush;
    public GameObject _dot;
    LineRenderer _currentLineRenderer;
    Vector2 _lastPos;

    private void Update()
    {
        Drawing();
    }

    void Drawing()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Player first click
        {
            CreateDot();
            CreateBrush();
        }
        if (Input.GetKey(KeyCode.Mouse0)) //Player is holding the button
        {
            Vector2 _mousePos = _mCamera.ScreenToWorldPoint(Input.mousePosition);
            if (_mousePos != _lastPos)
            //Check if the mouse change position This is so we will not call multiple points on the same position.
            {
                AddAPoint(_mousePos);
                _lastPos = _mousePos;
            }

        }
        else // Player release the button
        {
            _currentLineRenderer = null; //Make it null when we are nut clicking
        }
    }

    void CreateBrush() //Create new instance of the brush (The line?)
    {
        GameObject _brushInstance = Instantiate(_brush);
        _brushInstance.transform.SetParent(this.transform);
        _currentLineRenderer = _brushInstance.GetComponent<LineRenderer>();

        Vector2 _mousePos = _mCamera.ScreenToWorldPoint(Input.mousePosition); //Study this

        //Setting default start point of the brush, where the player first clicks
        //Go back here later
        _currentLineRenderer.SetPosition(0, _mousePos);
        _currentLineRenderer.SetPosition(1, _mousePos);
    }

    void AddAPoint(Vector2 _pointPos)
    //Increase amount of points by one, also gets the index of the point the player just created
    //This function is called whenever the mouse is held down
    {
        _currentLineRenderer.positionCount++;
        //Setting it to the position of the mouse
        int _positionIndex = _currentLineRenderer.positionCount - 1;
        _currentLineRenderer.SetPosition(_positionIndex, _pointPos);
    }

    void CreateDot()
    {

        Vector2 _mousePos = _mCamera.ScreenToWorldPoint(Input.mousePosition);
        GameObject _drawDot = Instantiate(_dot, _mousePos, transform.rotation);
        _drawDot.transform.SetParent(this.transform);
    }

    public void UndoLine()
    {
        //To be continued
    }

    public void ClearAll()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
