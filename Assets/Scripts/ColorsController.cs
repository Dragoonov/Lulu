using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsController : MonoBehaviour
{

    private const int GOAL_SCALE = 30;

    GameStateController stateController;

    public BlueprintController topLeftBlueprint;
    public BlueprintController topRightBlueprint;
    public BlueprintController bottomLeftBlueprint;
    public BlueprintController bottomRightBlueprint;
    public GameObject helpModal;
    public GameObject finishModal;
    public Image goal;
    public int level;

    GameObject holderCopy;
    public bool paused;

    private void Awake()
    {
        stateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
    }

    private void Start()
    {
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
            foreach (Touch touch in Input.touches)
            {

            }
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(objPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.name == "ColorHolder")
                {
                    holderCopy = Instantiate(hit.collider.gameObject, new Vector3(objPosition.x, objPosition.y, 5f), Quaternion.identity);
                    holderCopy.gameObject.transform.localScale = new Vector3(10, 10, 10);
                    ColorHolderController copyController = holderCopy.GetComponent<ColorHolderController>();
                    copyController.isDragging = true;
                    copyController.assignableShapes =
                        new HashSet<string>(GameObject.Find("ColorHolder").GetComponent<ColorHolderController>().assignableShapes);
                    DisableAssignableBlueprints();
                }
                else if (hit.collider == null || hit.collider.gameObject.name.Contains("Clone"))
                {
                    if (objPosition.x < 0 && objPosition.y > 0 && IsBlueprintActivated(topLeftBlueprint))
                    {
                        topLeftBlueprint.CreateShape(objPosition);
                    }
                    else if (objPosition.x > 0 && objPosition.y > 0 && IsBlueprintActivated(topRightBlueprint))
                    {
                        topRightBlueprint.CreateShape(objPosition);
                    }
                    else if (objPosition.x < 0 && objPosition.y < 0 && IsBlueprintActivated(bottomLeftBlueprint))
                    {
                        bottomLeftBlueprint.CreateShape(objPosition);
                    }
                    else if (objPosition.x > 0 && objPosition.y < 0 && IsBlueprintActivated(bottomRightBlueprint))
                    {
                        bottomRightBlueprint.CreateShape(objPosition);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (holderCopy != null)
                {
                    Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                    Color holderColor = holderCopy.GetComponent<SpriteRenderer>().color;
                    RaycastHit2D hit = Physics2D.Raycast(objPosition, Vector3.back, 10f);
                    Debug.Log("Hit: " + hit.collider.gameObject + ", started on position: " + objPosition);
                    if (hit.collider != null)
                    {
                        EnableAssignableBlueprints();
                        GameObject obj = hit.collider.gameObject;
                        Debug.Log(holderCopy.GetComponent<ColorHolderController>().assignableShapes.Count);
                        obj.GetComponent<SpriteRenderer>().color = holderColor;
                        Destroy(holderCopy);
                        holderCopy = null;
                    }
                }
            }
        }
    }

    private void CheckIfGoalMatched()
    {
        GameObject finalGoal = GameObject.Find(goal.sprite.name + "(Clone)");
        if(finalGoal != null)
        {
            if(finalGoal.GetComponent<SpriteRenderer>().color == goal.color && finalGoal.transform.localScale.x >= GOAL_SCALE)
            {
                Pause();
                OpenFinishModal();
            }
        }
    }

    private void DisableAssignableBlueprints()
    {
        if(holderCopy != null)
        {
            ColorHolderController copyController = holderCopy.GetComponent<ColorHolderController>();
            if (topLeftBlueprint.isActiveAndEnabled &&
                (topLeftBlueprint.Locked || !copyController.FindAcceptedShape(topLeftBlueprint.GetShapeName() + "(Clone)")))
            {
                topLeftBlueprint.DisableAssigning();
            }
            if (topRightBlueprint.isActiveAndEnabled &&
                (topRightBlueprint.Locked || !copyController.FindAcceptedShape(topRightBlueprint.GetShapeName() + "(Clone)")))
            {
                topRightBlueprint.DisableAssigning();
            }
            if (bottomLeftBlueprint.isActiveAndEnabled &&
                (bottomLeftBlueprint.Locked || !copyController.FindAcceptedShape(bottomLeftBlueprint.GetShapeName() + "(Clone)")))
            {
                bottomLeftBlueprint.DisableAssigning();
            }
            if (bottomRightBlueprint.isActiveAndEnabled &&
                (bottomRightBlueprint.Locked || !copyController.FindAcceptedShape(bottomRightBlueprint.GetShapeName() + "(Clone)")))
            {
                bottomRightBlueprint.DisableAssigning();
            }
        }
    }

    private void EnableAssignableBlueprints()
    {
        if (topLeftBlueprint.isActiveAndEnabled)
            topLeftBlueprint.EnableAssigning();
        if (topRightBlueprint.isActiveAndEnabled )
            topRightBlueprint.EnableAssigning();
        if (bottomLeftBlueprint.isActiveAndEnabled)
            bottomLeftBlueprint.EnableAssigning();
        if (bottomRightBlueprint.isActiveAndEnabled)
            bottomRightBlueprint.EnableAssigning();
    }

    private bool IsBlueprintActivated(BlueprintController controller)
    {
        return controller != null && controller.isActiveAndEnabled;
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
