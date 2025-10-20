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

    [Header("Rotação da Alavanca")]
    public Transform AlavancaDown;
    public float anguloRotacao = -40f;

    private LeverPuzzle puzzleManager;
    private Quaternion rotacaoInicial;
    private bool inicializado = false;

    private void Start()
    {
        Debug.Log("=== Lever Iniciado: " + gameObject.name + " ===");
        
        puzzleManager = GetComponentInParent<LeverPuzzle>();
        
        if (puzzleManager == null)
        {
            Debug.LogError("ERRO: LeverPuzzle não encontrado no parent de " + gameObject.name);
        }
        else
        {
            Debug.Log("LeverPuzzle encontrado para " + gameObject.name);
        }

        // Guarda a rotação inicial da alavanca
        if (AlavancaDown != null)
        {
            rotacaoInicial = AlavancaDown.localRotation;
            inicializado = true;
            Debug.Log("Rotação inicial salva para " + gameObject.name);
        }
        else
        {
            Debug.LogWarning("AlavancaDown é NULL em " + gameObject.name);
        }

        SetState(false);
    }

    public void Interact()
    {
        Debug.Log("*** INTERACT CHAMADO em " + gameObject.name + " ***");
        
        if (puzzleManager != null)
        {
            Debug.Log("Chamando TryActivateLever do puzzleManager");
            puzzleManager.TryActivateLever(this);
        }
        else
        {
            Debug.LogError("puzzleManager é NULL! Não pode ativar a alavanca.");
        }
    }

    public void SetState(bool active)
    {
        Debug.Log("SetState chamado em " + gameObject.name + " | active = " + active);
        isActive = active;

        if (AlavancaDown != null && inicializado)
        {
            if (active)
            {
                AlavancaDown.localRotation = rotacaoInicial * Quaternion.Euler(0, anguloRotacao, 0);
                Debug.Log("Alavanca " + gameObject.name + " ABAIXADA");
                onLeverPulled.Invoke();
            }
            else
            {
                AlavancaDown.localRotation = rotacaoInicial;
                Debug.Log("Alavanca " + gameObject.name + " RESETADA");
                onLeverReset.Invoke();
            }
        }
    }
}