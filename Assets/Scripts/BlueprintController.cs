using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueprintController : MonoBehaviour
{
    public GameObject shape;
    public SpriteRenderer renderer;
    public Color tempColor;
    public GameObject lockObject;
    public GameObject blockObject;
    public TextMeshProUGUI spawnAmountObj;
    public int spawnAmount;
    private const int POSITION_OFFSET_PIXEL = 100;

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
        spawnAmountObj.text = "";
        SetPositionViewport();
        //SetPositionPixel();
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
        UpdateSpawnAmount();
    }

    private void UpdateSpawnAmount()
    {
        if(spawnAmount > 50)
        {
            return;
        }
        spawnAmountObj.text = spawnAmount.ToString();
    }

    private void SetPositionPixel()
    {
        if(shape.name == "triangle")
        {
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.safeArea.x + POSITION_OFFSET_PIXEL, Screen.safeArea.y + POSITION_OFFSET_PIXEL, 10));
        }
        if (shape.name == "circle")
        {
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.safeArea.x + POSITION_OFFSET_PIXEL,
                Screen.safeArea.height - POSITION_OFFSET_PIXEL, 10));
        }
        if (shape.name == "square")
        {
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.safeArea.width - POSITION_OFFSET_PIXEL,
                Screen.safeArea.height - POSITION_OFFSET_PIXEL, 10));
        }
        if (shape.name == "pentagon")
        {
            transform.position = Camera.main.ScreenToWorldPoint(
                new Vector3(Screen.safeArea.width - POSITION_OFFSET_PIXEL,
                POSITION_OFFSET_PIXEL + Screen.safeArea.y, 10));
        }
    }

    private void SetPositionViewport()
    {
        Vector2 savebotttomLeftOffset = Camera.main.ScreenToViewportPoint(new Vector2(Screen.safeArea.x, Screen.safeArea.y));
        Vector2 saveBottomRightOffset = Camera.main.ScreenToViewportPoint(
            new Vector2(Camera.main.pixelWidth-Screen.safeArea.width, Screen.safeArea.y));
        Vector2 savetopLeftOffset = Camera.main.ScreenToViewportPoint(
            new Vector2(Screen.safeArea.x, Camera.main.pixelHeight - Screen.safeArea.height));
        Vector2 savetopRightOffset = Camera.main.ScreenToViewportPoint(
            new Vector2(Camera.main.pixelWidth-Screen.safeArea.width, Camera.main.pixelHeight - Screen.safeArea.height));

        if (shape.name == "triangle")
        {
            transform.position = Camera.main.ViewportToWorldPoint(
                new Vector3(0.1f+savebotttomLeftOffset.x, 0.05f + savebotttomLeftOffset.y, 10));
        }
        if (shape.name == "circle")
        {
            transform.position = Camera.main.ViewportToWorldPoint(
                new Vector3(0.1f + savetopLeftOffset.x, 0.95f - savetopLeftOffset.y, 10));
        }
        if (shape.name == "square")
        {
            transform.position = Camera.main.ViewportToWorldPoint(
                new Vector3(0.9f - savetopRightOffset.x, 0.95f - savetopRightOffset.y, 10));
        }
        if (shape.name == "pentagon")
        {
            transform.position = Camera.main.ViewportToWorldPoint(
                new Vector3(0.9f - saveBottomRightOffset.x, 0.05f + saveBottomRightOffset.y, 10));
        }
    }

    public void CreateShape(Vector2 position, int fingerId)
    {
        if(!Blocked && spawnAmount > 0)
        {
            GameObject clone = Instantiate(shape, position, Quaternion.identity);
            clone.GetComponent<SpriteRenderer>().color = renderer.color;
            clone.GetComponent<ShapeController>().fingerId = fingerId;
            spawnAmount--;
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
