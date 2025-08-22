using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject PNG_starterMenu;
    [SerializeField] private GameObject starterMenuButtons;
    [SerializeField] private GameObject setMenuButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play() {
        SceneManager.LoadScene("CutScene");
    }

    public void SettingsMenu () {
        starterMenuButtons.SetActive(false);
        setMenuButtons.SetActive(true);
    }

    public void backStarterMenu()
    {
        starterMenuButtons.SetActive(true);
        setMenuButtons.SetActive(false);
    }
    public void closeGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
