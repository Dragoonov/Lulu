using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChooserController : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartLevel(int level)
    {
        if (level == 1)
        {
            //LevelDataHolder.setBottomLeftBlueprint(EffectType.DISABLE);
            //LevelDataHolder.setBottomRightBlueprint(EffectType.BLOCK, triangle, Color.cyan);
            //LevelDataHolder.setTopRightBlueprint(EffectType.LOCK, pentagon, Color.green);
            SceneManager.LoadScene("MainScene");
        }
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
}
