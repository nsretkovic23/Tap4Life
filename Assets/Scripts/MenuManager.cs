using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public Text high_txt;
    

    private void Awake()
    { 
        
        high_txt.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore1");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

   public void ResetHS()
    {
        PlayerPrefs.DeleteKey("HighScore1");
        high_txt.text = "HIGHSCORE: " + PlayerPrefs.GetInt("HighScore1");
    }

    public void startuj()
    {
        SceneManager.LoadScene("Game");
    }
    public void izlaz()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
