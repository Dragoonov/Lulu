using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    BlueprintController parent;
    GameObject unblockShape;
    string targetName;
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<BlueprintController>();
        unblockShape = transform.Find("UnblockShape").gameObject;
        targetName = unblockShape.GetComponent<SpriteRenderer>().sprite.name;
    }

    public void SetShape(Sprite sprite, Color color)
    {
        SpriteRenderer renderer = unblockShape.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = color;
        targetName = sprite.name;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject desiredShape = GameObject.Find(targetName + "(Clone)");
        if (desiredShape != null && desiredShape.transform.localScale.x > 30)
        {
            parent.Blocked = false;
        }
    }
}
