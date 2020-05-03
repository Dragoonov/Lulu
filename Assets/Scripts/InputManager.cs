using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static void HandleTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(touch.position);
                HandleInputDown(objPosition, touch.fingerId);
            }
        }
    }

    public static void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            HandleInputDown(objPosition);
        }
    }

    private static void HandleInputDown(Vector2 objPosition, int fingerId = 0)
    {
        //RaycastHit hit;
        //Physics.Raycast(new Vector3(objPosition.x, objPosition.y, 10f), Vector3.back,out hit, Mathf.Infinity, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
        RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector3(objPosition.x,objPosition.y,5f), Vector3.back, 10f);
        GameObject hit;
        List<GameObject> listHits = new List<GameObject>();
        foreach(RaycastHit2D hite in hits)
        {
            listHits.Add(hite.collider.gameObject);
        }
        if ((hit = listHits.Find((obj) => obj.name == "ColorHolder")) != null) { }
        else if ((hit = listHits.Find((obj) => obj.name.Contains("Blueprint"))) != null) { }
        else if ((hit = listHits.Find((obj) => obj.name == "TempColorHolder")) != null) { }
        else if ((hit = listHits.Find((obj) => obj.name == "Background")) != null) { }

        Debug.Log("Clicked: " + hit.name);
        IClickable component = hit.GetComponent<IClickable>();
        component.OnClicked();
    }
}

