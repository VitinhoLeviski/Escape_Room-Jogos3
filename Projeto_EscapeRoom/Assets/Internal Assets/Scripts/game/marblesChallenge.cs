using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marblesChallenge : MonoBehaviour
{
    public GameObject peca1;
    public GameObject peca2;
    public GameObject peca3;
    Interaction inter;
    bool pegou1 = false;
    bool pegou2 = false;
    bool pegou3 = false;

    // Start is called before the first frame update
    void Start()
    {
        inter = GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inter.currentInteractable == peca1)
        {
            Destroy(peca1);
            pegou1 = true;
            Debug.Log("palmas");
        }
        if (inter.currentInteractable == peca2)
        {
            Destroy(peca2);
            pegou2 = true;
        }
        if (inter.currentInteractable == peca3)
        {
            Destroy(peca3);
            pegou3 = true;
        }
    }
}
