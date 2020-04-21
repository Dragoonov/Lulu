using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    private const float SCALE_FACTOR = 0.7f;
    private const int PIXEL_OFFSET = 100;

    void Start()
    {
        SetPosition();
        var aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        var isTablet = (DeviceCheck.DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);
        if (isTablet)
        {
            Vector2 scale = transform.GetChild(0).localScale;
            transform.GetChild(0).localScale = new Vector3(scale.x * SCALE_FACTOR, scale.y * SCALE_FACTOR);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPosition()
    {
        float y = Camera.main.ScreenToViewportPoint(
            new Vector2(0, Screen.safeArea.height)).y;

        float yBottom = Camera.main.ScreenToViewportPoint(
            new Vector2(0, Screen.safeArea.y)).y;

        transform.GetChild(1).position = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, y - 0.1f));
        transform.GetChild(2).position = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, yBottom + 0.1f));
    }
}
