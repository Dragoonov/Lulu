using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    BlueprintController parent;
    GameObject unblockShape;
    string targetName;
    Color targetColor;
    GameObject[] desiredShapes;

    private void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<BlueprintController>();
        unblockShape = transform.Find("UnblockShape").gameObject;
        targetName = unblockShape.GetComponent<SpriteRenderer>().sprite.name;
        desiredShapes = GameObject.FindGameObjectsWithTag("Blueprint");
    }

    public void SetShape(Sprite sprite, Color color)
    {
        SpriteRenderer renderer = unblockShape.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = color;
        targetName = sprite.name;
        targetColor = color;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject desiredShape in desiredShapes)
        {
            if (desiredShape != null &&
                desiredShape.name == targetName &&
                desiredShape.GetComponent<SpriteRenderer>().color == targetColor)
            {
                parent.state.Blocked = false;
                break;
            }
        }
    }
}
