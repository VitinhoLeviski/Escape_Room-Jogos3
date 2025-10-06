using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LeverPuzzle : MonoBehaviour
{
    [Header("Lista de Alavancas na Ordem Correta (1 a 5)")]
    public Lever[] levers;              // Array de referências para as 5 alavancas na ordem correta

    private int currentIndex = 0;       // Qual alavanca precisa ser puxada agora

    [Header("Eventos do Puzzle")]
    public UnityEvent onPuzzleSolved;   // Evento disparado quando todas as alavancas são ativadas na ordem correta
    public UnityEvent onPuzzleReset;    // Evento disparado quando o puzzle é resetado

    /// <summary>
    /// Chamado por cada alavanca quando o jogador interage com ela.
    /// </summary>
    public void TryActivateLever(Lever lever)
    {
        if (levers[currentIndex] == lever)
        {
            // ✅ Alavanca correta
            lever.SetState(true);       // Ativa a alavanca
            currentIndex++;

            // Puzzle completo
            if (currentIndex >= levers.Length)
            {
                Debug.Log("✅ Enigma resolvido!");
                onPuzzleSolved.Invoke();
            }
        }
        else
        {
            // ❌ Ordem errada → Resetar
            Debug.Log("❌ Ordem incorreta! Resetando puzzle...");
            ResetPuzzle();
        }
    }

    /// <summary>
    /// Reseta todas as alavancas para o estado inicial.
    /// </summary>
    public void ResetPuzzle()
    {
        foreach (var lever in levers)
        {
            lever.SetState(false);      // Volta cada alavanca
        }

        currentIndex = 0;
        onPuzzleReset.Invoke();
    }
}
