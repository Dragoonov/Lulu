using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempColorHolderController : MonoBehaviour, IClickable
{
    // Start is called before the first frame update
    private const int POSITION_OFFSET_PIXEL = 100;
    public TempHolderState state;
    private ColorHolderController holderController;
    private SpriteRenderer renderer;
    private const float SELECTED_SCALE_X = 19;
    private const float DEFAULT_SCALE_X = 15;
    private const float DEFAULT_SCALE_Y = 10;
    private const float SELECTED_SCALE_Y = 14;

    private void Awake()
    {
        holderController = GameObject.FindGameObjectWithTag("Holder").GetComponent<ColorHolderController>();
        renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        state = new TempHolderState(this);
        SetPositionViewport();
    }

    private void Update()
    {
        if (state.Selected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, SELECTED_SCALE_X, 0.05f), Mathf.Lerp(transform.localScale.y, SELECTED_SCALE_Y, 0.05f), transform.localScale.z);
        }
        if (!state.Selected)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, DEFAULT_SCALE_X, 0.05f), Mathf.Lerp(transform.localScale.y, DEFAULT_SCALE_Y, 0.05f),transform.localScale.z);
        }
    }

    private void SetPositionPixel()
    {
        transform.position = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.safeArea.x + POSITION_OFFSET_PIXEL, Screen.height/2, 10));
       
    }
        
    private void SetPositionViewport()
    {
        Vector2 savebotttomRightOffset = Camera.main.ScreenToViewportPoint(new Vector2(Screen.safeArea.width, Screen.safeArea.y));

        transform.position = Camera.main.ViewportToWorldPoint(
              new Vector3(savebotttomRightOffset.x - 0.2f, 0.25f, 10));
    }

    public void OnClicked()
    {
        if(holderController.state.Selected)
        {
            SpriteRenderer holderRenderer = holderController.GetComponent<SpriteRenderer>();
            renderer.color = holderRenderer.color;
            holderRenderer.color = holderController.defaultColor;
            holderController.RemoveBlueprintUsages();
            holderController.ClearBlueprints();
            holderController.state.Selected = false;
        }
        else
        {
            state.Selected = !state.Selected;
            if(state.Selected)
            {
                HighlightBlueprints(true);
            }
            else
            {
                HighlightBlueprints(false);
            }
        }
    }

    public void HighlightBlueprints(bool highlight)
    {
        GameObject[] blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
        foreach (GameObject blueprint in blueprints)
        {
            BlueprintController blueprintController = blueprint.GetComponent<BlueprintController>();
            if (!blueprintController.state.Locked && !blueprintController.state.Blocked)
            {
                blueprintController.state.Highlighted = highlight;
            }
        }
    }

    public class TempHolderState
    {
        private TempColorHolderController controller;

        public TempHolderState(TempColorHolderController controller)
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
                if (value == true)
                {
                    GameObject[] blueprints = GameObject.FindGameObjectsWithTag("Blueprint");
                    foreach (GameObject blueprint in blueprints)
                    {
                        BlueprintController blueprintController = blueprint.GetComponent<BlueprintController>();
                        blueprintController.state.Highlighted = true;
                    }
                }
                if (value == false)
                {
                    //controller.gameObject.GetComponent<Collider2D>().enabled = true;
                    //controller.renderer.color = tempColor;
                }
                this._selected = value;
                Highlighted = value;
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
                this._highlighted = value;
                if (value)
                    Debug.Log(controller.gameObject.name + "highlighted");
                if (!value)
                    Debug.Log(controller.gameObject.name + "not highlighted");
            }
        }
    }
}
