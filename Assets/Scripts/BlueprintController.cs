using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintController : MonoBehaviour
{
    public GameObject shape;
    private SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateShape(Vector2 position)
    {
        GameObject clone = Instantiate(shape, position, Quaternion.identity);
        clone.GetComponent<SpriteRenderer>().color = renderer.color;
    }

    public void ChangeColor(Color color)
    {
        renderer.color = color;
    }

    public string GetShapeName()
    {
        return shape.name;
    }

    public Color GetColor()
    {
        return renderer.color;
    }


}
