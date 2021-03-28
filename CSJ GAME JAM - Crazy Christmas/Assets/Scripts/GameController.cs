using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject[] colorsPick; //White, yellow, pink, blue
    public GameObject player;
    public GameObject[] stars;

    int _randomColorPick;

    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI pointsDisplay;

    public GameObject _gameOver;
    public GameObject _gameInfoContainer;

    public TextMeshProUGUI scoreMessage;

    float colorChange;
    float colorChangeTimer;
    float _fadeTimer;
    float preSpeed;

    [HideInInspector]
    public bool isBlue, isYellow, isWhite, isPink;

    bool _isFaded;
    public bool isEnded;

    public Color noFade;
    public Color faded;

    public Image timeBar;

   
    AudioSource _sceneMusic;
    public AudioSource buttonAudio;

    // Start is called before the first frame update
    void Start()
    {
        //_randomColorPick = Random.Range(0, 4);
        //colorsPick[_randomColorPick].SetActive(true);
        _sceneMusic = GetComponent<AudioSource>();
       
        StartCoroutine(ColorPick());
        _isFaded = false;
        _sceneMusic.Play();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        colorChangeTimer -= Time.deltaTime;
        _fadeTimer += Time.deltaTime;
        TimerDisplay();
        DisplayPoints();
        FadeAway();
         
        
    }
    IEnumerator ColorPick()
    {
        _randomColorPick = Random.Range(0, 4);
        colorsPick[_randomColorPick].SetActive(true);
        switch (_randomColorPick)
        {
            case 0:
                colorsPick[1].SetActive(false);
                colorsPick[2].SetActive(false);
                colorsPick[3].SetActive(false);
                isWhite = true;
                isBlue = false;
                isYellow = false;
                isPink = false;
                break;
            case 1:
                colorsPick[0].SetActive(false);
                colorsPick[2].SetActive(false);
                colorsPick[3].SetActive(false);
                isWhite = false;
                isBlue = false;
                isYellow = true;
                isPink = false;
                break;
            case 2:
                colorsPick[0].SetActive(false);
                colorsPick[1].SetActive(false);
                colorsPick[3].SetActive(false);
                isWhite = false;
                isBlue = false;
                isYellow = false;
                isPink = true;
                break;
            case 3:
                colorsPick[0].SetActive(false);
                colorsPick[1].SetActive(false);
                colorsPick[2].SetActive(false);
                isWhite = false;
                isBlue = true;
                isYellow = false;
                isPink = false;
                break;
        }
        colorChange = Random.Range(5, 10);
        
        colorChangeTimer = colorChange;
        yield return new WaitForSeconds(colorChange);
       
        StartCoroutine(ColorPick());


    }


    void TimerDisplay()
    {
        timeDisplay.text = colorChangeTimer.ToString("0.00");
    }

    void DisplayPoints()
    {
        //var playerPoints = ;
        pointsDisplay.text = player.GetComponent<PlayerMove>().points.ToString();
    }




    public void FadeAway()
    {       
        if (!_isFaded && Input.GetKeyDown(KeyCode.Space) && _fadeTimer >30f)
        {
            _fadeTimer = 0;
            player.GetComponent<CapsuleCollider2D>().enabled = false;
            player.GetComponent<SpriteRenderer>().color = faded;   
            preSpeed = player.GetComponent<PlayerMove>().speed;
            player.GetComponent<PlayerMove>().speed = 8;
            _isFaded = true;
        }
        else if(_fadeTimer > 5f && _isFaded)
        {
            player.GetComponent<CapsuleCollider2D>().enabled =true;
            player.GetComponent<SpriteRenderer>().color = noFade;
            _isFaded = false;
            player.GetComponent<PlayerMove>().speed = preSpeed;
            _fadeTimer = 0f;      
        }

        if (_isFaded)
        {
            
            timeBar.fillAmount -= Time.deltaTime/5;
            timeBar.color = Color.red;
        }
        else
        {
            timeBar.fillAmount = _fadeTimer / 30;
            timeBar.color = Color.green;
        }
    }


    public void DisplayGameOver()
    {
        scoreMessage.text = player.GetComponent<PlayerMove>().points.ToString("VOCÊ SALVOU\n" + player.GetComponent<PlayerMove>().points + "\nPRESENTES");
        _gameOver.SetActive(isEnded);
        _gameInfoContainer.SetActive(!isEnded);
        Time.timeScale = 0f;
        _sceneMusic.Stop();

        if (player.GetComponent<PlayerMove>().points ==0)
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }
        else if (player.GetComponent<PlayerMove>().points <= 10)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
        }
        else if (player.GetComponent<PlayerMove>().points <= 29)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(false);
        }
        else if (player.GetComponent<PlayerMove>().points >= 30)
        {
            stars[0].SetActive(true);
            stars[1].SetActive(true);
            stars[2].SetActive(true);
        }

    }

    public void RestartButton()
    {
        buttonAudio.Play();
        _gameOver.SetActive(!isEnded);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
   
}
