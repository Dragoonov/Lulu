using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    /*bool rising;
    private float scaleUpFactor = 100.3f;
    private float scaleDownFactor = 25f;
    Color currentColor;
    private ColorHolderController colorHolder;
    private bool colliding;
    public int fingerId;

    void Start()
    {
        rising = true;
        Debug.Log(transform.localScale);
        currentColor = GetComponent<SpriteRenderer>().color;
        colorHolder = GameObject.Find("ColorHolder").GetComponent<ColorHolderController>();
        colliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTouch();
        //HandleMouse();
    }

    private void HandleTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == fingerId && touch.phase == TouchPhase.Ended && rising)
            {
                rising = false;
            }
            if (rising && touch.fingerId == fingerId && touch.phase != TouchPhase.Ended)
            {
                Expand();
            }
        }
        if(!rising)
        {
            Shrink();
        }
    }

    private void HandleMouse()
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
    }

    private void Shrink()
    {
        Vector2 scale = transform.localScale;
        scale.x -= scaleDownFactor * Time.deltaTime;
        scale.y -= scaleDownFactor * Time.deltaTime;
        transform.localScale = scale;
        if (transform.localScale.x < 0.01f)
        {
            if(colliding)
            {
                colorHolder.RemoveColorWithShape(currentColor, gameObject.name);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name != gameObject.name)
        {
            colorHolder.AddColorWithShape(currentColor,gameObject.name);
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name != gameObject.name)
        {
            colorHolder.RemoveColorWithShape(currentColor, gameObject.name);
            colliding = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != gameObject.name)
        {
            colorHolder.AddColorWithShape(currentColor, gameObject.name);
            colliding = true;
        }
    }*/
}
