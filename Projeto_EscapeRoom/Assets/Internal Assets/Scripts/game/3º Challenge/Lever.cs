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
        puzzleManager = GetComponentInParent<LeverPuzzle>();

        // Guarda a rotação inicial da alavanca
        if (AlavancaDown != null)
        {
            rotacaoInicial = AlavancaDown.localRotation;
            inicializado = true;
        }

        SetState(false);
    }

    public void Interact()
    {
        if (puzzleManager != null)
        {
            puzzleManager.TryActivateLever(this);
        }
    }

    public void SetState(bool active)
    {
        isActive = active;

        if (AlavancaDown != null && inicializado)
        {
            if (active)
            {

                AlavancaDown.localRotation = rotacaoInicial * Quaternion.Euler(0, anguloRotacao, 0);
            }
            else
            {

                AlavancaDown.localRotation = rotacaoInicial;
            }
        }
    }
}