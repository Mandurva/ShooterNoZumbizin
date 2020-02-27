using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlaDificuldade : MonoBehaviour
{
    public float[] TempoGeracaoZumbis;
    public int[] LimiteZumbis;
    public int[] DanoZumbiMaximo;
    public int[] DanoZumbiMinimo;
    public int[] VidaZumbi;
    public float[] VelocidadeZumbi;
    public float[] DistanciaPerseguirZumbi;
    private int i = 0;
    public ItensColetaveis controlaColetaveis;
    [SerializeField]
    private int[] scoresDificuldade; 
    private GeradorDeZumbis[] geradorDeZumbis;
    public CaveirasDificuldade caveirinhas;
    private GameObject[] ZumbisVivos;
    public ControlaSpawnDoBoss spawnBoss;
    public GameObject interfaceScore;
    public static int dificuldadeAtual = 0;
    private bool UmBossTaBomNe;

    public Image caveiraAlteraDificuldade;
    private float cronometroImagem = 3f;
    public Color32[] CoresCaveiras;
    public AudioSource AudioJogo;
    void Awake()
    {
        dificuldadeAtual = 0;
        geradorDeZumbis = GetComponentsInChildren<GeradorDeZumbis>();
        AtualizaDificuldade();
    }

    
    void Update()
    {
        if (caveiraAlteraDificuldade.IsActive())
        {
            cronometroImagem -= Time.deltaTime;
            if (cronometroImagem <= 0)
            {
                caveiraAlteraDificuldade.gameObject.SetActive(false);
                cronometroImagem = 3f;
            }
        }

        if (dificuldadeAtual < scoresDificuldade.Length)
        {
            if (ContadorPontos.pontosScore >= scoresDificuldade[dificuldadeAtual])
            {
                if (dificuldadeAtual == i)
                {
                    dificuldadeAtual++;
                    if (dificuldadeAtual < 5)
                    {
                        AtualizaDificuldade();
                        AvisoNovaDificuldade();
                    }
                    i++;

                }
            }
        }
        else 
        {
            if (!UmBossTaBomNe)
            {
                BossFight();
                UmBossTaBomNe = true;
                AvisoNovaDificuldade();
            }
        }
       
    }
  
    void AtualizaDificuldade()
    {
        foreach (GeradorDeZumbis geradorDeZumbis in geradorDeZumbis)
        {
            geradorDeZumbis.TempoGerarZumbis = TempoGeracaoZumbis[dificuldadeAtual];
            geradorDeZumbis.LimiteZumbis = LimiteZumbis[dificuldadeAtual];
            geradorDeZumbis.VidaZumbi = VidaZumbi[dificuldadeAtual];
            geradorDeZumbis.DistanciaPerseguirZumbi = DistanciaPerseguirZumbi[dificuldadeAtual];
        }
        controlaColetaveis.ValorDaMunicao++;
        caveirinhas.AtivaCaveira();
    }
    public void BossFight()
    {
        LimparZumbis();
        spawnBoss.ComecarGeracaoDoBoss();
        interfaceScore.SetActive(false);
        AudioJogo.GetComponent<ControlaAudio>().MusicaBoss();
    }
    public void LimparZumbis()
    {
        ZumbisVivos = GameObject.FindGameObjectsWithTag("Inimigo");
        int i = 0;
        foreach (GameObject zumbi in ZumbisVivos)
        {
            Destroy(ZumbisVivos[i].gameObject) ;
            i++;
        }
        ContadorPontos.pontosScore -= i;

        i = 0;
        foreach(Transform gerador in transform)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
    }
    private void AvisoNovaDificuldade()
    {
        caveiraAlteraDificuldade.gameObject.SetActive(true);
        caveiraAlteraDificuldade.color = CoresCaveiras[dificuldadeAtual - 1];
    }
}
