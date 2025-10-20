using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberarCrsor : MonoBehaviour
{
    // Start is called before the first frame update
void Start()
{
    Cursor.lockState = CursorLockMode.None; // Libera o cursor
    Cursor.visible = true; // Torna o cursor visível
}


    // Update is called once per frame
    void Update()
    {
        
    }
}
