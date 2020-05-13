using UnityEngine;

public class BlockController : MonoBehaviour
{
    BlueprintController parent;
    GameObject unblockShape;
    string targetName;
    Color targetColor;
    GameObject[] desiredShapes;

    private void Awake()
    {
        parent = transform.parent.gameObject.GetComponent<BlueprintController>();
        unblockShape = transform.Find("UnblockShape").gameObject;
        targetName = unblockShape.GetComponent<SpriteRenderer>().sprite.name;
        desiredShapes = GameObject.FindGameObjectsWithTag("Blueprint");
    }

    public void SetShape(Sprite sprite, Color color)
    {
        SpriteRenderer renderer = unblockShape.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.color = color;
        targetName = sprite.name;
        targetColor = color;
        SetBackground(sprite);
    }

    private void SetBackground(Sprite sprite)
    {
        SpriteRenderer background = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        background.sprite = sprite;
        background.color = Color.black;
        background.transform.localScale = new Vector3(
            1.2f,
            1.2f,
            1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject desiredShape in desiredShapes)
        {
            if (desiredShape != null &&
                desiredShape.name == targetName &&
                desiredShape.GetComponent<SpriteRenderer>().color == targetColor)
            {
                parent.state.Blocked = false;
                break;
            }
        }
    }
}
