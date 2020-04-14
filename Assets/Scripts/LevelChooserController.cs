using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
    public void StartLevel(int level)
    {
        if (level == 1)
        {
            //LevelDataHolder.setBottomLeftBlueprint(EffectType.DISABLE);
            //LevelDataHolder.setBottomRightBlueprint(EffectType.BLOCK, triangle, Color.cyan);
            //LevelDataHolder.setTopRightBlueprint(EffectType.LOCK, pentagon, Color.green);
            LevelDataHolder.SetLevel(level);
            LevelDataHolder.SetTopLeftBlueprint(Color.white);
            LevelDataHolder.SetGoal(circle, Color.white);
            SceneManager.LoadScene("MainScene");
            controller.PlayTapSound();
        }
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
    }
}
