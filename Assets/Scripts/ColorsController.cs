using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsController : MonoBehaviour
{
    public BlueprintController topLeftBlueprint;
    public BlueprintController topRightBlueprint;
    public BlueprintController bottomLeftBlueprint;
    public BlueprintController bottomRightBlueprint;

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
                    topLeftBlueprint.CreateShape(objPosition);
                }
                else if (objPosition.x > 0 && objPosition.y > 0)
                {
                    topRightBlueprint.CreateShape(objPosition);
                }
                else if (objPosition.x < 0 && objPosition.y < 0)
                {
                    bottomLeftBlueprint.CreateShape(objPosition);
                }
                else if (objPosition.x > 0 && objPosition.y < 0)
                {
                    bottomRightBlueprint.CreateShape(objPosition);
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(holderCopy != null)
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Color holderColor = holderCopy.GetComponent<SpriteRenderer>().color;
                Destroy(holderCopy);
                holderCopy = null;
                RaycastHit2D hit = Physics2D.Raycast(objPosition, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject + "\nColor: " + holderColor);
                    if (hit.collider.gameObject.name == "TopLeftBlueprint")
                    {
                        topLeftBlueprint.ChangeColor(holderColor);
                    }
                    else if (hit.collider.gameObject.name == "TopRightBlueprint")
                    {
                        topRightBlueprint.ChangeColor(holderColor);
                    }
                    else if (hit.collider.gameObject.name == "BottomLeftBlueprint")
                    {
                        bottomLeftBlueprint.ChangeColor(holderColor);
                    }
                    else if (hit.collider.gameObject.name == "BottomRightBlueprint")
                    {
                        bottomRightBlueprint.ChangeColor(holderColor);
                    }
                }
            }
        }
    }

    private void GreyOutUnassignableBlueprints()
    {
        if(holderCopy != null)
        {

        }
    }
}
