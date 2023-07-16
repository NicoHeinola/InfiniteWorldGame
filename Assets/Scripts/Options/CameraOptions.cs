using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraOptions : MonoBehaviour
{
    public Camera cam;
    public void OnViewSizeChange(float value)
    {
        this.cam.orthographicSize = value;
    }
}
