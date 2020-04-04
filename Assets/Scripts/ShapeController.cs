using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    bool rising;
    private float scaleUpFactor = 100.3f;
    private float scaleDownFactor = 25f;

    void Start()
    {
        rising = true;
        Debug.Log(transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            rising = false;

        if (rising && Input.GetMouseButton(0))
            Expand();
        else
            Shrink();
    }

    private void Expand()
    {
        Vector2 scale = transform.localScale;
        scale.x += scaleUpFactor * Time.deltaTime;
        scale.y += scaleUpFactor * Time.deltaTime;
        transform.localScale = scale;
        Debug.Log(transform.localScale);
    }

    private void Shrink()
    {
        Vector2 scale = transform.localScale;
        scale.x -= scaleDownFactor * Time.deltaTime;
        scale.y -= scaleDownFactor * Time.deltaTime;
        transform.localScale = scale;
        Debug.Log(transform.localScale);
        if (transform.localScale.x < 0.01f)
            Destroy(gameObject);
    }
}
