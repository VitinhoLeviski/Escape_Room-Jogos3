using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    Interaction inter;
    
    void Start()
    {
        inter = GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inter != null && inter.currentInteractable.item != null)
            {
            if (inter.currentInteractable.item.pushable)
            {
                inter.currentInteractable.transform.position = new Vector3(-1747.09998f, -8.57247162f, -172.422485f);
            }
        }
    }
}
