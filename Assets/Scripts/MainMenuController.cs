using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    TextMeshProUGUI phoneNewGameButton;
    TextMeshProUGUI tabletNewGameButton;
    GameStateController controller;
    void Start()
    {
        controller = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        phoneNewGameButton = transform.Find("CanvasPhone").transform.Find("NewGameButton").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        tabletNewGameButton = transform.Find("CanvasTablet").transform.Find("NewGameButton").transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        int lastLevel = controller.LastLevel;
        if(lastLevel < 0)
        {
            phoneNewGameButton.GetComponent<TextMeshProUGUI>().text = "Play";
            tabletNewGameButton.GetComponent<TextMeshProUGUI>().text = "Play";
        }
        else
        {
            phoneNewGameButton.GetComponent<TextMeshProUGUI>().SetText("Continue\nlevel " + lastLevel);
            tabletNewGameButton.GetComponent<TextMeshProUGUI>().SetText("Continue\nlevel " + lastLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
