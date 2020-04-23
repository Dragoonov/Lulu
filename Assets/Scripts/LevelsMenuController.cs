using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMenuController : MonoBehaviour
{
    public const int LEVEL_AMOUNT = 20;

    void Start()
    {
        int lastLevel = GameObject.Find("GameStateController").GetComponent<GameStateController>().LastLevel;
        if(lastLevel < 0)
        {
            lastLevel = 1;
        }
        for (int i = lastLevel + 1; i <= LEVEL_AMOUNT; i++)
        {
            transform.Find("Button (" + i + ")").GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
