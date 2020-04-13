using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    BlueprintController parent;
    GameObject unlockShape;
    string targetName;
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<BlueprintController>();
        unlockShape = transform.Find("UnlockShape").gameObject;
        targetName = unlockShape.GetComponent<SpriteRenderer>().sprite.name;
    }

    public void SetShape(Sprite sprite, Color color)
    {
        SpriteRenderer renderer = unlockShape.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = color;
        targetName = sprite.name;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject desiredShape = GameObject.Find(targetName + "(Clone)");
        if(desiredShape != null && desiredShape.transform.localScale.x > 30)
        {
            parent.Locked = false;
        }
    }
}
