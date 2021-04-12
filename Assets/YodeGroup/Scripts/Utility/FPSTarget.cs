using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTarget : MonoBehaviour {

    public int targetFrameRate = 60;

    private void Start()
    {
        SetFps(targetFrameRate);
    }

    public void SetFps(int frameRate)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameRate;
    }
}