using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate;
    public TextMeshProUGUI display_Text;
    private bool doUpdate = true;
    public float updatesPerSecond = 1;
    public void Update()
    {
        if (this.doUpdate)
        {
            this.StartCoroutine(updateFPS());
        }

    }

    private IEnumerator updateFPS()
    {
        this.doUpdate = false;

        float current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        display_Text.text = avgFrameRate.ToString() + " FPS";

        yield return new WaitForSeconds(1f / this.updatesPerSecond);
        this.doUpdate = true;
    }
}
