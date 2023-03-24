using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject deathScreen;

    public bool gamePaused = false;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        unpauseGame();
        hideDeathScreen();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if(!gamePaused)
                pauseGame();
            else
                unpauseGame();
        }
    }
    public void showDeathScreen()
    {
        gameOver = true;
        pauseScreen.SetActive(false);
        deathScreen.SetActive(true);
        FindObjectOfType<DeathScreenManager>().GetComponent<DeathScreenManager>().updateCandyText();
        gamePaused = true;
        Time.timeScale = 0f;
        
        
    }
    void hideDeathScreen(){
        Time.timeScale = 1f;
        deathScreen.SetActive(false);
    }
    public void pauseGame()
    {
        gamePaused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
    public void unpauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
    public void goToMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
