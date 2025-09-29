using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPuzzle : MonoBehaviour
{
    [Header("Coin Prefabs")]
    public GameObject MoedaOdisseu;
    public GameObject MoedaIthaca;
    public GameObject MoedaTroia;

    [Header("Slots")]
    public Transform[] slots; // Array de 5 slots (Transforms vazios ou GameObjects com colliders). Atribua no Inspector!

    [Header("Puzzle Solution")]
    [SerializeField] private CoinType[] correctCombination = new CoinType[5]; // Defina a combinação correta aqui (ex: Ithaca no slot 0, None no 1, Troia no 2, Odisseu no 3, None no 4)

    // Enum para tipos de moedas (facilita identificação)
    public enum CoinType
    {
        None,      // Slot vazio
        Odisseu,   // Moeda de Odisseu
        Ithaca,    // Moeda de Ítaca
        Troia      // Moeda de Troia
    }

    // Estados dos slots: qual moeda está em cada um
    private CoinType[] currentCombination = new CoinType[5];

    // Referências para as moedas instanciadas (para remoção ou verificação)
    private GameObject[] placedCoins = new GameObject[5];

    // Flag para puzzle resolvido
    private bool puzzleSolved = false;

    // Lista para coletar os slots digitados pelo player (ex: 1,3,5)
    private List<int> selectedSlots = new List<int>();

    // Ordem fixa das moedas: 1º digitado = Odisseu, 2º = Ithaca, 3º = Troia
    private CoinType[] coinOrder = { CoinType.Odisseu, CoinType.Ithaca, CoinType.Troia };

    void Start()
    {
        // Inicialize os slots como vazios
        for (int i = 0; i < 5; i++)
        {
            currentCombination[i] = CoinType.None;
            placedCoins[i] = null;

            // Certifique-se de que há exatamente 5 slots
            if (i >= slots.Length)
            {
                Debug.LogError("CoinPuzzle: Configure exatamente 5 slots no array 'slots' no Inspector!");
                return;
            }
        }

        // Isso significa: correctCombination[0] = Odisseu, [2] = Ithaca, [4] = Troia (slots 1,3,5 indexados como 0,2,4)
        correctCombination[0] = CoinType.Ithaca;  // Slot 1 (índice 0)
        correctCombination[1] = CoinType.None;     // Slot 2 vazio
        correctCombination[2] = CoinType.Odisseu;   // Slot 3 (índice 2)
        correctCombination[3] = CoinType.None;     // Slot 4 vazio
        correctCombination[4] = CoinType.Troia;    // Slot 5 (índice 4)

        selectedSlots.Clear();
        Debug.Log("CoinPuzzle iniciado. Digite 3 números (1-5) para colocar as moedas na ordem: Odisseu > Ithaca > Troia. Exemplo: 1,3,5");
        Debug.Log("Pressione R para resetar. Pressione P para ver estado atual.");
    }

    void Update()
    {
        // Captura input de teclas numéricas (1-5)
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

        // Opcional: Pressione R para resetar o puzzle (útil para testes)
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPuzzle();
        }

        // Opcional: Pressione Enter para confirmar manualmente (se quiser, mas auto-confirma após 3)
        if (Input.GetKeyDown(KeyCode.Return) && selectedSlots.Count == 3 && !puzzleSolved)
        {
            PlaceCoinsInSelectedSlots();
        }
    }


    private void PlaceCoinsInSelectedSlots()
    {
        // Limpa colocações anteriores (se houver)
        ResetPuzzle(false); // Reset sem limpar selectedSlots

        for (int i = 0; i < 3; i++)
        {
            int slotNumber = selectedSlots[i];
            int slotIndex = slotNumber - 1;
            CoinType coinType = coinOrder[i]; // Odisseu (0), Ithaca (1), Troia (2)

            // Coloca a moeda
            GameObject coinPrefab = GetCoinPrefab(coinType);
            if (coinPrefab != null)
            {
                placedCoins[slotIndex] = Instantiate(coinPrefab, slots[slotIndex].position, slots[slotIndex].rotation, slots[slotIndex]);
                currentCombination[slotIndex] = coinType;
                Debug.Log("Moeda " + coinType + " colocada no slot " + slotNumber + " (índice " + slotIndex + ")");
            }
            else
            {
                Debug.LogError("Prefab da moeda " + coinType + " não encontrado!");
            }
        }

        // Verifica a solução imediatamente
        CheckSolution();

        // Limpa os selectedSlots para próxima tentativa (se errado)
        if (!puzzleSolved)
        {
            selectedSlots.Clear();
            Debug.Log("Tentativa processada. Tente novamente com outra combinação.");
        }
    }

    // Verifica se a combinação atual é a correta
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
            Debug.Log("Enigma resolvido! A ordem digitada está correta: " + string.Join(", ", selectedSlots));
        }
        else
        {
            Debug.Log("Combinação incorreta. A ordem das moedas não matches a solução. Tente novamente.");
        }
    }

    // Reseta o puzzle (limpa moedas e estados)
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
        Debug.Log("Puzzle resetado. Digite uma nova combinação de 3 slots (1-5).");
    }

    // Retorna o prefab baseado no tipo de moeda
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

}
