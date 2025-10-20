using UnityEngine;

public class terminarjogo : MonoBehaviour
{
    [Header("Referências do Puzzle")]
    public LeverPuzzle puzzle;

    [Header("Portas")]
    public Transform TrPortaL;
    public Transform TrPortaR;

    private bool portaAberta = false;

    private void Start()
    {
        // Debug para verificar se as referências estão corretas
        if (puzzle == null)
        {
            Debug.LogError("ERRO: Referência 'puzzle' está NULL! Arraste o objeto com LeverPuzzle no Inspector.");
        }
        else
        {
            Debug.Log("Referência do puzzle OK. Puzzle completo no Start: " + puzzle.puzzlecompleto);
        }

        if (TrPortaL == null)
        {
            Debug.LogError("ERRO: TrPortaL está NULL!");
        }

        if (TrPortaR == null)
        {
            Debug.LogError("ERRO: TrPortaR está NULL!");
        }
    }

    private void Update()
    {
        // Debug contínuo para monitorar o estado do puzzle
        if (puzzle != null)
        {
            // Mostra o estado a cada 2 segundos
            if (Time.frameCount % 120 == 0) // A cada ~2 segundos (60 FPS)
            {
                Debug.Log("Status do puzzle: " + puzzle.puzzlecompleto + " | Porta aberta: " + portaAberta);
            }
        }

        if (puzzle != null && puzzle.puzzlecompleto && !portaAberta)
        {
            AbrirPorta();
        }
    }

    private void AbrirPorta()
    {
        portaAberta = true;

        Debug.Log("=== ABRINDO PORTA! ===");
        Debug.Log("Puzzle completo: " + puzzle.puzzlecompleto);

        if (TrPortaL != null)
        {
            TrPortaL.Rotate(0, 90, 0);
            Debug.Log("Porta esquerda rotacionada para: " + TrPortaL.eulerAngles);
        }
        else
        {
            Debug.LogWarning("TrPortaL é null!");
        }

        if (TrPortaR != null)
        {
            TrPortaR.Rotate(0, -90, 0);
            Debug.Log("Porta direita rotacionada para: " + TrPortaR.eulerAngles);
        }
        else
        {
            Debug.LogWarning("TrPortaR é null!");
        }
    }
}