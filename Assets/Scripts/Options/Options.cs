using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public int targetFrameRate = 30;
    [Range(1, 4)] public int VsyncCount = 4;
    public bool Vsync = true;
    private void Start()
    {
        if (Vsync)
        {
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = targetFrameRate;
        }
    }
}
