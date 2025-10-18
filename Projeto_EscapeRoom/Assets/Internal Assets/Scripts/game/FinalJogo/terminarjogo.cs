using UnityEngine;

public class terminarjogo : MonoBehaviour
{
    [Header("Referências do Puzzle")]
    public LeverPuzzle puzzle;
    public marblesChallenge marbles;

    [Header("Portas")]
    public Transform TrPortaL;
    public Transform TrPortaR;

    private bool portaAberta = false;

    private void Update()
    {

        if (puzzle != null && puzzle.puzzlecompleto && marbles != null && marbles.desafioFeito && !portaAberta)
        {
            AbrirPorta();
        }
    }



    private void AbrirPorta()
    {
        portaAberta = true;

        TrPortaL.Rotate(0, 90, 0);
        TrPortaR.Rotate(0, -90, 0);


    }

}