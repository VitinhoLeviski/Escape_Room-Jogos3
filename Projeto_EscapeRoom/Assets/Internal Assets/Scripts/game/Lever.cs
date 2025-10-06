using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class Lever : MonoBehaviour
{
    [Header("Configuração da Alavanca")]
    public Item item;                         
    public UnityEvent onLeverPulled;          
    public UnityEvent onLeverReset;           

    [HideInInspector] public bool isActive;   

    private LeverPuzzle puzzleManager;

    [Header("Sons da Alavanca (FMOD)")]
    [Tooltip("Som tocado ao puxar a alavanca")]
    public EventReference leverPulledEvent;

    [Tooltip("Som tocado ao resetar a alavanca")]
    public EventReference leverResetEvent;

    private void Start()
    {
        puzzleManager = GetComponentInParent<LeverPuzzle>();
        SetState(false); // Começa desativada
    }

    /// <summary>
    /// Chamado quando o jogador interage com a alavanca.
    /// </summary>
    public void Interact()
    {
        if (puzzleManager != null)
        {   
            puzzleManager.TryActivateLever(this);
        }
        else
        {
            // Se não houver puzzleManager, alterna manualmente
            SetState(!isActive);
        }
    }

    /// <summary>
    /// Define o estado da alavanca (ativada ou resetada).
    /// </summary>
    public void SetState(bool active)
    {
        isActive = active;

        if (active)
        {
            // Dispara animação e eventos de puxar
            onLeverPulled?.Invoke();

            // Toca som de puxar (FMOD)
            if (!string.IsNullOrEmpty(leverPulledEvent.Path))
                RuntimeManager.PlayOneShot(leverPulledEvent, transform.position);
            else
                Debug.LogWarning("Lever: Evento de som de puxar não atribuído!");
        }
        else
        {
            // Dispara animação e eventos de resetar
            onLeverReset?.Invoke();

            // Toca som de resetar (FMOD)
            if (!string.IsNullOrEmpty(leverResetEvent.Path))
                RuntimeManager.PlayOneShot(leverResetEvent, transform.position);
            else
                Debug.LogWarning("Lever: Evento de som de resetar não atribuído!");
        }
    }
}
