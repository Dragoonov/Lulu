using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolderController : MonoBehaviour
{
    private SpriteRenderer renderer;
    List<Color> colors;
    HashSet<string> assignableShapes;
    public bool isDragging;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Debug.Log(renderer);
        colors = new List<Color>();
        assignableShapes = new HashSet<string>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    public void AddColor(Color color, string shape)
    {
        if(!colors.Contains(color))
            colors.Add(color);
        assignableShapes.Add(shape);
        UpdateColor();
    }

    public void RemoveColor(Color color, string shape)
    {
        colors.Remove(color);
        assignableShapes.Remove(shape);
        UpdateColor();
    }

    private void UpdateColor()
    {
        // code to calculate color
        Debug.Log(renderer);
        if (colors.Count > 0)
            renderer.color = colors[colors.Count - 1];
        else
            renderer.color = Color.white;
    }
}
