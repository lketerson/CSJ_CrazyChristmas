using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject historia;
    // Start is called before the first frame update


    void Start()
    {
        howToPlay.SetActive(false);
        mainMenu.SetActive(true);
        historia.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void VoltarMenu()
    {
        howToPlay.SetActive(false);
        mainMenu.SetActive(true);
        historia.SetActive(false);
    }

    public void DisplayHistoria()
    {

        howToPlay.SetActive(false);
        mainMenu.SetActive(false);
        historia.SetActive(true);
    }

    public void DisplayHowTo()
    {

        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
        historia.SetActive(false);
    }
}
