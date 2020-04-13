using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public Sprite triangle;
    public Sprite circle;
    public Sprite square;
    public Sprite pentagon;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevesMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

}
