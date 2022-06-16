using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Header("Components")]
    public MeshFilter filter;
    public Transform pointStart;
    public Transform rotate;
    public Text area;
    
    [Header("Length")]
    public Vector3 width;
    public Vector3 length;
    public float leftright;
    public float thickness;
    public int widthCount;
    public int lengthCount;

    private void OnEnable()
    {
        SetPlane();
        enabled = false;
    }

    public void SetPlane()
    {
        if (thickness < 0) thickness = 0;
        filter.mesh = CreaterPlane.Plane(pointStart.position, width,
            length, widthCount, lengthCount, thickness, leftright
            );
    }

    public void RechangeThickness(Slider slider) {
        thickness = slider.value;
        enabled = true;
    }
    public void RechangeCount(Slider slider)
    {
        leftright = slider.value;
        enabled = true;
    }

    public void RechangeRotate(Slider slider)
    {
        filter.transform.localEulerAngles = new Vector3(0, slider.value, 0);
        enabled = true;
    }

    public void RechangeWidth(Slider slider)
    {
        widthCount = System.Convert.ToInt32(slider.value);
        enabled = true;
    }

    public void RechangeHeight(Slider slider)
    {
        lengthCount = System.Convert.ToInt32(slider.value);
        enabled = true;
    }
}
