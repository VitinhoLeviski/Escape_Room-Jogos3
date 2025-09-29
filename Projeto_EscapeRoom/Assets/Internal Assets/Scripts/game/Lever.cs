using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [Header("Configuração da Alavanca")]
    public Item item;                       // Referência ao Item usado pelo sistema de interação
    public UnityEvent onLeverPulled;        // Evento para animações / sons
    public UnityEvent onLeverReset;         // Evento para resetar a animação

    [HideInInspector] public bool isActive; // Estado da alavanca (puxada ou não)

    private LeverPuzzle puzzleManager;
    

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
            onLeverPulled.Invoke();
        }
        else
        {
            onLeverReset.Invoke();
        }
    }
}
