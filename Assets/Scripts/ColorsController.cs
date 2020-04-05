using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsController : MonoBehaviour
{
    public Color topLeftColor;
    public Color topRightColor;
    public Color bottomLeftColor;
    public Color bottomRightColor;

    public GameObject topLeftShape;
    public GameObject topRightShape;
    public GameObject bottomLeftShape;
    public GameObject bottomRightShape;


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
            GameObject shapeObj = null;
            if (objPosition.x < 0 && objPosition.y > 0)
            {
                shapeObj = Instantiate(topLeftShape, objPosition, Quaternion.identity);
                shapeObj.GetComponent<SpriteRenderer>().color = topLeftColor;
            }
            else if (objPosition.x > 0 && objPosition.y > 0)
            {
                shapeObj = Instantiate(topRightShape, objPosition, Quaternion.identity);
                shapeObj.GetComponent<SpriteRenderer>().color = topRightColor;
            }
            else if (objPosition.x < 0 && objPosition.y < 0)
            {
                shapeObj = Instantiate(bottomLeftShape, objPosition, Quaternion.identity);
                shapeObj.GetComponent<SpriteRenderer>().color = bottomLeftColor;
            }
            else if (objPosition.x > 0 && objPosition.y < 0)
            {
                shapeObj = Instantiate(bottomRightShape, objPosition, Quaternion.identity);
                shapeObj.GetComponent<SpriteRenderer>().color = bottomRightColor;
            }
        }
    }
}
