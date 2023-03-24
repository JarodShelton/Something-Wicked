using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene(2);
    }
    public void ToCredits(){
        SceneManager.LoadScene(1);
    }
    public void ToMenu(){
        SceneManager.LoadScene(0);
    }
    public void ToTutorial(){
        SceneManager.LoadScene(3);
    }
    public void Exit(){
        Application.Quit();
    }
}
