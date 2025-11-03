using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class Lever : MonoBehaviour
{
    [Header("Configuração da Alavanca")]
    public Item item; // Referência ao item associado à alavanca
    public UnityEvent onLeverPulled; // Evento que será chamado quando a alavanca for puxada corretamente
    public UnityEvent onLeverReset; // Evento que será chamado quando a alavanca for puxada incorretamente, acionando um reset
    [HideInInspector] public bool isActive; // Flag que indica se a alavanca está ativa e pode ser puxada

    [Header("Rotação da Alavanca")]
    public Transform AlavancaDown; // Transform que representa a parte da alavanca que se move
    public float anguloRotacao = -40f; // Ângulo de rotação quando a alavanca é puxada

    private LeverPuzzle puzzleManager; // Referência ao gerenciador do puzzle da alavanca
    private Quaternion rotacaoInicial; // Armazena a rotação inicial da alavanca
    private bool inicializado = false; // Flag que indica se a alavanca foi corretamente inicializada

    private void Start()
    {
        // Obtém o componente LeverPuzzle que está no objeto pai (geralmente o controlador do puzzle)
        puzzleManager = GetComponentInParent<LeverPuzzle>();

        // Guarda a rotação inicial da alavanca, se AlavancaDown não for nulo
        if (AlavancaDown != null)
        {
            rotacaoInicial = AlavancaDown.localRotation; // Armazena a rotação original para resetar a alavanca posteriormente
            inicializado = true; // Marca a alavanca como inicializada
        }

        // Define o estado inicial da alavanca como desativado
        SetState(false);
    }

    // Função chamada quando o jogador interage com a alavanca (ex: puxando a alavanca)
    public void Interact()
    {
        if (puzzleManager != null)
        {
            // Tenta ativar a alavanca no contexto do puzzle
            puzzleManager.TryActivateLever(this);
        }
    }

    // Define o estado da alavanca (ativa ou inativa) e altera sua rotação com base nesse estado
    public void SetState(bool active)
    {
        isActive = active; // Atualiza o estado da alavanca

        // Verifica se a AlavancaDown e a rotação inicial foram corretamente configuradas
        if (AlavancaDown != null && inicializado)
        {
            if (active)
            {
                // Se a alavanca está ativa, aplica a rotação para puxá-la
                AlavancaDown.localRotation = rotacaoInicial * Quaternion.Euler(0, anguloRotacao, 0);
            }
            else
            {
                // Se a alavanca não está ativa, reseta a rotação para a inicial
                AlavancaDown.localRotation = rotacaoInicial;
            }
        }
    }
}
