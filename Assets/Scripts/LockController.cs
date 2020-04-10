using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockController : MonoBehaviour
{
    BlueprintController parent;
    public GameObject unlockShape;
    void Start()
    {
        parent = transform.parent.gameObject.GetComponent<BlueprintController>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject desiredShape = GameObject.Find(unlockShape.name + "(Clone)");
        if(desiredShape != null && desiredShape.transform.localScale.x > 30)
        {
            parent.Locked = false;
        }
    }
}
