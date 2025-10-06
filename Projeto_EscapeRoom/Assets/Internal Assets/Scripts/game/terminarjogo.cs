using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Para encerrar jogo ou trocar de cena

public class terminarjogo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Para no editor
#else
            Application.Quit(); // Fecha o jogo na build
            #endif
                
        }
    }
}
