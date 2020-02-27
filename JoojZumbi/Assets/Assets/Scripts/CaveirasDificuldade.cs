using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveirasDificuldade : MonoBehaviour
{
    void Start()
    {
        
        transform.GetChild(ControlaDificuldade.dificuldadeAtual).gameObject.SetActive(true);
    }

   public void AtivaCaveira()
    {
        transform.GetChild(ControlaDificuldade.dificuldadeAtual).gameObject.SetActive(true);
    }
}
