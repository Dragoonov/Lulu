using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    BlueprintController parent;
    public GameObject unblockShape;
    string targetName;
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<BlueprintController>();
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
