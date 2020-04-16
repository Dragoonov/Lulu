﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintController : MonoBehaviour
{
    public GameObject shape;
    public SpriteRenderer renderer;
    private Color tempColor;
    public GameObject lockObject;
    public GameObject blockObject;

    [SerializeField]
    private bool _blocked;
    public bool Blocked
    {
        get
        {
            return _blocked;
        }
        set
        {
            if (_blocked != value)
            {
                _blocked = value;
                if (_blocked)
                    Block();
                else
                    Unblock();
            }
        }
    }

    [SerializeField]
    private bool _locked;
    public bool Locked
    {
        get
        {
            return _locked;
        }
        set
        {
            if(_locked != value)
            {
                _locked = value;
                if (_locked)
                    Lock();
                else
                    Unlock();
            }
        }
    }

    private void Awake()
    {
        blockObject = transform.Find("Block").gameObject;
        lockObject = transform.Find("Lock").gameObject;
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        tempColor = renderer.color;
        blockObject.SetActive(false);
        lockObject.SetActive(false);
        if (_locked)
            Lock();
        if (_blocked)
            Block();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateShape(Vector2 position, int fingerId)
    {
        if(!Blocked)
        {
            GameObject clone = Instantiate(shape, position, Quaternion.identity);
            clone.GetComponent<SpriteRenderer>().color = renderer.color;
            clone.GetComponent<ShapeController>().fingerId = fingerId;
        }
    }


    public string GetShapeName()
    {
        return shape.name;
    }

    public void DisableAssigning()
    {
        tempColor = renderer.color;
        renderer.color = Color.gray;
        GetComponent<Collider2D>().enabled = false;
    }

    public void EnableAssigning()
    {
        if(!Blocked)
        {
            GetComponent<Collider2D>().enabled = true;
        }
        renderer.color = tempColor;
    }

    private void Lock()
    {
        lockObject.SetActive(true);
    }


    private void Unlock()
    {
        lockObject.SetActive(false);
    }

    private void Block()
    {
        GetComponent<Collider2D>().enabled = false;
        blockObject.SetActive(true);
    }

    private void Unblock()
    {
        GetComponent<Collider2D>().enabled = true;
        blockObject.SetActive(false);
    }

    public void Lock(Sprite sprite, Color color)
    {
        Locked = true;
        lockObject.GetComponent<LockController>().SetShape(sprite, color);
    }

    public void Block(Sprite sprite, Color color)
    {
        Blocked = true;
        blockObject.GetComponent<BlockController>().SetShape(sprite, color);
    }
}
