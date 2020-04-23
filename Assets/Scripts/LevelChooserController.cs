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
        if (currentLevel < 20)
        {
            controller.ShowAd();
            StartLevel(currentLevel + 1);
        }
        else
            StartMainMenu();
    }

    public void StartLevel(int level)
    {
        LevelDataHolder.SetLevel(level);
        //Tutorial spawnowanie
        //Tutorial helpPanel
        //Tutorial mieszanie
        if (level == 1)
        {
            LevelDataHolder.SetTopLeftBlueprint(Color.green);
            LevelDataHolder.SetTopRightBlueprint(Color.blue);
            LevelDataHolder.SetBottomLeftBlueprint(Color.yellow);
            LevelDataHolder.SetBottomRightBlueprint(Color.red);
            LevelDataHolder.SetGoal(triangle, (Color.yellow + Color.green)/2);
        }
        if (level == 2)
        {
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(Color.green);
            LevelDataHolder.SetBottomLeftBlueprint(Color.yellow);
            LevelDataHolder.SetBottomRightBlueprint(Color.blue);
            LevelDataHolder.SetGoal(pentagon, (Color.blue + Color.green)/2);
        }
        //Tutorial limit
        if (level == 3)
        {
            Color goal = (((Color.red + Color.green) / 2) + Color.blue) / 2;
            LevelDataHolder.SetTopLeftBlueprint(Color.red,3);
            LevelDataHolder.SetTopRightBlueprint(Color.green);
            LevelDataHolder.SetBottomLeftBlueprint(Color.yellow);
            LevelDataHolder.SetBottomRightBlueprint(Color.blue);
            LevelDataHolder.SetGoal(circle, goal);
        }
        //Tutorial lock
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
        //Tutorial block
        if (level == 5) 
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
        //Tutorial tempHolder
        if (level == 7)
        {
            Color goal = Color.blue;
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(Color.blue);
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.yellow);
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue);
            LevelDataHolder.SetGoal(triangle, goal);
            LevelDataHolder.SetTempHolderData(Color.gray);
        }

        if (level == 8)
        {
            Color goal = (Color.yellow + Color.red) / 2;
            LevelDataHolder.SetTopLeftBlueprint(Color.red);
            LevelDataHolder.SetTopRightBlueprint(Color.blue);
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.yellow);
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.green);
            LevelDataHolder.SetGoal(pentagon, goal);
            LevelDataHolder.SetTempHolderData(Color.gray);
        }

        if (level == 9)
        {
            Color goal = (Color.yellow + Color.blue)/2;
            LevelDataHolder.SetTopLeftBlueprint(Color.yellow);
            LevelDataHolder.SetTopRightBlueprint(Color.magenta);
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.magenta);
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue,
                effect: EffectType.BLOCK,
                sprite: circle,
                color:(Color.yellow + Color.magenta)/2);
            LevelDataHolder.SetGoal(pentagon, goal);
            LevelDataHolder.SetTempHolderData(Color.yellow);
        }
        if (level == 10)
        {
            Color goal = Color.magenta;
            LevelDataHolder.SetTopLeftBlueprint(
                Color.magenta,
                effect: EffectType.BLOCK,
                sprite: pentagon,
                color: (Color.yellow + Color.magenta) / 2);
            LevelDataHolder.SetTopRightBlueprint(Color.magenta);
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.magenta);
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.magenta);
            LevelDataHolder.SetGoal(circle, goal);
            LevelDataHolder.SetTempHolderData(Color.yellow);
        }
        if (level == 11)
        {
            Color goal = Color.blue;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.red,
                effect: EffectType.LOCK,
                sprite: triangle,
                color: Color.magenta
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.blue,
                effect: EffectType.BLOCK,
                sprite: pentagon,
                color: Color.magenta
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.yellow,
                effect: EffectType.BLOCK,
                sprite: circle,
                color: Color.magenta
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.green,
                effect: EffectType.BLOCK,
                sprite: square,
                color: Color.magenta
                );
            LevelDataHolder.SetGoal(circle, goal);
            LevelDataHolder.SetTempHolderData(Color.blue);
        }
        if (level == 12)
        {
            Color goal = Color.blue;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.red, 1
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.yellow, 2
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.cyan, 1
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue,
                limit: 1,
                effect: EffectType.BLOCK,
                sprite: square,
                color: (Color.red + Color.yellow)/2
                );
            LevelDataHolder.SetGoal(pentagon, goal);
        }
        if (level == 13)
        {
            Color goal = (Color.blue + Color.cyan) / 2;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.red
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.yellow
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.green, 2
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue,
                limit: 2,
                effect: EffectType.BLOCK,
                sprite: square,
                color: (((Color.red + Color.green) / 2) + Color.yellow)/2
                );
            LevelDataHolder.SetGoal(pentagon, goal);
            LevelDataHolder.SetTempHolderData(Color.cyan);
        }
        if (level == 14)
        {
            Color goal = (Color.cyan + Color.magenta) / 2;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.cyan,
                limit: 2,
                effect: EffectType.BLOCK,
                sprite: pentagon,
                color: (((Color.blue + Color.green) / 2) + Color.red) / 2
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.green,
                limit: 2
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.blue
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.red
               // limit: 2
                //effect: EffectType.BLOCK,
               // sprite: square,
               // color: (((Color.red + Color.green) / 2) + Color.yellow) / 2
                );
            LevelDataHolder.SetGoal(circle, goal);
            LevelDataHolder.SetTempHolderData(Color.magenta);
        }
        if (level == 15)
        {
            Color goal = Color.green;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.red,
               // limit: 2,
                effect: EffectType.LOCK,
                sprite: triangle,
                color: (((Color.blue + Color.yellow) / 2) + Color.cyan) / 2
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.green,
                //limit: 2
                effect: EffectType.BLOCK,
                sprite: pentagon,
                color: (Color.red + Color.cyan) / 2
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.blue
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.yellow
                // limit: 2
                //effect: EffectType.BLOCK,
                // sprite: square,
                // color: (((Color.red + Color.green) / 2) + Color.yellow) / 2
                );
            LevelDataHolder.SetGoal(square, goal);
            LevelDataHolder.SetTempHolderData(Color.cyan);
        }
        if (level == 16)
        {
            Color goal = (Color.blue + (Color.yellow + Color.cyan) / 2) /2;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.magenta
                // limit: 2,
                //effect: EffectType.LOCK,
                //sprite: triangle,
                //color: (((Color.blue + Color.yellow) / 2) + Color.cyan) / 2
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.blue,
                //limit: 2
                effect: EffectType.BLOCK,
                sprite: circle,
                color: (Color.yellow + Color.cyan) / 2
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.yellow,
                //limit: 2
                effect: EffectType.LOCK,
                sprite: pentagon,
                color: (((Color.magenta + Color.red) / 2) + Color.cyan) / 2
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.red
                // limit: 2
                //effect: EffectType.BLOCK,
                // sprite: square,
                // color: (((Color.red + Color.green) / 2) + Color.yellow) / 2
                );
            LevelDataHolder.SetGoal(square, goal);
            LevelDataHolder.SetTempHolderData(Color.cyan);
        }
        if (level == 17)
        {
            Color goal = ((((Color.red + Color.yellow) / 2 + Color.yellow) / 2) + Color.blue) / 2;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.red,
                limit: 1
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.yellow
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.cyan,
                limit: 1,
                effect: EffectType.LOCK,
                sprite: pentagon,
                color: Color.magenta
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue,
                effect: EffectType.BLOCK,
                sprite: square,
                color: ((Color.red + Color.yellow) / 2 + Color.yellow) / 2
                );
            LevelDataHolder.SetGoal(pentagon, goal);
            LevelDataHolder.SetTempHolderData(Color.gray);
        }
        if (level == 18)
        {
            Color goal = (Color.yellow + Color.red) / 2;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.green,
                effect: EffectType.LOCK,
                sprite: circle,
                color: (Color.yellow + Color.magenta) / 2
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.yellow
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.magenta,
                sprite: pentagon,
                color: Color.magenta
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.red,
                effect: EffectType.BLOCK,
                sprite: circle,
                color: ((Color.magenta + Color.yellow) / 2 + Color.magenta) / 2
                );
            LevelDataHolder.SetGoal(square, goal);
            LevelDataHolder.SetTempHolderData(Color.gray);
        }
        if (level == 19)
        {
            Color goal = ((((((Color.red + Color.yellow) / 2 + Color.green) / 2) + Color.blue) / 2) + Color.yellow) / 2;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.red
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.yellow
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.green
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue
                );
            LevelDataHolder.SetGoal(pentagon, goal);
        }
        if (level == 20)
        {
            Color goal = ((((((((Color.blue + Color.yellow) / 2 + Color.red) / 2) + Color.blue) / 2) + Color.red) / 2) + Color.green) / 2;
            LevelDataHolder.SetTopLeftBlueprint(
                colorOwn: Color.red
                );
            LevelDataHolder.SetTopRightBlueprint(
                colorOwn: Color.yellow
                );
            LevelDataHolder.SetBottomLeftBlueprint(
                colorOwn: Color.green
                );
            LevelDataHolder.SetBottomRightBlueprint(
                colorOwn: Color.blue
                );
            LevelDataHolder.SetGoal(triangle, goal);
        }
        controller.PlayTapSound();
        controller.ShowAd();
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
