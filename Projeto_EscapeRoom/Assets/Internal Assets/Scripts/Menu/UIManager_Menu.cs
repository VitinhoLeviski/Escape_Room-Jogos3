using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class UIManager_Menu : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button setButton;
    [SerializeField] EventReference menuSound;
    [SerializeField] EventReference starGameSound;
    [SerializeField] GameObject TextNome;

    [SerializeField] GameObject ControlesImage;
    [SerializeField] private GameObject PNG_starterMenu;
    [SerializeField] private GameObject starterMenuButtons;
    [SerializeField] private GameObject settingsMenuButtons;



    //Funções da Unity 



    void Start()
    {
        
    }

    void Update()
    {

    }

    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }



    //botões do menu



    public void SettingsMenu()
    {
        RuntimeManager.PlayOneShot(menuSound);
        starterMenuButtons.SetActive(false);
        settingsMenuButtons.SetActive(true);
        TextNome.SetActive(true);
    }

    public void backStarterMenu()
    {
        RuntimeManager.PlayOneShot(menuSound);
        starterMenuButtons.SetActive(true);
        settingsMenuButtons.SetActive(false);
        TextNome.SetActive(false);
        ControlesImage.SetActive(false);
    }

    public void Controls()
    {
        RuntimeManager.PlayOneShot(menuSound);
        starterMenuButtons.SetActive(false);
        settingsMenuButtons.SetActive(true);
        ControlesImage.SetActive(true);
    }



    //trocar de cenas



    public void Play()
    {
        RuntimeManager.PlayOneShot(starGameSound);
        Invoke("startGame", 2f);
        playButton.interactable = false;
        setButton.interactable = false;
    }
    public void closeGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
