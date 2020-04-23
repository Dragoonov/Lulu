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
            HandleTouch();
            //HandleMouse();
        }
    }

    private void HandleTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(touch.position);
                HandleInputDown(objPosition, touch.fingerId);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (holderCopy != null || (tempColorHolder!= null && tempColorHolder.isDragging))
                {
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    HandleInputUp(objPosition);
                }
            }
        }
    }

    private void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            HandleInputDown(objPosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (holderCopy != null)
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                HandleInputUp(objPosition);
            }
        }
    }

    private void CheckIfGoalMatched()
    {
        GameObject[] finalGoals = GameObject.FindGameObjectsWithTag("Spawned");
        foreach(GameObject finalGoal in finalGoals)
        {
            if(finalGoal.GetComponent<SpriteRenderer>().color == goal.color &&
                finalGoal.transform.localScale.x >= GOAL_SCALE &&
                finalGoal.name.Contains(goal.sprite.name))
            {
                Pause();
                OpenFinishModal();
            }
        }
    }

    private bool ColorMatched(Color color)
    {
        if (Mathf.Abs(color.r - goal.color.r) <= 10 &&
            Mathf.Abs(color.g - goal.color.g) <= 10 &&
            Mathf.Abs(color.b - goal.color.b) <= 10)
            return true;
        else
            return false;
    }

    private void HandleInputDown(Vector2 objPosition, int fingerId = 0)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(objPosition, Vector3.back, 10f);
        RaycastHit2D holderHit = new RaycastHit2D();
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.name == "ColorHolder" || hit.collider.gameObject.name == "TempColorHolder")
                holderHit = hit;
        }
        if (holderHit.collider != null && holderHit.collider.gameObject.name == "ColorHolder")
        {
            holderCopy = Instantiate(holderHit.collider.gameObject, new Vector3(objPosition.x, objPosition.y, 5f), Quaternion.identity);
            holderCopy.gameObject.transform.localScale = new Vector3(10, 10, 10);
            ColorHolderController copyController = holderCopy.GetComponent<ColorHolderController>();
            copyController.isDragging = true;
            copyController.assignableShapes =
                new HashSet<string>(GameObject.Find("ColorHolder").GetComponent<ColorHolderController>().assignableShapes);
            DisableAssignableBlueprints();
        }
        if (holderHit.collider != null && holderHit.collider.gameObject.name == "TempColorHolder")
        {
            tempColorHolder.isDragging = true;
        }
        else if (holderHit.collider == null ||
            (holderHit.collider.gameObject.name != "ColorHolder" &&
            holderHit.collider.gameObject.name != "TempColorHolder"))
        {
            if (objPosition.x < 0 && objPosition.y > 0 && IsBlueprintActivated(topLeftBlueprint))
            {
                topLeftBlueprint.CreateShape(objPosition, fingerId);
            }
            else if (objPosition.x > 0 && objPosition.y > 0 && IsBlueprintActivated(topRightBlueprint))
            {
                topRightBlueprint.CreateShape(objPosition, fingerId);
            }
            else if (objPosition.x < 0 && objPosition.y < 0 && IsBlueprintActivated(bottomLeftBlueprint))
            {
                bottomLeftBlueprint.CreateShape(objPosition, fingerId);
            }
            else if (objPosition.x > 0 && objPosition.y < 0 && IsBlueprintActivated(bottomRightBlueprint))
            {
                bottomRightBlueprint.CreateShape(objPosition, fingerId);
            }
        }
    }

    private void HandleInputUp(Vector2 objPosition)
    {
        Color holderColor = new Color();
        RaycastHit2D[] hits = Physics2D.RaycastAll(objPosition, Vector3.back, 10f);
        RaycastHit2D hit = new RaycastHit2D();
        foreach (RaycastHit2D hite in hits)
        {
            if (hite.collider.gameObject.layer == LayerMask.NameToLayer("Blueprint") || hite.collider.gameObject.name == "TempColorHolder")
            {
                hit = hite;
            }
        }
        if (holderCopy != null)
        {
            Debug.Log("Pierwszy komentarz");
            EnableAssignableBlueprints();
            holderColor = holderCopy.GetComponent<SpriteRenderer>().color;
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Blueprint"))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name + ", started on position: " + objPosition);
                GameObject obj = hit.collider.gameObject;
                Debug.Log(holderCopy.GetComponent<ColorHolderController>().assignableShapes.Count);
                obj.GetComponent<SpriteRenderer>().color = holderColor;
                obj.GetComponent<BlueprintController>().tempColor = holderColor;
            }
            if (hit.collider != null && hit.collider.gameObject.name == "TempColorHolder")
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name + ", started on position: " + objPosition);
                GameObject obj = hit.collider.gameObject;
                obj.GetComponent<SpriteRenderer>().color = holderColor;
            }
            Destroy(holderCopy);
            holderCopy = null;
        }
        else if (tempColorHolder != null && tempColorHolder.isDragging)
        {
            Debug.Log("Drugi komentarz");
            holderColor = tempColorHolder.GetComponent<SpriteRenderer>().color;
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Blueprint"))
            {
                Debug.Log("Trzeci komentarz");
                Debug.Log("Hit: " + hit.collider.gameObject.name + ", started on position: " + objPosition);
                GameObject obj = hit.collider.gameObject;
                obj.GetComponent<SpriteRenderer>().color = holderColor;
                obj.GetComponent<BlueprintController>().tempColor = holderColor;
                Destroy(tempColorHolder.gameObject);
                tempColorHolder = null;
            }
            else
                tempColorHolder.Drop();
        }
        Debug.Log("Czwarty komentarz");
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
