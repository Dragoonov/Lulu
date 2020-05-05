using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolderController : MonoBehaviour, IClickable
{
    private SpriteRenderer renderer;
    [SerializeField]
    private List<GameObject> blueprints;
    public Color defaultColor;
    public HolderState state;
    private const int SELECTED_SCALE = 14;    
    private const int DEFAULT_SCALE = 10;    
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Debug.Log(renderer);
        blueprints = new List<GameObject>();
        defaultColor = renderer.color;
        SubscribeToBlueprints();
        state = new HolderState(this);
    }

    private void Update()
    {
        if (state.Selected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, SELECTED_SCALE, 0.05f), Mathf.Lerp(transform.localScale.y, SELECTED_SCALE, 0.05f), transform.localScale.z);
        }
        if (!state.Selected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, DEFAULT_SCALE, 0.05f), Mathf.Lerp(transform.localScale.y, DEFAULT_SCALE, 0.05f),transform.localScale.z);
        }
    }

    private void OnDestroy()
    {
        UnsubscribeFromBlueprints();
    }

    private void UnsubscribeFromBlueprints()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Blueprint");

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.GetComponent<BlueprintController>().OnSelected -= AddBlueprint;
            gameObject.GetComponent<BlueprintController>().OnDeselected -= RemoveBlueprint;
        }
    }

    private void SubscribeToBlueprints()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Blueprint");
        BackgroundController background = GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundController>();

        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.GetComponent<BlueprintController>().OnSelected += AddBlueprint;
            gameObject.GetComponent<BlueprintController>().OnDeselected += RemoveBlueprint;
        }
        background.OnSelected += ClearHolder;
    }

    private void ClearHolder()
    {
        state.Selected = false;
        state.Highlighted = false;
        renderer.color = defaultColor;
        ClearBlueprints();
    }

    private void AddBlueprint(GameObject blueprint)
    {
        blueprints.Add(blueprint);
        UpdateColor();
    }

    private void RemoveBlueprint(GameObject blueprint)
    {
        blueprints.Remove(blueprint);
        UpdateColor();
    }
    
          
    private void UpdateColor()
    {
        if (blueprints.Count <= 1)
        {
            renderer.color = defaultColor;
            return;
        }
        Color initialColor = blueprints[0].GetComponent<SpriteRenderer>().color;
        foreach (GameObject blueprint in blueprints)
        {
            initialColor = ProduceNewColor(initialColor, blueprint.GetComponent<SpriteRenderer>().color);
        }
        renderer.color = initialColor;
    }

    internal void ClearBlueprints()
    {
        foreach (GameObject blueprint in blueprints)
        {
            blueprint.GetComponent<BlueprintController>().state.Selected = false;
            blueprint.GetComponent<BlueprintController>().state.Highlighted = false;
        }
        blueprints.Clear();
    }

    public void OnClicked()
    {
        state.Selected = !state.Selected;
        if (blueprints.Count >=2 && state.Selected)
        {
            HighlightBlueprints(true);
        }
        else if(!state.Selected)
        {
            HighlightBlueprints(false);
        }
    }

    private void HighlightBlueprints(bool highlight)
    {
        foreach (GameObject blueprint in blueprints)
        {
            BlueprintController blueprintController = blueprint.GetComponent<BlueprintController>();
            if (!blueprintController.state.Locked && !blueprintController.state.Blocked)
            {
                blueprintController.state.Highlighted = highlight;
            }
        }
    }

    internal void RemoveBlueprintUsages()
    {
        foreach (GameObject blueprint in blueprints)
        {
            BlueprintController blueprintController = blueprint.GetComponent<BlueprintController>();
            blueprintController.spawnAmount--;
        }
    }

    private Color ProduceNewColor(Color color1, Color color2)
    {

        //r = 255 - Mathf.Sqrt((Mathf.Pow(255 - color1.r, 2) + Mathf.Pow(255 - color2.r, 2)) / 2),
        //b = 255 - Mathf.Sqrt((Mathf.Pow(255 - color1.r, 2) + Mathf.Pow(255 - color2.r, 2)) / 2)
        //g = 255 - Mathf.Sqrt((Mathf.Pow(255 - color1.r, 2) + Mathf.Pow(255 - color2.r, 2)) / 2),


        Color newColor = (color1 + color2) / 2;
        Debug.Log(newColor);
        return newColor;
    }
    public class HolderState
    {
        private ColorHolderController controller;

        public HolderState(ColorHolderController controller)
        {
            this.controller = controller;
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
                if(controller.blueprints.Count >=2)
                {
                    foreach (GameObject blueprint in controller.blueprints)
                    {
                        BlueprintController blueprintController = blueprint.GetComponent<BlueprintController>();
                        if(!blueprintController.state.Blocked && !blueprintController.state.Locked)
                        {
                            blueprintController.state.Highlighted = value;
                        }
                    }
                }
                GameObject tempHolder = GameObject.FindGameObjectWithTag("TempHolder");
                if(tempHolder != null && tempHolder.activeInHierarchy)
                {
                    tempHolder.GetComponent<TempColorHolderController>().state.Highlighted = value;
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
                //make glow
                this._highlighted = value;
                if(value)
                    Debug.Log(controller.gameObject.name + "highlighted");
                if (!value)
                    Debug.Log(controller.gameObject.name + "not highlighted");
            }
        }
    }

}
