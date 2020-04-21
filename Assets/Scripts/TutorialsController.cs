using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsController : MonoBehaviour
{
    [SerializeField]
    private ColorsController colorsController;

    public GameObject[] tutorials;

    private GameStateController stateController;

    void Start()
    {
        stateController = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        colorsController.paused = true;
        if(colorsController.level == 1)
        {
            tutorials[0].SetActive(true);
        }
        else if(colorsController.level == 3)
        {
            tutorials[3].SetActive(true);
        }
        else if(colorsController.level == 4)
        {
            tutorials[4].SetActive(true);
        }
        else if (colorsController.level == 5)
        {
            tutorials[5].SetActive(true);
        }
        else if (colorsController.level == 7)
        {
            tutorials[6].SetActive(true);
        }
        else
        {
            colorsController.paused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTutorial(GameObject tutorial)
    {
        if(tutorial.gameObject.name == "Tutorial1")
        {
            tutorial.SetActive(false);
            tutorials[1].SetActive(true);
        }
        else if (tutorial.gameObject.name == "Tutorial2")
        {
            tutorial.SetActive(false);
            tutorials[2].SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
            colorsController.paused = false;
        }
        stateController.PlayTapSound();
    }

}
