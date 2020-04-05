using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolderController : MonoBehaviour
{
    private SpriteRenderer renderer;
    List<Color> colors;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Debug.Log(renderer);
        colors = new List<Color>();
    }

    public void AddColor(Color color)
    {
        if(!colors.Contains(color))
            colors.Add(color);
        UpdateColor();
    }

    public void RemoveColor(Color color)
    {
        colors.Remove(color);
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
