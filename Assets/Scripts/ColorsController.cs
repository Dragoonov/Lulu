using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsController : MonoBehaviour
{

    private const int GOAL_SCALE = 50;

    GameStateController stateController;

    public BlueprintController topLeftBlueprint;
    public BlueprintController topRightBlueprint;
    public BlueprintController bottomLeftBlueprint;
    public BlueprintController bottomRightBlueprint;
    public List<GameObject> blueprints;
    public GameObject helpModal;
    public GameObject finishModal;
    public Image goal;
    public int level;
    public TempColorHolderController tempColorHolder;

    GameObject holderCopy;
    public bool paused;

    private void Awake()
    {
        stateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
    }

    private void Start()
    {
        blueprints = new List<GameObject>() { 
            topLeftBlueprint.gameObject, 
            topRightBlueprint.gameObject,
            bottomLeftBlueprint.gameObject,
            bottomRightBlueprint.gameObject };
        LevelDataHolder.InjectData(this);
        if(level > stateController.LastLevel)
        {
            stateController.LastLevel = level;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            CheckIfGoalMatched();
            InputManager.HandleTouch();
            //HandleMouse();
        }
    }


    private void CheckIfGoalMatched()
    {
        foreach(GameObject blueprint in blueprints)
        {
            if(ColorMatched(blueprint.GetComponent<SpriteRenderer>().color) &&
                blueprint.name.Contains(goal.sprite.name))
            {
                Pause();
                OpenFinishModal();
            }
        }
    }

    private bool ColorMatched(Color color)
    {
        if (Mathf.Abs(color.r - goal.color.r) <= 10f/255 &&
            Mathf.Abs(color.g - goal.color.g) <= 10f/255 &&
            Mathf.Abs(color.b - goal.color.b) <= 10f/255)
        {
            return true;
        }
        else
            return false;
    }

    public void Open()
    {
        stateController.PlayTapSound();
        helpModal.SetActive(true);
        helpModal.GetComponent<HelpController>().Initialize();
        paused = true;
    }

    public void OpenFinishModal()
    {
        paused = true;
        finishModal.SetActive(true);
    }

    public void Close()
    {
        stateController.PlayTapSound();
        helpModal.SetActive(false);
        paused = false;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Unpause()
    {
        paused = false;
    }
}
