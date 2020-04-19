using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceCanvasChooser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        var isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);
        if(isTablet)
        {
            transform.Find("CanvasPhone").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("CanvasTablet").gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

        Debug.Log("Getting device inches: " + diagonalInches);

        return diagonalInches;
    }
}
