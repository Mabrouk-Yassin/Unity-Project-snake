using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    private Vector2 gridPosition;
    /// <summary>
    public Vector2 wordSize;
    public GameObject foodDot;
    private bool isGameOver = false;

    //ui
    public GameObject gameOverText;
    public GameObject restartButton;

    public GameObject gameOverTextstrat;
    public GameObject startButton;
    public GameObject snake;

    public GameObject scoreTextdeb;
    public GameObject scoreText;
    public GameObject bravolog;
    public GameObject restartButtonBravo;
    public GameObject quitButton;
   
    /// //////////////////
    /// </summary>
    Vector2 direction;
    Rigidbody2D rb;
    public float moveSpeed;
    public float rotateAmount;
    float rot;


    // Start is called before the first frame update
     void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

   
    }


    // Update is called once per frame
    void Update()
    {
        
        if (!isGameOver)
        {
            
            if (Input.GetMouseButton(0))
            {
                SoundManager.PlaySound(SoundManager.Sound.SnakeMove);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < 0)
                {
                    rot = rotateAmount;
                }
                else
                {
                    rot = -rotateAmount;
                }
                transform.Rotate(0, 0, rot);
            }
           
        }
  

    }
    private void FixedUpdate()
    {      
        rb.velocity = transform.up * moveSpeed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Destroy(collision.gameObject);
            ScoreSript.score += 10;
            SoundManager.PlaySound(SoundManager.Sound.SnakeEat);
            if (ScoreSript.score >= 30)
            {
                bravo();
                
                print("level complete");
            }
        }
  
       
            if (collision.gameObject.tag == "Danger")
            {
                SoundManager.PlaySound(SoundManager.Sound.SnakeDie);
                gameOver();
            }

    
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {

      
        if (collision.gameObject.tag == "wall" )
        {  
            translation();        
        }       
    }

    public void translation() {
          
        Vector3 pos = transform.position;
        if (pos.y > 6)
        {
            pos.y = pos.y - 13;    
        }
        else
        {
            if (pos.y < -6)
            {
                pos.y = pos.y + 13;
            }
            else
            {
                if (pos.x > 3)
                {
                    pos.x = pos.x - 7;
                }
                else
                {
                    if (pos.x < -3)
                    {                
                        pos.x = pos.x + 7;
                    }
                   
                }
            }
        }
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
         
    }

     
    private void gameOver() {
        isGameOver = true;
        gameOverText.SetActive(true);
        restartButton.SetActive(true);
        scoreText.SetActive(true);
        snake.SetActive(false);
        scoreTextdeb.SetActive(false);
        quitButton.SetActive(true);
        SoundManager.PlaySound(SoundManager.Sound.ButtonOver);
    }

    public void RestGame()
    {
      
        Application.LoadLevel(0);
        ScoreSript.score = 0;
        gameOverText.SetActive(false);
        restartButton.SetActive(false);
        scoreText.SetActive(false);
        snake.SetActive(true);
        scoreTextdeb.SetActive(true);
        quitButton.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
        
    }
    public void stratGame()
    {
        
        scoreTextdeb.SetActive(true);
        snake.SetActive(true);
        gameOverTextstrat.SetActive(false);
        startButton.SetActive(false);
        quitButton.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);
    }

    public void bravo()
    {
        //SoundManager.PaySound(SoundManager.Sound.ButtonClick);
        snake.SetActive(false);
        bravolog.SetActive(true);
        restartButtonBravo.SetActive(true);
        scoreTextdeb.SetActive(false);
        quitButton.SetActive(true);
    }

    public void RestGameBravo()
    {
      
        Application.LoadLevel(0);
        ScoreSript.score = 0;
        snake.SetActive(true);
        bravolog.SetActive(false);
        restartButtonBravo.SetActive(false);
        scoreTextdeb.SetActive(true);
        quitButton.SetActive(false);
        SoundManager.PlaySound(SoundManager.Sound.ButtonClick);

    }
   
}