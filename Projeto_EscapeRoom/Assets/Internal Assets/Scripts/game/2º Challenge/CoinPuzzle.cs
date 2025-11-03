using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPuzzle : MonoBehaviour
{
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
    // Array que define a combinação correta dos slots (usando o enum CoinType)
    [SerializeField] private CoinType[] correctCombination = new CoinType[5];

    // Enum para representar os tipos de moedas ou vazio
    public enum CoinType
    {
        None,      // Slot vazio
        Odisseu,   // Moeda de Odisseu
        Ithaca,    // Moeda de Ítaca
        Troia      // Moeda de Troia
    }

    // Array que armazena a combinação atual dos slots
    private CoinType[] currentCombination = new CoinType[5];

    // Array que armazena as instâncias das moedas colocadas nos slots
    private GameObject[] placedCoins = new GameObject[5];

    
    private bool puzzleSolved = false;

    // Lista para coletar os slots selecionados pelo jogador
    private List<int> selectedSlots = new List<int>();

    // Ordem das moedas a serem colocadas nos slots selecionados
    private CoinType[] coinOrder = { CoinType.Odisseu, CoinType.Ithaca, CoinType.Troia };

    void Start()
    {
        // Inicializa os arrays de combinação atual e moedas colocadas
        for (int i = 0; i < 5; i++)
        {
            currentCombination[i] = CoinType.None;
            placedCoins[i] = null;

            // Verifica se o índice i está dentro do tamanho do array slots
            if (i >= slots.Length)
            {
                return; // Sai se não houver slots suficientes
            }
        }

        // Define a combinação correta do puzzle
        correctCombination[0] = CoinType.Ithaca;  // Slot 1 (índice 0)
        correctCombination[1] = CoinType.None;     // Slot 2 vazio
        correctCombination[2] = CoinType.Odisseu;   // Slot 3 (índice 2)
        correctCombination[3] = CoinType.None;     // Slot 4 vazio
        correctCombination[4] = CoinType.Troia;    // Slot 5 (índice 4)

        // Limpa a lista de slots selecionados
        selectedSlots.Clear();

        // Desativa as imagens de feedback no início
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
            // Verifica se a tecla numérica foi pressionada, se ainda não selecionou 3 slots e o puzzle não foi resolvido
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) && selectedSlots.Count < 3 && !puzzleSolved)
            {
                int slotNumber = i; // Número do slot (1-5)
                int slotIndex = slotNumber - 1; // Converte para índice do array (0-4)

                // Verifica se o slot já foi selecionado para evitar duplicatas
                if (selectedSlots.Contains(slotNumber))
                {

                    continue; // Pula para a próxima iteração
                }

                // Adiciona o slot à lista de selecionados
                selectedSlots.Add(slotNumber);


                // Se for o 3º input, processa a colocação das moedas e verifica a solução
                if (selectedSlots.Count == 3)
                {
                    PlaceCoinsInSelectedSlots();
                }
            }
        }
#endif
    }

    // Método para colocar as moedas nos slots selecionados
    private void PlaceCoinsInSelectedSlots()
    {
        // Limpa as colocações anteriores (mas não limpa selectedSlots ainda)
        ResetPuzzle(false);

        // Itera sobre os 3 slots selecionados
        for (int i = 0; i < 3; i++)
        {
            int slotNumber = selectedSlots[i];
            int slotIndex = slotNumber - 1;
            CoinType coinType = coinOrder[i]; // Pega o tipo de moeda na ordem: Odisseu, Ítaca, Troia

            // Verifica se o índice está dentro dos limites
            if (slotIndex < 0 || slotIndex >= slots.Length)
            {
                continue; // Pula se inválido
            }

            // Verifica se o slot existe
            if (slots[slotIndex] == null)
            {
                continue; // Pula se o slot for nulo
            }

            // Instancia a moeda no slot correspondente
            GameObject coinPrefab = GetCoinPrefab(coinType);
            if (coinPrefab != null)
            {
                placedCoins[slotIndex] = Instantiate(coinPrefab, slots[slotIndex].position, slots[slotIndex].rotation, slots[slotIndex]);
                currentCombination[slotIndex] = coinType; // Atualiza a combinação atual
            }
        }

        // Verifica se a solução está correta
        CheckSolution();

        // Limpa os slots selecionados se o puzzle não foi resolvido
        if (!puzzleSolved)
        {
            selectedSlots.Clear();
        }
    }

    private void CheckSolution()
    {
        bool isCorrect = true;
        // Compara cada slot da combinação atual com a correta
        for (int i = 0; i < 5; i++)
        {
            if (currentCombination[i] != correctCombination[i])
            {
                isCorrect = false;
                break; // Sai do loop se encontrar diferença
            }
        }

        // Se correto, marca como resolvido, mostra acerto e ativa o pergaminho
        if (isCorrect)
        {
            puzzleSolved = true;
            MostrarAcerto();
            pergaminho.SetActive(true);
        }
        else
        {
           
            MostrarErro();
        }
    }

    // Método para mostrar feedback de erro
    private void MostrarErro()
    {
        StartCoroutine(ExibirImagem(imagemErro));
    }

    // Método para mostrar feedback de acerto
    private void MostrarAcerto()
    {
        StartCoroutine(ExibirImagem(imagemAcerto));
    }

    // Coroutine para exibir a imagem
    private IEnumerator ExibirImagem(Image imagem)
    {
        if (imagem != null)
        {
            imagem.gameObject.SetActive(true); 
            yield return new WaitForSeconds(tempoExibicao); // Espera o tempo definido
            imagem.gameObject.SetActive(false); 
        }
    }

    // Método para resetar o puzzle
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

    // Método para obter o prefab da moeda baseado no tipo
    private GameObject GetCoinPrefab(CoinType type)
    {
        switch (type)
        {
            case CoinType.Odisseu: return MoedaOdisseu;
            case CoinType.Ithaca: return MoedaIthaca;
            case CoinType.Troia: return MoedaTroia;
            default: return null; // Retorna null se inválido
        }
    }

    // Método para receber a sequência de input
    public void ReceiveInputSequence(string input)
    {
        // Ignora se o puzzle já foi resolvido
        if (puzzleSolved)
        {
            return;
        }

        // Limpa a lista de slots selecionados
        selectedSlots.Clear();

        // Remove espaços e vírgulas do input para normalizar
        input = input.Replace(" ", "").Replace(",", "");

      
        if (input.Length != 3)
        {
            return;
        }

        
        for (int i = 0; i < 3; i++)
        {
            char c = input[i];
            // Verifica se é um dígito
            if (!char.IsDigit(c))
            {
                return; // Ignora se não for número
            }

            // Converte o caractere para número (1-5)
            int slotNumber = (int)char.GetNumericValue(c);

            // Verifica se está no intervalo válido
            if (slotNumber < 1 || slotNumber > 5)
            {
                return; 
            }

            // Verifica duplicatas
            if (selectedSlots.Contains(slotNumber))
            {
                return; 
            }

            // Adiciona à lista
            selectedSlots.Add(slotNumber);

           
            PlaceCoinsInSelectedSlots();
        }
    }
}
