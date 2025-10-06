using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para encerrar jogo ou trocar de cena

public class terminarjogo : MonoBehaviour
{
    [Header("Referência ao Último Enigma")]
    public LeverPuzzle finalPuzzle;        // Arraste aqui o puzzle final no Inspector

    [Header("Configuração da Porta")]
    public string playerTag = "Player";    // Tag do player
    private bool puzzleSolved = false;     // Para controlar se a porta está liberada

    private void OnEnable()
    {
        // Inscreve-se no evento do puzzle
        if (finalPuzzle != null)
            finalPuzzle.onPuzzleSolved.AddListener(UnlockDoor);
    }

    private void OnDisable()
    {
        // Remove a inscrição no evento
        if (finalPuzzle != null)
            finalPuzzle.onPuzzleSolved.RemoveListener(UnlockDoor);
    }

    /// <summary>
    /// Chamado quando o enigma final é resolvido.
    /// </summary>
    private void UnlockDoor()
    {
        puzzleSolved = true;
        Debug.Log("A porta está liberada. Vá até ela para sair do jogo.");
    }

    /// <summary>
    /// Detecta quando o jogador entra na área da porta.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (puzzleSolved && other.CompareTag(playerTag))
        {
            Debug.Log("Jogador alcançou a porta. Encerrando jogo...");
            QuitGame();
        }
    }

    /// <summary>
    /// Encerra o jogo (ou troca de cena, se preferir).
    /// </summary>
    private void QuitGame()
    {
#if UNITY_EDITOR
        // Para testes dentro do Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Para build final
        Application.Quit();
#endif
    }
}
