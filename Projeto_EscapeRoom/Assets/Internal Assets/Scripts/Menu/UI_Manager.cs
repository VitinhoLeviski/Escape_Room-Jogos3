using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;



public class UI_Manager : MonoBehaviour
{
    public EventReference menuSound;
    public EventReference starGameSound;
    [SerializeField] private GameObject PNG_starterMenu;
    [SerializeField] private GameObject starterMenuButtons;
    [SerializeField] private GameObject settingsMenuButtons;



    //Funções da Unity gerais



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void game()
    {
        SceneManager.LoadScene("CutScene");
    }



    //botões do menu



    public void SettingsMenu()
    {
        RuntimeManager.PlayOneShot(menuSound);
        starterMenuButtons.SetActive(false);
        settingsMenuButtons.SetActive(true);
    }

    public void backStarterMenu()
    {
        RuntimeManager.PlayOneShot(menuSound);
        starterMenuButtons.SetActive(true);    
    }



    //trocar de cenas



    public void Play()
    {
        RuntimeManager.PlayOneShot(starGameSound);
        Invoke("game", 2f);
    }
    public void closeGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
