using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class marblesChallenge : MonoBehaviour
{
    public GameObject peca1;
    public GameObject peca2;
    public GameObject peca3;
    public GameObject ativapeca1;
    public GameObject ativapeca2;
    public GameObject ativapeca3;
    public GameObject quadro1;
    public GameObject quadro2;
    public GameObject quadro3;

    public GameObject LivroMoeda;

    Interaction inter;
    bool pegou1 = false;
    bool pegou2 = false;
    bool pegou3 = false;
    bool parte1feita = false;
    bool parte2feita = false;
    bool parte3feita = false;
    public bool desafioFeito = false;
    


    // Start is called before the first frame update
    void Start()
    {
        inter = GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        destroyMarbles();
        putMarbles();
        desafioPronto();
    }

    private void destroyMarbles()
    {
        if (inter != null && inter.currentInteractable != null && inter.currentInteractable.item != null)
        {
            if (inter.currentInteractable.item.destroy1)
            {
                peca1.SetActive(false);
                pegou1 = true;
            }

            if (inter.currentInteractable.item.destroy2)
            {
                peca2.SetActive(false);
                pegou2 = true;
            }

            if (inter.currentInteractable.item.destroy3)
            {
                peca3.SetActive(false);
                pegou3 = true;
            }
        }
    }

    private void putMarbles()
    {
        if (pegou1 && inter.currentInteractable.item.picture1)
        {
            inter.currentInteractable.item.text = "O mar";
            ativapeca1.SetActive(true);
            parte1feita = true;
            pegou1 = false;
            inter.FinishView();
        }

        if (pegou2 && inter.currentInteractable.item.picture2)
        {
            ativapeca2.SetActive(true);
            parte2feita = true;
            pegou2 = false;
            inter.FinishView();
        }

        if (pegou3 && inter.currentInteractable.item.picture3)
        {
            ativapeca3.SetActive(true);
            parte3feita = true;
            pegou3 = false;
            inter.FinishView();
        }
    }

    void desafioPronto()
    {
        if (parte1feita && parte2feita && parte3feita)
        {
            desafioFeito = true;
            LivroMoeda.SetActive(true);
        }
    }
}
