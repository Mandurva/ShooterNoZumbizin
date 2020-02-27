using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPersonagens : MonoBehaviour
{
    public int VidaInicial = 100;
    [HideInInspector]
    public float Vida;
    [HideInInspector]
    public float Escudo;
    public float Velocidade;
    public float tempoProximaPosicao;
    public float DanoZumbi;
    public float Tamanho;
    public int VidaZumbi;
    public int[] VidaZumbiMin;
    public int[] VidaZumbiMax;
    public float[] DanoZumbiMin;
    public float[] DanoZumbiMax;
    public float[] VelocidadeMin;
    public float[] VelocidadeMax;
    public float[] TamanhoMin;
    public float[] TamanhoMax;

    private void Awake()
    {
        Vida = VidaInicial;
    }
    public void AumentaVida(int vidaRecebida)
    {
        if (Vida < VidaInicial*2)
        {
            Vida += vidaRecebida;
            Escudo = Vida - VidaInicial;
            if(Escudo < 0)
            {
                Escudo = 0;
            }
            if (Vida > VidaInicial*2)
            {
                Vida = VidaInicial*2;
            }
        }

    }
    public void EscolheTamanho()
    {
        Tamanho = Random.Range(TamanhoMin[ControlaDificuldade.dificuldadeAtual], TamanhoMax[ControlaDificuldade.dificuldadeAtual]);
        transform.localScale = Vector3.Scale(new Vector3(Tamanho, Tamanho, Tamanho), transform.localScale);
        Velocidade = 
            ((Tamanho - TamanhoMax[ControlaDificuldade.dificuldadeAtual]) / (TamanhoMin[ControlaDificuldade.dificuldadeAtual]
                - TamanhoMax[ControlaDificuldade.dificuldadeAtual]))*((VelocidadeMax[ControlaDificuldade.dificuldadeAtual] - VelocidadeMin[ControlaDificuldade.dificuldadeAtual])) 
                    + VelocidadeMin[ControlaDificuldade.dificuldadeAtual];
        DanoZumbi =
            ((Tamanho - TamanhoMin[ControlaDificuldade.dificuldadeAtual]) / (TamanhoMax[ControlaDificuldade.dificuldadeAtual]
                - TamanhoMin[ControlaDificuldade.dificuldadeAtual])) * ((DanoZumbiMax[ControlaDificuldade.dificuldadeAtual] - DanoZumbiMin[ControlaDificuldade.dificuldadeAtual]))
                    + DanoZumbiMin[ControlaDificuldade.dificuldadeAtual];
        VidaZumbi =(int)Mathf.Round((Tamanho
            - TamanhoMin[ControlaDificuldade.dificuldadeAtual]) / (TamanhoMax[ControlaDificuldade.dificuldadeAtual]
                - TamanhoMin[ControlaDificuldade.dificuldadeAtual]) * (VidaZumbiMax[ControlaDificuldade.dificuldadeAtual] - VidaZumbiMin[ControlaDificuldade.dificuldadeAtual])
                    + VidaZumbiMin[ControlaDificuldade.dificuldadeAtual]);
    }
}
