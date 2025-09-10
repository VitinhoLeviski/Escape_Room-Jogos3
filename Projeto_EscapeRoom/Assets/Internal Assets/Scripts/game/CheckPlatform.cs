using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlatform : MonoBehaviour
{
    public GameObject Controle1;
    public GameObject Controle2;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Controle1.SetActive(false);
            Controle2.SetActive(false);
        }
    }

}
