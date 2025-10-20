using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;

public class LeverPuzzle : MonoBehaviour
{
    [Header("Lista de Alavancas na Ordem Correta (1 a 5)")]
    public Lever[] levers;              // Array de referências para as 5 alavancas na ordem correta

    private int currentIndex = 0;       // Qual alavanca precisa ser puxada agora

    [Header("Eventos do Puzzle")]
    public UnityEvent onPuzzleSolved;   // Evento disparado quando todas as alavancas são ativadas na ordem correta
    public UnityEvent onPuzzleReset;    // Evento disparado quando o puzzle é resetado

    [Header("Sons do Enigma (FMOD)")]
    [Tooltip("Evento FMOD para o som de alavanca correta")]
    public EventReference enigmaCertoEvent;

    [Tooltip("Evento FMOD para o som de alavanca errada")]
    public EventReference enigmaErradoEvent;

    [Header("Estado do Puzzle")]
    public bool puzzlecompleto = false;

    private void Start()
    {
        Debug.Log("=== LeverPuzzle Iniciado ===");
        Debug.Log("Total de alavancas: " + levers.Length);
        Debug.Log("Puzzle completo no Start: " + puzzlecompleto);
    }

    /// <summary>
    /// Chamado por cada alavanca quando o jogador interage com ela.
    /// </summary>
    public void TryActivateLever(Lever lever)
    {
        Debug.Log("=== TryActivateLever chamado ===");
        Debug.Log("Alavanca recebida: " + lever.name);
        Debug.Log("Index atual: " + currentIndex);
        Debug.Log("Alavanca esperada: " + (currentIndex < levers.Length ? levers[currentIndex].name : "NENHUMA"));

        if (levers[currentIndex] == lever)
        {
            // Alavanca correta
            Debug.Log("✓ ALAVANCA CORRETA! Progresso: " + (currentIndex + 1) + "/" + levers.Length);
            lever.SetState(true);
            currentIndex++;

            // Toca som de acerto (FMOD)
            if (!enigmaCertoEvent.IsNull)
                RuntimeManager.PlayOneShot(enigmaCertoEvent, transform.position);

            // Puzzle completo
            if (currentIndex >= levers.Length)
            {
                puzzlecompleto = true;
                Debug.Log("★★★ ENIGMA RESOLVIDO! puzzlecompleto = " + puzzlecompleto + " ★★★");
                onPuzzleSolved.Invoke();
            }
        }
        else
        {
            // Alavanca errada → Resetar
            Debug.Log("✗ ORDEM INCORRETA! Resetando puzzle...");

            if (!enigmaErradoEvent.IsNull)
                RuntimeManager.PlayOneShot(enigmaErradoEvent, transform.position);

            ResetPuzzle();
        }
    }

    /// <summary>
    /// Reseta o puzzle
    /// </summary>
    public void ResetPuzzle()
    {
        Debug.Log("=== RESETANDO PUZZLE ===");
        foreach (var lever in levers)
        {
            lever.SetState(false);
        }

        currentIndex = 0;
        puzzlecompleto = false;
        Debug.Log("Puzzle resetado. puzzlecompleto = " + puzzlecompleto);
        onPuzzleReset.Invoke();
    }
}