using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolderController : MonoBehaviour
{
    private SpriteRenderer renderer;
    List<Color> colors;
    public HashSet<string> assignableShapes;
    public bool isDragging;
    private Color defaultColor;
    private const int POSITION_OFFSET_PIXEL = 200;

    private void Awake()
    {
        if (assignableShapes == null)
            assignableShapes = new HashSet<string>();
    }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Debug.Log(renderer);
        colors = new List<Color>();
        defaultColor = renderer.color;
        SetPosition();
    }

    private void SetPosition()
    {
        float y = Camera.main.ScreenToViewportPoint(
            new Vector2(0, Screen.safeArea.height)).y;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f,y-0.15f,
            15));
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
            //Debug.Log(transform.position);
        }
    }

    public void AddColorWithShape(Color color, string shape)
    {
        colors.Add(color);
        assignableShapes.Add(shape);
        UpdateColor();
    }

    public void RemoveColorWithShape(Color color, string shape)
    {
        colors.Remove(color);
        assignableShapes.Remove(shape);
        UpdateColor();
    }

    public bool FindAcceptedShape(string shape)
    {
        return assignableShapes.Contains(shape);
    }

    private void UpdateColor()
    {
        if (colors.Count > 0)
        {
            Color color = colors[0];
            for (int i = 1; i < colors.Count; i++)
            {
                color = ProduceNewColor(color, colors[i]);
            }
            renderer.color = color;
        }
        else
            renderer.color = defaultColor;
    }

    private Color ProduceNewColor(Color color1, Color color2)
    {

        //r = 255 - Mathf.Sqrt((Mathf.Pow(255 - color1.r, 2) + Mathf.Pow(255 - color2.r, 2)) / 2),
        //b = 255 - Mathf.Sqrt((Mathf.Pow(255 - color1.r, 2) + Mathf.Pow(255 - color2.r, 2)) / 2)
        //g = 255 - Mathf.Sqrt((Mathf.Pow(255 - color1.r, 2) + Mathf.Pow(255 - color2.r, 2)) / 2),


        Color newColor = (color1 + color2) / 2;
        Debug.Log(newColor);
        return newColor;
    }

}
