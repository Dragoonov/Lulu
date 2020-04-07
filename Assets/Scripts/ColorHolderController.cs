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
            for(int i=1;i<colors.Count;i++)
            {
                color = new Color(Average(color.r, colors[i].r), Average(color.g, colors[i].g), Average(color.b, colors[i].b));
            }
            renderer.color = color;
        }
        else
            renderer.color = Color.white;
    }

    private float Average(float a, float b)
    {
        return (a + b) / 2;
    }
}
