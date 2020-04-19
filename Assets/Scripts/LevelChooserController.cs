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
            Advertisement.Show();
            StartLevel(currentLevel + 1);
        }
    }

    public void StartLevel(int level)
    {
        if (level == 1)
        {
            //LevelDataHolder.setBottomLeftBlueprint(EffectType.DISABLE);
            //LevelDataHolder.setBottomRightBlueprint(EffectType.BLOCK, triangle, Color.cyan);
            //LevelDataHolder.setTopRightBlueprint(EffectType.LOCK, pentagon, Color.green);
            LevelDataHolder.SetLevel(level);
            LevelDataHolder.SetTopLeftBlueprint(Color.green);
            LevelDataHolder.SetGoal(circle, Color.white);
            SceneManager.LoadScene("MainScene");
        }
        if (level == 2)
        {
            LevelDataHolder.SetLevel(level);
            LevelDataHolder.SetTopLeftBlueprint(Color.red,EffectType.LOCK,square,Color.green);
            LevelDataHolder.SetTopRightBlueprint(Color.green, EffectType.BLOCK, triangle, Color.yellow);
            LevelDataHolder.SetBottomLeftBlueprint(Color.yellow);
            LevelDataHolder.SetGoal(circle, (Color.red + Color.green)/2);
            SceneManager.LoadScene("MainScene");
        }
        controller.PlayTapSound();
    }

    public void RestartLevel()
    {
        Advertisement.Show();
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
        StartLevel(lastLevel);
        controller.PlayTapSound();
    }
}
