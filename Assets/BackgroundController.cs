using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour, IClickable
{
    List<BlueprintController> blueprints;
    ColorHolderController holder;
    TempColorHolderController tempHolder;

    public delegate void SelectAction();
    public event SelectAction OnSelected;

    public void OnClicked()
    {
        OnSelected();
        foreach (BlueprintController blueprintController in blueprints)
        {
            blueprintController.state.Selected = false;
            blueprintController.state.Highlighted = false;
        }
        if(tempHolder != null && tempHolder.state != null)
        {
            tempHolder.state.Selected = false;
            tempHolder.state.Highlighted = false;
        }
    }

    private void Awake()
    {
        blueprints = new List<BlueprintController>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Blueprint");
        foreach(GameObject tem in temp)
        {
            blueprints.Add(tem.GetComponent<BlueprintController>());
        }
        holder = GameObject.FindGameObjectWithTag("Holder").GetComponent<ColorHolderController>();
        tempHolder = GameObject.FindGameObjectWithTag("TempHolder").GetComponent<TempColorHolderController>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
