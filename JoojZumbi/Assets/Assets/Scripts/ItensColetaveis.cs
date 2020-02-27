using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{
    public GameObject[] PackDeItem;
    public int ScoreParaAparecerMunicao;
    private bool jaInstanciou = true;
    public Color corDoGizmo;
    public int numeroDaVez;
    public int ValorDaVida;
    public int ValorDaMunicao;
    public int MunicaoRifle;
    [SerializeField]
    private int valorComumVida = 25;
    [SerializeField]
    private int ValorComumMunicao;
    private ControlaPacotinhos[] controlaPacotinhos;
    [SerializeField]
    private int ValorPacote;
    private int numeroDeCoisasInstanciadas;
    private void Start()
    {
        numeroDaVez = Random.Range(0, PackDeItem.Length);
        ValorDaVida = valorComumVida;
        ValorDaMunicao = ValorComumMunicao;;
        controlaPacotinhos = GameObject.FindWithTag("Interface").GetComponentsInChildren<ControlaPacotinhos>();
        numeroDeCoisasInstanciadas = 0;
}
    void Update()
    {
        if(ContadorPontos.pontosScore % ScoreParaAparecerMunicao == 0 && !jaInstanciou)
        {
            jaInstanciou = true;
            if (ContadorPontos.pontosScore != 0 && numeroDeCoisasInstanciadas < PackDeItem.Length)
            {
                VerificaSeItemAtivo();
                controlaPacotinhos[ValorPacote].MostrarPacote();
            }
            else if(numeroDeCoisasInstanciadas == PackDeItem.Length)
            {
                return;
            }
        }
        else if(ContadorPontos.pontosScore % ScoreParaAparecerMunicao != 0)
        {
            jaInstanciou = false;
        }
    }


    public void AoColetarItem(int valorItem)
    {
        PackDeItem[valorItem].SetActive(false);
        numeroDeCoisasInstanciadas--;
    }

    public void InstanciaItem()
    {
        if (ContadorPontos.pontosScore % (ScoreParaAparecerMunicao * 3) == 0)
        {
            ValorDaVida = valorComumVida * 2;
            ValorDaMunicao *= 2;
            PackDeItem[numeroDaVez].transform.localScale = Vector3.Scale(new Vector3(1.5f, 1.5f, 1.5f), transform.localScale);
        }
        else
        {
            ValorDaMunicao = ValorComumMunicao + ControlaDificuldade.dificuldadeAtual;
            ValorDaVida = valorComumVida;
            PackDeItem[numeroDaVez].transform.localScale = Vector3.Scale(new Vector3(1f, 1f, 1f), transform.localScale);
        }
        MunicaoRifle = Random.Range(0, ValorDaMunicao + 1);
        PackDeItem[numeroDaVez].SetActive(true);
        numeroDeCoisasInstanciadas++;
        numeroDaVez = Random.Range(0, PackDeItem.Length);
    }
    private void VerificaSeItemAtivo()
    {
        if (PackDeItem[numeroDaVez].gameObject.activeSelf)
        {
            numeroDaVez = Random.Range(0, PackDeItem.Length);
            jaInstanciou = false;
            
        }
        else
        {
            
            InstanciaItem();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = corDoGizmo;
        int i = 0;
        foreach (GameObject gameObject in PackDeItem)
        {
            Gizmos.DrawWireSphere(PackDeItem[i].transform.position, 3);
            i++;
        }
    }
    

}
