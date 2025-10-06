using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para encerrar jogo ou trocar de cena

public class terminarjogo : MonoBehaviour
{
    [Header("Referência para o Script do Puzzle")]
    public LeverPuzzle puzzle; // Arraste o objeto com o script LeverPuzzle no Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica se o puzzle está completo
            if (puzzle != null && puzzle.puzzlecompleto)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // Para no editor
#else
                Application.Quit(); // Fecha o jogo na build
#endif
            }
        }
    }
}
