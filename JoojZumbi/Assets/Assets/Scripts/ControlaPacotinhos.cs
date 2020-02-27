using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaPacotinhos : MonoBehaviour
{
    [SerializeField]
    private Animator animacaoPacote;
   
    public void MostrarPacote()
    {
        animacaoPacote.Play("AnimacaoPacote");
    }
}
