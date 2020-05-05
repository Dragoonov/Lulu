using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlueprintController : MonoBehaviour, IClickable
{
    public SpriteRenderer renderer;
    public Color tempColor;
    public GameObject lockObject;
    public GameObject blockObject;
    public TextMeshProUGUI spawnAmountObj;
    public int spawnAmount;
    public BlueprintState state;
    private ColorHolderController holder;
    private TempColorHolderController tempHolder;

    public delegate void SelectAction(GameObject blueprint);
    public event SelectAction OnSelected;
    public event SelectAction OnDeselected;

    private const int DEFAULT_SCALE = 6;
    private const int SELECTED_SCALE = 8;

    private void Awake()
    {
        blockObject = transform.Find("Block").gameObject;
        lockObject = transform.Find("Lock").gameObject;
        renderer = GetComponent<SpriteRenderer>();
        state = new BlueprintState(this);
        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<ColorHolderController>();
        tempHolder = GameObject.FindGameObjectWithTag("TempHolder").GetComponent<TempColorHolderController>();
        spawnAmountObj.text = "";
        tempColor = renderer.color;
        state.Blocked = false;
        state.Locked = false;
        state.Highlighted = false;
        state.Selected = false;
        state.Disabled = false;
    }

    void Start()
    {
    }

    private void Update()
    {
        if(state.Selected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, SELECTED_SCALE, 0.05f), Mathf.Lerp(transform.localScale.y, SELECTED_SCALE, 0.05f),transform.localScale.z);
        }
        if (!state.Selected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, DEFAULT_SCALE, 0.05f), Mathf.Lerp(transform.localScale.y, DEFAULT_SCALE, 0.05f), transform.localScale.z);
        }
    }


    public void UpdateSpawnAmount()
    {
        if(spawnAmount > 50)
        {
            return;
        }
        spawnAmountObj.text = spawnAmount.ToString();
    }

    public void Lock(Sprite sprite, Color color)
    {
        state.Locked = true;
        lockObject.GetComponent<LockController>().SetShape(sprite, color);
    }

    public void Block(Sprite sprite, Color color)
    {
        state.Blocked = true;
        blockObject.GetComponent<BlockController>().SetShape(sprite, color);
    } 

    public void OnClicked()
    {
        if(holder.state.Selected)
        {
            if(state.Highlighted)
            {
                SpriteRenderer holderRenderer = holder.GetComponent<SpriteRenderer>();
                renderer.color = holderRenderer.color;
                ColorHolderController holderController = holder.GetComponent<ColorHolderController>();
                holderRenderer.color = holderController.defaultColor;
                holder.state.Selected = false;
                holderController.RemoveBlueprintUsages();
                holderController.ClearBlueprints();
            }
        }
        else if (tempHolder != null && tempHolder.isActiveAndEnabled && tempHolder.state.Selected)
        {
            SpriteRenderer holderRenderer = tempHolder.GetComponent<SpriteRenderer>();
            renderer.color = holderRenderer.color;
            Destroy(tempHolder.gameObject);
            tempHolder = null;
        }
        else
        {
            state.Selected = !state.Selected;
            if(state.Selected)
            {
                OnSelected(gameObject);
            }
            else
            {
                OnDeselected(gameObject);
            }
        }
    }

    public class BlueprintState
    {
        private BlueprintController controller;

        private Color tempColor = Color.green;
        public BlueprintState(BlueprintController controller)
        {
            this.controller = controller;
        }

        private bool _locked;
        public bool Locked
        {
            get
            {
                return _locked;
            }
            set
            {
                if (Blocked)
                {
                    return;
                }
                if(value == true)
                {
                    controller.lockObject.SetActive(true);
                }
                if (value == false)
                {
                    controller.lockObject.SetActive(false);
                }
                this._locked = value;
            }
        }

        private bool _blocked;
        public bool Blocked
        {
            get
            {
                return _blocked;
            }
            set
            {
                if (value == true)
                {
                    controller.blockObject.SetActive(true);
                }
                if (value == false)
                {
                    controller.blockObject.SetActive(false);
                }
                this._blocked = value;
            }
        }

        private bool _selected;
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if(Blocked)
                {
                    return;
                }
                this._selected = value;
            }
        }

        private bool _disabled;
        public bool Disabled
        {
            get
            {
                return _disabled;
            }
            set
            {
                if (Blocked)
                {
                    return;
                }
                if (value == true)
                {
                    tempColor = controller.renderer.color;
                    controller.renderer.color = Color.gray;
                    controller.gameObject.GetComponent<Collider2D>().enabled = false;

                }
                if (value == false)
                {
                    controller.gameObject.GetComponent<Collider2D>().enabled = true;
                    controller.renderer.color = tempColor;
                }
                this._selected = value;
            }
        }

        private bool _highlighted;
        public bool Highlighted
        {
            get
            {
                return _highlighted;
            }
            set
            {
                if (Blocked)
                {
                    return;
                }
                if (value)
                {
                    Debug.Log(controller.gameObject.name + "highlighted");
                }
                if (!value)
                {
                    Debug.Log(controller.gameObject.name + "not highlighted");
                }
                this._highlighted = value;
            }
        }

    }
}
