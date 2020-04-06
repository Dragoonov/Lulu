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

    GameObject holderCopy;

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
            RaycastHit2D hit = Physics2D.Raycast(objPosition, Vector2.zero);
            GameObject shapeObj = null;
            if(hit.collider != null && hit.collider.gameObject.name == "ColorHolder")
            {
                holderCopy = Instantiate(hit.collider.gameObject);
                holderCopy.gameObject.transform.localScale = new Vector3(10, 10, 10);
                holderCopy.GetComponent<ColorHolderController>().isDragging = true;
            }
            else
            {
                if (objPosition.x < 0 && objPosition.y > 0)
                {
                    shapeObj = Instantiate(topLeftShape, objPosition, Quaternion.identity);
                }
                else if (objPosition.x > 0 && objPosition.y > 0)
                {
                    shapeObj = Instantiate(topRightShape, objPosition, Quaternion.identity);
                }
                else if (objPosition.x < 0 && objPosition.y < 0)
                {
                    shapeObj = Instantiate(bottomLeftShape, objPosition, Quaternion.identity);
                }
                else if (objPosition.x > 0 && objPosition.y < 0)
                {
                    shapeObj = Instantiate(bottomRightShape, objPosition, Quaternion.identity);
                }
                shapeObj.GetComponent<Rigidbody2D>().simulated = true;
                shapeObj.GetComponent<ShapeController>().enabled = true;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(holderCopy != null)
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(objPosition, Vector2.zero);
                if(hit.collider != null)
                {
                    Color holderColor = hit.collider.gameObject.GetComponent<SpriteRenderer>().color;
                    if (hit.collider.gameObject.name == "TopLeftBlueprint")
                    {
                        topLeftShape.GetComponent<SpriteRenderer>().color = holderColor;
                    }
                    else if (hit.collider.gameObject.name == "TopRightBlueprint")
                    {
                        topRightShape.GetComponent<SpriteRenderer>().color = holderColor;
                    }
                    else if (hit.collider.gameObject.name == "BottomLeftBlueprint")
                    {
                        bottomLeftShape.GetComponent<SpriteRenderer>().color = holderColor;
                    }
                    else if (hit.collider.gameObject.name == "BottomRightBlueprint")
                    {
                        bottomRightShape.GetComponent<SpriteRenderer>().color = holderColor;
                    }
                }
                Destroy(holderCopy);
                holderCopy = null;
            }
        }
    }
}
