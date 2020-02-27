using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaArma : MonoBehaviour
{
    public int NumeroArma = 0;
    private ControlaInterface controlaInterface;
    private ControlaArma controlaArma;
    private void Awake()
    {
        controlaInterface =
            GameObject.FindWithTag("Interface").GetComponent<ControlaInterface>();
        
    }
    void Start()
    {
        TrocarArma();
    }
    void Update()
    {
        int ArmaEscolhida = NumeroArma;
        if (!ControlaPause.JogoPausado)
        {
            if (Input.GetKeyDown("e"))
            {

                if (NumeroArma >= transform.childCount - 1)
                {
                    NumeroArma = 0;


                }
                else
                {
                    NumeroArma++;
                }

            }
            if (ArmaEscolhida != NumeroArma)
            {
                TrocarArma();
            }
        }
    }
    void TrocarArma()
    {
        int i = 0;
        foreach (Transform arma in transform)
        {
            if (i == NumeroArma)
            {
                arma.gameObject.SetActive(true);
                
            }
            else
            {
                arma.gameObject.SetActive(false);
            }
            i++;
        }
        controlaArma = GetComponentInChildren<ControlaArma>();
        controlaArma.recarregando = false;
        controlaArma.cronometroRecarregamento =0;
        controlaInterface.AtualizarArma();
        
    }
}

