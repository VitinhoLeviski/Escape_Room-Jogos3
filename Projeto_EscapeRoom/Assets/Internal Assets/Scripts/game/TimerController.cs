using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    [Header("Configurações do Timer")]
    public float tempoInicial = 300f; // 5 minutos em segundos
    private float tempoRestante;

    [Header("Referência UI")]
    public TextMeshProUGUI timerText;

    private bool timerAtivo = true;

    void Start()
    {
        tempoRestante = tempoInicial;
        AtualizarTexto();
    }

    void Update()
    {
        if (!timerAtivo) return;

        tempoRestante -= Time.deltaTime;

        if (tempoRestante <= 0f)
        {
            tempoRestante = 0f;
            timerAtivo = false;
            AtualizarTexto();
            CarregarCenaGameOver();
        }
        else
        {
            AtualizarTexto();
        }
    }

    void AtualizarTexto()
    {
        int minutos = Mathf.FloorToInt(tempoRestante / 60);
        int segundos = Mathf.FloorToInt(tempoRestante % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void CarregarCenaGameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
