using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusZumbiEspecial : MonoBehaviour
{
    public float VidaInicial;
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

    public void EscolheTamanho()
    {
        Tamanho = Random.Range(TamanhoMin[GiraBoss.ControlaDificuldadeBoss], TamanhoMax[GiraBoss.ControlaDificuldadeBoss]);
        transform.localScale = Vector3.Scale(new Vector3(Tamanho, Tamanho, Tamanho), transform.localScale);
        Velocidade = 
            ((Tamanho - TamanhoMax[GiraBoss.ControlaDificuldadeBoss]) / (TamanhoMin[GiraBoss.ControlaDificuldadeBoss]
                - TamanhoMax[GiraBoss.ControlaDificuldadeBoss]))*((VelocidadeMax[GiraBoss.ControlaDificuldadeBoss] - VelocidadeMin[GiraBoss.ControlaDificuldadeBoss])) 
                    + VelocidadeMin[GiraBoss.ControlaDificuldadeBoss];
        DanoZumbi =
            ((Tamanho - TamanhoMin[GiraBoss.ControlaDificuldadeBoss]) / (TamanhoMax[GiraBoss.ControlaDificuldadeBoss]
                - TamanhoMin[GiraBoss.ControlaDificuldadeBoss])) * ((DanoZumbiMax[GiraBoss.ControlaDificuldadeBoss] - DanoZumbiMin[GiraBoss.ControlaDificuldadeBoss]))
                    + DanoZumbiMin[GiraBoss.ControlaDificuldadeBoss];
        VidaZumbi =(int)Mathf.Round((Tamanho
            - TamanhoMin[GiraBoss.ControlaDificuldadeBoss]) / (TamanhoMax[GiraBoss.ControlaDificuldadeBoss]
                - TamanhoMin[GiraBoss.ControlaDificuldadeBoss]) * (VidaZumbiMax[GiraBoss.ControlaDificuldadeBoss] - VidaZumbiMin[GiraBoss.ControlaDificuldadeBoss])
                    + VidaZumbiMin[GiraBoss.ControlaDificuldadeBoss]);
    }
}
