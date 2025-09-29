using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Variável para controlar o estado do jogo
    private bool isPaused = false;
    public GameObject fundinho;

    void Update()
    {
        // Verifica se o jogador apertou a tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alterna entre pausado e rodando
            if (isPaused)
                ResumeGame();
            else
                Pause();
        }
    }

       
    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            Pause();
    }

    // Função para pausar o jogo
    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        fundinho.SetActive(true);
    }

    // Função para retomar o jogo
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        fundinho.SetActive(false);
    }
}

