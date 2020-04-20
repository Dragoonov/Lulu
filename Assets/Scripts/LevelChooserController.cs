using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class LevelChooserController : MonoBehaviour
{
    
    GameStateController controller;
    GameObject objUnmute;
    GameObject objMute;

    public Sprite triangle;
    public Sprite circle;
    public Sprite square;
    public Sprite pentagon;

    int currentLevel;

    private void Start()
    {
        controller = GameObject.Find("GameStateController").GetComponent<GameStateController>();
        objUnmute = GameObject.Find("UnmuteImage");
        objMute = GameObject.Find("MuteImage");
        if (objMute != null && objUnmute != null)
        {
            objUnmute.SetActive(!controller.muted);
            objMute.SetActive(controller.muted);
        }
        ColorsController colorsController = GetComponent<ColorsController>();
        if(colorsController != null)
        {
            currentLevel = colorsController.level;
        }
    }

    public void StartNextLevel()
    {
        if(currentLevel < 20)
        {
            controller.ShowAd();
            StartLevel(currentLevel + 1);
        }
    }

    public void StartLevel(int level)
    {
        LevelDataHolder.SetLevel(level);
        if (level == 1)
        {
            LevelDataHolder.SetTopLeftBlueprint(Color.green);
            LevelDataHolder.SetTopRightBlueprint(effect: EffectType.DISABLE);
            LevelDataHolder.SetBottomLeftBlueprint(effect: EffectType.DISABLE);
            LevelDataHolder.SetBottomRightBlueprint(effect: EffectType.DISABLE);
            LevelDataHolder.SetGoal(circle, Color.green);
        }
        if (level == 2)
        {
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(Color.green);
            LevelDataHolder.SetBottomLeftBlueprint(Color.yellow);
            LevelDataHolder.SetBottomRightBlueprint(Color.blue);
            LevelDataHolder.SetGoal(square, (Color.red + Color.green)/2);
        }
        if (level == 3)
        {
            Color goal = (((Color.red + Color.green) / 2) + Color.blue) / 2;
            LevelDataHolder.SetTopLeftBlueprint(Color.red,4);
            LevelDataHolder.SetTopRightBlueprint(Color.green);
            LevelDataHolder.SetBottomLeftBlueprint(Color.yellow);
            LevelDataHolder.SetBottomRightBlueprint(Color.blue);
            LevelDataHolder.SetGoal(circle, goal);
        }
        if (level == 4)
        {
            Color goal = (Color.green + Color.yellow) / 2;
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(Color.green);
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.yellow,
                effect: EffectType.LOCK,
                sprite: circle,
                color: (Color.red + Color.blue)/2);
            LevelDataHolder.SetBottomRightBlueprint(Color.blue);
            LevelDataHolder.SetGoal(triangle, goal);
        }
        if (level == 5) //do poprawy
        {
            Color goal = (Color.red + Color.blue) / 2;
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(Color.green);
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.yellow,
                effect: EffectType.LOCK,
                sprite: square,
                color: (Color.red + Color.green) / 2);
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue,
                effect: EffectType.BLOCK,
                sprite: triangle,
                color: (Color.yellow + Color.red) / 2);
            LevelDataHolder.SetGoal(circle, goal);
        }
        if (level == 6)
        {
            Color goal = Color.magenta;
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.blue,
                effect: EffectType.BLOCK,
                sprite: triangle,
                color: (((Color.green + Color.red) / 2) + Color.green) / 2);
            LevelDataHolder.SetBottomLeftBlueprint(Color.green);
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.magenta,
                effect: EffectType.BLOCK,
                sprite: triangle,
                color: (Color.blue + ((((Color.green + Color.red) / 2) + Color.green) / 2))/2);
            LevelDataHolder.SetGoal(pentagon, goal);
        }
        if (level == 7)
        {
            Color goal = (Color.green + Color.yellow) / 2;
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(Color.green);
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.yellow,
                effect: EffectType.LOCK,
                sprite: circle,
                color: (Color.red + Color.green) / 2);
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue,
                effect: EffectType.BLOCK,
                sprite: triangle,
                color: (Color.yellow + Color.red) / 2);
            LevelDataHolder.SetGoal(triangle, goal);
        }
        controller.PlayTapSound();
        SceneManager.LoadScene("MainScene");
    }

    public void RestartLevel()
    {
        controller.ShowAd();
        StartLevel(currentLevel);
    }

    public void StartMainMenu()
    {
        controller.PlayTapSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void Mute()
    {
        controller.muted = !controller.muted;
        objUnmute.SetActive(!controller.muted);
        objMute.SetActive(controller.muted);
        controller.PlayTapSound();
    }    

    public void StartLevelsMenu()
    {
        controller.PlayTapSound();
        SceneManager.LoadScene("LevelsMenu");
    }

    public void StartOptionsMenu()
    {
        controller.PlayTapSound();
        SceneManager.LoadScene("OptionsMenu");
    }

    public void QuitGame()
    {
        controller.PlayTapSound();
        Application.Quit();
    }

    public void StartGame()
    {
        int lastLevel = controller.LastLevel;
        if (lastLevel < 0)
        {
            lastLevel = 1;
        }
        StartLevel(lastLevel);
        controller.PlayTapSound();
    }
}
