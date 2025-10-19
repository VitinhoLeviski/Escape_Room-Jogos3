using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImagemFinal : MonoBehaviour
{
    [Header("Configurações")]
    public Image imagemVitoria;
    public float tempoExibicao = 2f;

    private void Start()
    {

        if (imagemVitoria != null)
        {
            imagemVitoria.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ExibirTelaVitoria());
        }
    }

    private IEnumerator ExibirTelaVitoria()
    {


        yield return new WaitForSeconds(tempoExibicao);

        SceneManager.LoadScene("Victory");
    }
}