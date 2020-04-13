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

    AudioSource source;
    public bool muted;

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

}
