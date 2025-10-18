using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImagemInicio : MonoBehaviour
{
    [Header("Configurações")]
    public Image imagemInicio;
    public float tempoExibicao = 2f;

    private void Start()
    {
        StartCoroutine(ExibirEDesativar());
    }

    private IEnumerator ExibirEDesativar()
    {
  
        if (imagemInicio != null)
        {
            imagemInicio.gameObject.SetActive(true);
        }


        yield return new WaitForSeconds(tempoExibicao);

        if (imagemInicio != null)
        {
            imagemInicio.gameObject.SetActive(false);
        }
    }
}