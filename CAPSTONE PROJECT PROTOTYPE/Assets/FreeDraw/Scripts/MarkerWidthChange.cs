using UnityEngine;
using UnityEngine.UI;
using FreeDraw;

public class MarkerWidthChange : MonoBehaviour
{
    public Slider _slider;
    public DrawingSettings _ds;

    private void Update()
    {
        _ds.SetMarkerWidth(_slider.value);
    }
}
