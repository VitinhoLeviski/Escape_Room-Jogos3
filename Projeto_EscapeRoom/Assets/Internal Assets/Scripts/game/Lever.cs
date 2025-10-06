using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;        // Necessário para acessar EventReference e RuntimeManager

public class Lever : MonoBehaviour
{
    [Header("Configuração da Alavanca")]
    public Item item;                         // Referência ao Item usado pelo sistema de interação
    public UnityEvent onLeverPulled;          // Evento para animações / sons
    public UnityEvent onLeverReset;           // Evento para resetar a animação

    [HideInInspector] public bool isActive;   // Estado da alavanca (puxada ou não)

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
            onLeverPulled.Invoke();

            // Toca som de puxar (FMOD)
            if (leverPulledEvent.IsNull == false)
                RuntimeManager.PlayOneShot(leverPulledEvent, transform.position);
        }
        else
        {
            // Dispara animação e eventos de resetar
            onLeverReset.Invoke();

            // Toca som de resetar (FMOD)
            if (leverResetEvent.IsNull == false)
                RuntimeManager.PlayOneShot(leverResetEvent, transform.position);
        }
    }
}
