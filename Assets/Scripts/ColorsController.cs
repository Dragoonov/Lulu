using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsController : MonoBehaviour
{
    public BlueprintController topLeftBlueprint;
    public BlueprintController topRightBlueprint;
    public BlueprintController bottomLeftBlueprint;
    public BlueprintController bottomRightBlueprint;

    private Color tempTopLeftBlueprintColor;
    private Color temptopRightBlueprintColor;
    private Color tempBottomLeftBlueprintColor;
    private Color tempBottomRightBlueprintColor;

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
                holderCopy = Instantiate(hit.collider.gameObject,new Vector3(objPosition.x,objPosition.y,5f),Quaternion.identity);
                holderCopy.gameObject.transform.localScale = new Vector3(10, 10, 10);
                ColorHolderController copyController = holderCopy.GetComponent<ColorHolderController>();
                copyController.isDragging = true;
                copyController.assignableShapes =
                    new HashSet<string>(hit.collider.gameObject.GetComponent<ColorHolderController>().assignableShapes);
                Debug.Log("Copty controller shapesSet size: " + copyController.assignableShapes.Count);
                LockUnassignableBlueprints();
            }
            else
            {
                if (objPosition.x < 0 && objPosition.y > 0 && IsBlueprintActivated(topLeftBlueprint))
                {
                    topLeftBlueprint.CreateShape(objPosition);
                }
                else if (objPosition.x > 0 && objPosition.y > 0 && IsBlueprintActivated(topRightBlueprint))
                {
                    topRightBlueprint.CreateShape(objPosition);
                }
                else if (objPosition.x < 0 && objPosition.y < 0 && IsBlueprintActivated(bottomLeftBlueprint))
                {
                    bottomLeftBlueprint.CreateShape(objPosition);
                }
                else if (objPosition.x > 0 && objPosition.y < 0 && IsBlueprintActivated(bottomRightBlueprint))
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
                RaycastHit2D hit = Physics2D.Raycast(objPosition, Vector3.back, 10f);
                Debug.Log("Hit: " + hit.collider.gameObject + ", started on position: " + objPosition);
                if (hit.collider != null)
                {
                    UnlockUnassignableBlueprints();
                    GameObject obj = hit.collider.gameObject;
                    Debug.Log(holderCopy.GetComponent<ColorHolderController>().assignableShapes.Count);
                    if (obj.name == "TopLeftBlueprint")
                    {
                        topLeftBlueprint.ChangeColor(holderColor);
                    }
                    else if (obj.name == "TopRightBlueprint")
                    {
                        topRightBlueprint.ChangeColor(holderColor);
                    }
                    else if (obj.name == "BottomLeftBlueprint")
                    {
                        bottomLeftBlueprint.ChangeColor(holderColor);
                    }
                    else if (obj.name == "BottomRightBlueprint")
                    {
                        bottomRightBlueprint.ChangeColor(holderColor);
                    }
                    Destroy(holderCopy);
                    holderCopy = null;
                }
            }
        }
    }

    private void LockUnassignableBlueprints()
    {
        if(holderCopy != null)
        {
            tempTopLeftBlueprintColor = topLeftBlueprint.GetColor();
            temptopRightBlueprintColor = topRightBlueprint.GetColor();
            tempBottomLeftBlueprintColor = bottomLeftBlueprint.GetColor();
            tempBottomRightBlueprintColor = bottomRightBlueprint.GetColor();
            ColorHolderController copyController = holderCopy.GetComponent<ColorHolderController>();
            if(!copyController.FindAcceptedShape(topLeftBlueprint.GetShapeName() + "(Clone)"))
            {
                topLeftBlueprint.ChangeColor(Color.grey);
                topLeftBlueprint.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
            if (!copyController.FindAcceptedShape(topRightBlueprint.GetShapeName() + "(Clone)"))
            {
                topRightBlueprint.ChangeColor(Color.grey);
                topRightBlueprint.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (!copyController.FindAcceptedShape(bottomLeftBlueprint.GetShapeName() + "(Clone)"))
            {
                bottomLeftBlueprint.ChangeColor(Color.grey);
                bottomLeftBlueprint.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
            if (!copyController.FindAcceptedShape(bottomRightBlueprint.GetShapeName() + "(Clone)"))
            {
                bottomRightBlueprint.ChangeColor(Color.grey);
                bottomRightBlueprint.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
        }
    }

    private void UnlockUnassignableBlueprints()
    {
        topLeftBlueprint.ChangeColor(tempTopLeftBlueprintColor);
        topRightBlueprint.ChangeColor(temptopRightBlueprintColor);
        bottomLeftBlueprint.ChangeColor(tempBottomLeftBlueprintColor);
        bottomRightBlueprint.ChangeColor(tempBottomRightBlueprintColor);
        topLeftBlueprint.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        topRightBlueprint.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        bottomLeftBlueprint.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        bottomRightBlueprint.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    private bool IsBlueprintActivated(BlueprintController controller)
    {
        return controller != null &&
            controller.isActiveAndEnabled;
    }
}
