using UnityEngine;

public class terminarjogo : MonoBehaviour
{
    [Header("Referências do Puzzle")]
    public LeverPuzzle puzzle;

    [Header("Portas")]
    public Transform TrPortaL;
    public Transform TrPortaR;

    private bool portaAberta = false;
    private bool puzzleEstavaConcluido = false;

    private void Start()
    {
 
        if (puzzle != null)
        {
            puzzleEstavaConcluido = puzzle.puzzlecompleto;
        }
    }

    private void Update()
    {
        if (puzzle != null && puzzle.puzzlecompleto && !puzzleEstavaConcluido && !portaAberta)
        {
            AbrirPorta();
        }
    }

    private void AbrirPorta()
    {
        portaAberta = true;
        puzzleEstavaConcluido = true;

        TrPortaL.Rotate(0, 90, 0);
        TrPortaR.Rotate(0, -90, 0);

    }
}