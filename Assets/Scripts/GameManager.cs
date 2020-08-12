using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using JetBrains.Annotations;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor;

public class GameManager : MonoBehaviour, IUnityAdsListener
{
    //audio
    public AudioSource die;
    public AudioClip dieclip;
    bool playonce = true;
    //ads
    string GooglePlay_ID = "3709011";
    bool testMode = true;
    string myPlacementId = "rewardedVideo";
    static int kliknayescounter=0;
    //highscore
    int hs;
    //gameplay
    public Ball b1;
    public int scoree;
    public static int currentscore=0;
    public Text endtxt;
    public GameObject but;
    public GameObject backtomenubut;
    public Text highscoreText;
    bool destroy = false;
    bool izvrseno = false;
    public GameObject panel;
    public Button yes_button;
    public Button no_button;

    void Awake()
    {
        PlayerPrefs.GetInt("HighScore1", 0);
        hs = PlayerPrefs.GetInt("HighScore1", 0);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
        scoree = currentscore;
        Advertisement.AddListener(this);
        Advertisement.Initialize(GooglePlay_ID, testMode);
        panel.SetActive(false);
        highscoreText.enabled = false;
        endtxt.enabled = false;
        but.SetActive(false);
        backtomenubut.SetActive(false);
        
    }
    public int getScore()
    {
        return currentscore;
    }
    void klikNaYes()
    {
        if(Advertisement.IsReady(myPlacementId))
        { 
        kliknayescounter++;
        Advertisement.Show(myPlacementId);
        Debug.Log("Kliknuto na yes");
        }
        else
        {
            Debug.Log("video ad nije spreman");
        }
    }
    void klikNaNo()
    {

        if (destroy == false)
        {
            destroy = true;
            Destroy(b1.gameObject);
        }
        panel.SetActive(false);
        endtxt.text = "Game Over!\nYour score is:\n " + scoree;
        endtxt.enabled = true;
        if (scoree >hs ) //PlayerPrefs.GetInt("HighScore1", 0)
        { 
            highscoreText.enabled = true; 
        }
        but.SetActive(true);
        backtomenubut.SetActive(true);
        currentscore = 0;
        scoree = 0;
        kliknayescounter = 0;

    }
    void krajIgre()
    {
        
        if (scoree > PlayerPrefs.GetInt("HighScore1",0))
        { 
        PlayerPrefs.SetInt("HighScore1",scoree);
            
        }
        currentscore = scoree;
        
        if (izvrseno == false) 
        {
        izvrseno = true;
        b1.gameObject.SetActive(false);
        if(kliknayescounter>=2 || !Advertisement.IsReady(myPlacementId))
            {
                klikNaNo();

            }
        else
            {
                panel.SetActive(true);
                yes_button.onClick.AddListener(klikNaYes);
                no_button.onClick.AddListener(klikNaNo);
            }
        
        }

    }
    public void resett()
    {
        if(Advertisement.IsReady())
        { 
        Advertisement.Show();
        SceneManager.LoadScene("Game");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void backtomenu()
    {
        
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if(b1.hp<=0f)
        {
            if (playonce)
            {
                playonce = false;
                die.PlayOneShot(dieclip);

            }
            
            krajIgre();
        }
    }
    
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            
            SceneManager.LoadScene("Game");

            // Reward the user for watching the ad to completion.
        }
        else if (showResult == ShowResult.Skipped && placementId==myPlacementId)
        {
            klikNaNo();
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            scoree = 0;
            currentscore = 0;
            SceneManager.LoadScene("Menu");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
