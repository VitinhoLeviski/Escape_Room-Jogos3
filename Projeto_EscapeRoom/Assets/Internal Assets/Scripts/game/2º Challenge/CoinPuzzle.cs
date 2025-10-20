using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinPuzzle : MonoBehaviour
{
    public TMP_Text txtObjetivo;
    public GameObject pergaminho;

    [Header("Coin Prefabs")]
    public GameObject MoedaOdisseu;
    public GameObject MoedaIthaca;
    public GameObject MoedaTroia;


    [Header("Slots")]
    public Transform[] slots;

    [Header("Feedback Visual")]
    public Image imagemErro;
    public Image imagemAcerto;
    public float tempoExibicao = 2f;

    [Header("Puzzle Solution")]
    [SerializeField] private CoinType[] correctCombination = new CoinType[5]; 
    public enum CoinType
    {
        None,      // Slot vazio
        Odisseu,   // Moeda de Odisseu
        Ithaca,    // Moeda de Ítaca
        Troia      // Moeda de Troia
    }


    private CoinType[] currentCombination = new CoinType[5];


    private GameObject[] placedCoins = new GameObject[5];


    private bool puzzleSolved = false;

    // Lista para coletar os slots digitados
    private List<int> selectedSlots = new List<int>();

    private CoinType[] coinOrder = { CoinType.Odisseu, CoinType.Ithaca, CoinType.Troia };

    void Start()
    {
  
        for (int i = 0; i < 5; i++)
        {
            currentCombination[i] = CoinType.None;
            placedCoins[i] = null;

   
            if (i >= slots.Length)
            {
                return;
            }
        }


        correctCombination[0] = CoinType.Ithaca;  // Slot 1 (índice 0)
        correctCombination[1] = CoinType.None;     // Slot 2 vazio
        correctCombination[2] = CoinType.Odisseu;   // Slot 3 (índice 2)
        correctCombination[3] = CoinType.None;     // Slot 4 vazio
        correctCombination[4] = CoinType.Troia;    // Slot 5 (índice 4)

        selectedSlots.Clear();

        if (imagemErro != null)
        {
            imagemErro.gameObject.SetActive(false);
        }
        if (imagemAcerto != null)
        {
            imagemAcerto.gameObject.SetActive(false);
        }

    }

    void Update()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
        for (int i = 1; i <= 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) && selectedSlots.Count < 3 && !puzzleSolved)
            {
                int slotNumber = i; // 1-5
                int slotIndex = slotNumber - 1; // Converter para índice 0-4

                // Verifica se o slot já foi selecionado (evita duplicatas)
                if (selectedSlots.Contains(slotNumber))
                {
                    Debug.LogWarning("Slot " + slotNumber + " já selecionado! Escolha outro.");
                    continue;
                }

                selectedSlots.Add(slotNumber);
                Debug.Log("Slot " + slotNumber + " selecionado. Slots escolhidos até agora: " + string.Join(", ", selectedSlots));

                // Se for o 3º input, processe a colocação e verifique
                if (selectedSlots.Count == 3)
                {
                    PlaceCoinsInSelectedSlots();
                }
            }
        }


        #endif
    }


    private void PlaceCoinsInSelectedSlots()
    {
        // Limpa colocações anteriores
        ResetPuzzle(false); 

        for (int i = 0; i < 3; i++)
        {
            int slotNumber = selectedSlots[i];
            int slotIndex = slotNumber - 1;
            CoinType coinType = coinOrder[i];

            if (slotIndex < 0 || slotIndex >= slots.Length)
            {
                continue;
            }

            // Verifica se o slot existe
            if (slots[slotIndex] == null)
            {
                continue;
            }


            GameObject coinPrefab = GetCoinPrefab(coinType);
            if (coinPrefab != null)
            {
                placedCoins[slotIndex] = Instantiate(coinPrefab, slots[slotIndex].position, slots[slotIndex].rotation, slots[slotIndex]);
                currentCombination[slotIndex] = coinType;
            }
            else
            {

            }
        }

  
        CheckSolution();

        // Limpa os selectedSlots
        if (!puzzleSolved)
        {
            selectedSlots.Clear();

        }
    }


    private void CheckSolution()
    {
        bool isCorrect = true;
        for (int i = 0; i < 5; i++)
        {
            if (currentCombination[i] != correctCombination[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            puzzleSolved = true;
            MostrarAcerto();
            pergaminho.SetActive(true);
            txtObjetivo.text = "O que essas alavancas fazem, deve ter algo explicando-as";
        }
        else
        {
            MostrarErro();
        }
    }


    private void MostrarErro()
    {
        StartCoroutine(ExibirImagem(imagemErro));
    }

    private void MostrarAcerto()
    {
        StartCoroutine(ExibirImagem(imagemAcerto));
    }


    private IEnumerator ExibirImagem(Image imagem)
    {
        if (imagem != null)
        {
            imagem.gameObject.SetActive(true);
            yield return new WaitForSeconds(tempoExibicao);
            imagem.gameObject.SetActive(false);
        }
    }

    // Reseta o puzzle
    public void ResetPuzzle(bool clearSelected = true)
    {
        for (int i = 0; i < 5; i++)
        {
            if (placedCoins[i] != null)
            {
                Destroy(placedCoins[i]);
                placedCoins[i] = null;
            }
            currentCombination[i] = CoinType.None;
        }

        if (clearSelected)
        {
            selectedSlots.Clear();
        }

        puzzleSolved = false;

    }


    private GameObject GetCoinPrefab(CoinType type)
    {
        switch (type)
        {
            case CoinType.Odisseu: return MoedaOdisseu;
            case CoinType.Ithaca: return MoedaIthaca;
            case CoinType.Troia: return MoedaTroia;
            default: return null;
        }
    }

public void ReceiveInputSequence(string input)
{
    if (puzzleSolved)
    {
        return;
    }

    selectedSlots.Clear();

    input = input.Replace(" ", " ").Replace(",","");

    if(input.Length != 3)
    {
        return;
    }

    for(int i = 0;i < 3; i++)
    {
        char c = input[i];
        if(!char.IsDigit(c))
        {
            return;
        }
    

    int slotNumber = (int)char.GetNumericValue(c);

    if(slotNumber < 1 || slotNumber > 5)
    {
        return;
    }
    
    if(selectedSlots.Contains(slotNumber))
    {
        return;
    }

    selectedSlots.Add(slotNumber);

    PlaceCoinsInSelectedSlots();
}

}
}