using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{

    AudioSource source;
    public bool muted;
    private const string LAST_LEVEL_KEY = "last_level";
    public int adCounter = 0;
    public int LastLevel
    {
        get
        {
           return GetLastLevel();
        }
        set
        {
            SaveLastLevel(value);
        }
    }

    public void ShowAd()
    {
        adCounter++;
        if(adCounter >= 3)
        {
            Advertisement.Show();
            adCounter = 0;
        }
    }

    private static GameObject instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this.gameObject;
            source = GetComponent<AudioSource>();
            muted = false;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTapSound()
    {
        if (!muted)
            source.Play();
    }

    public void ClearUserData()
    {
        PlayerPrefs.DeleteAll();
    }

    private void SaveLastLevel(int level)
    {
        PlayerPrefs.SetInt(LAST_LEVEL_KEY, level);
    }

    private int GetLastLevel()
    {
        return PlayerPrefs.GetInt(LAST_LEVEL_KEY,-1);
    }

}
