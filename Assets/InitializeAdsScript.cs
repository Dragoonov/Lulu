using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
{

    string gameId = "3562400";
    bool testMode = true;

    void Start()
    {
        Advertisement.Initialize(gameId, testMode);
    }
}
