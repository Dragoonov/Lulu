using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    BlueprintController parent;
    GameObject unblockShape;
    string targetName;
    Color targetColor;

    private void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<BlueprintController>();
        unblockShape = transform.Find("UnblockShape").gameObject;
        targetName = unblockShape.GetComponent<SpriteRenderer>().sprite.name;
    }
    void Start()
    {
        
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
        GameObject desiredShape = GameObject.Find(targetName + "(Clone)");
        if (desiredShape != null && desiredShape.transform.localScale.x > 30 && desiredShape.GetComponent<SpriteRenderer>().color == targetColor)
        {
            parent.Blocked = false;
        }
    }
}
