using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsController : MonoBehaviour
{
    public Color topLeftColor;
    public Color topRightColor;
    public Color bottomLeftColor;
    public Color bottomRightColor;
    public GameObject shape;

    // Update is called once per frame
    void Update()
    {
        foreach(Touch touch in Input.touches)
        {

        }
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            GameObject shapeObj = Instantiate(shape, objPosition, Quaternion.identity);
            if (objPosition.x < 0 && objPosition.y > 0)
                shapeObj.GetComponent<SpriteRenderer>().color = topLeftColor;
            else if (objPosition.x > 0 && objPosition.y > 0)
                shapeObj.GetComponent<SpriteRenderer>().color = topRightColor;
            else if (objPosition.x < 0 && objPosition.y < 0)
                shapeObj.GetComponent<SpriteRenderer>().color = bottomLeftColor;
            else if (objPosition.x > 0 && objPosition.y < 0)
                shapeObj.GetComponent<SpriteRenderer>().color = bottomRightColor;
        }
    }
}
