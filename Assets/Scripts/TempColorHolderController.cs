using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempColorHolderController : MonoBehaviour
{
    // Start is called before the first frame update
    private const int POSITION_OFFSET_PIXEL = 100;
    public bool isDragging;
    void Start()
    {
        SetPositionViewport();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
            //Debug.Log(transform.position);
        }
    }

    private void SetPositionPixel()
    {
        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.safeArea.x + POSITION_OFFSET_PIXEL, Screen.height/2, 10));
       
    }

    public void Drop()
    {
        SetPositionViewport();
        isDragging = false;
    }

    private void SetPositionViewport()
    {
        Vector2 savebotttomLeftOffset = Camera.main.ScreenToViewportPoint(new Vector2(Screen.safeArea.x, Screen.safeArea.y));

        transform.position = Camera.main.ViewportToWorldPoint(
              new Vector3(0.1f + savebotttomLeftOffset.x, 0.5f, 10));
    }
}
