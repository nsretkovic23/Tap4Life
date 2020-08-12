using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{   //startovanje igre

    public AudioSource tapp;

    public GameObject startPanel;
    public GameManager gm;

    
    public Text textt;
    
    public float accelerationTime = 1f;
    public float maxSpeed = 4f;
    private Vector2 movement;
    public float timeLeft;
    public Rigidbody2D rb;
    public HealthBar hp_bar;
      
    public float hp=100f;
    
    // Start is called before the first frame update
    void Start()
    {
        
        Application.targetFrameRate = 60;
        startPanel.SetActive(true);
        gameObject.SetActive(false);
        hp_bar.setmaxhp(hp);
        rb = GetComponent<Rigidbody2D>();
        rb.position =new Vector2(Random.Range(-2f, 2f),Random.Range(-3f,3f));
        textt.text = "Score: "+gm.getScore();   
    }

    public void startDugme()
    {
        startPanel.SetActive(false);
        gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        tapp.Play();
        hp += 60f;
        if (hp>100f)
        {
            hp = 100f;
        }
        
        gm.scoree++;
       
        textt.text = "Score: " + gm.scoree;    
    }
    // Update is called once per frame  
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            timeLeft += accelerationTime;
        }
        
        hp -= 0.37f;//hp

        hp_bar.setHP(hp);

        if (hp <= 0)
        {
            
            //gameObject.SetActive(false);
            //Destroy(gameObject);
            textt.enabled = false;
            
        }

    }
    private void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
        
    }
}
