using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador controlarJogador;
    private ControlaArma controlaArma;
    private float tempoPontuacaoMaximaSalva;
    private int pontosSalvos;
    private int numeroTextoArma;
    public bool FimDeJogo;
    public TextMeshProUGUI TextoArma;
    private Color32 corTexto = new Color32(68, 68, 68, 255);
    public GameObject UIJogoCorrido;
    private Dictionary<string, Color[]> coresArmas = new Dictionary<string, Color[]>();
    private Dictionary<int, string> numeroArmas = new Dictionary<int, string>();
    public GameObject Tutorial;
    //Cores Armas
    private Color corPistola1 = new Color32(255, 250, 0, 255);
    private Color corPistola2 = new Color32(94, 0, 74, 255);
    private Color corRR1 = new Color32(255, 0, 0, 255);
    private Color corRR2 = new Color32(255, 210, 0, 255);
    private Color corEscopeta1 = new Color32(0, 255, 246, 255);
    private Color corEscopeta2 = new Color32(39, 255, 4, 255);
    
    //Slider
    public ColorBlock corSlider;
    public Color corVidaMaxima, corVidaMinima;

    [SerializeField]
    private Slider sliderVidaJogador;

    [SerializeField]
    private Slider sliderEscudoJogador;

    [SerializeField]
    private Image imagemSliderVida;

    [SerializeField]
    private GameObject PainelDeGameOver;

    [SerializeField]
    private Text tempoDeSobrevivencia;

    [SerializeField]
    private Text textoTempoMaximo;

    [SerializeField]
    private Text textoPontuacao;

    [SerializeField]
    private Text textoPontuacaoMaxima;

    [SerializeField]
    private Text scoreZumbis;

    [SerializeField]
    private Text municaoArma;
    [SerializeField]
    private Text municaoMochila;

    [SerializeField]
    private Slider sliderRecarregamento;
    private void Awake()
    {
        coresArmas.Add("Pistola", new Color[] { corPistola1, corPistola2 });
        coresArmas.Add("Rifle R.", new Color[] { corRR1, corRR2 });
        coresArmas.Add("Escopeta", new Color[] { corEscopeta1, corEscopeta2 });


        numeroArmas.Add(0, "Pistola");
        numeroArmas.Add(1, "Rifle R.");
        numeroArmas.Add(2, "Escopeta");

        numeroTextoArma = numeroArmas.Count - 1;
    }
    void Start()
    {
        controlarJogador = GameObject.FindWithTag("Jogador").
            GetComponent<ControlaJogador>();

        controlaArma = GameObject.FindWithTag("Jogador").
            GetComponentInChildren<ControlaArma>();

        sliderVidaJogador.maxValue = controlarJogador.meuStatusJogador.Vida;
        AtualizarSliderVida();

        sliderRecarregamento.maxValue = controlaArma.TempoRecarregar;
        AtualizarSliderRecarregamento();
        AtualizarMunicao();
        AtualizarPentes();

        tempoPontuacaoMaximaSalva = PlayerPrefs.GetFloat("TempoMaximo");
        pontosSalvos = PlayerPrefs.GetInt("PontuacaoMaxima");

        ContadorPontos.pontosScore = 0;

        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            Tutorial.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Tutorial") == 1)
        {
            Tutorial.SetActive(true);
        }

    }
    private void Update()
    {
        AtualizarPontuacao();
    }
    public void AtualizarSliderVida()
    {
        sliderVidaJogador.value = controlarJogador.meuStatusJogador.Vida;
        sliderEscudoJogador.value = controlarJogador.meuStatusJogador.Escudo;
        float porcentagemDeVida = (float)controlarJogador.meuStatusJogador.Vida / (float)controlarJogador.meuStatusJogador.VidaInicial;
        Color CorDaVida = Color.Lerp(corVidaMinima, corVidaMaxima, porcentagemDeVida);
        imagemSliderVida.color = CorDaVida;
    }
    public void GameOver(bool morreu)
    {
        Time.timeScale = 0;
        if (morreu) 
        {   
            PainelDeGameOver.SetActive(true);
        }
        int minutosSobrevividos = (int)(Time.timeSinceLevelLoad / 60);
        int segundosSobrevividos = (int)(Time.timeSinceLevelLoad % 60);
        if (segundosSobrevividos < 10)
        {
            tempoDeSobrevivencia.text =
                string.Format("Tempo: {0}:0{1}", minutosSobrevividos, segundosSobrevividos);
        }
        else
        {
            tempoDeSobrevivencia.text =
                string.Format("Tempo: {0}:{1}", minutosSobrevividos, segundosSobrevividos);
        }
        textoPontuacao.text =
                string.Format("Score: {0}", ContadorPontos.pontosScore);
        AjustarTempoMaximo(minutosSobrevividos, segundosSobrevividos);
        AjustarPontuacaoMaxima(ContadorPontos.pontosScore);
        FimDeJogo = true;
        UIJogoCorrido.SetActive(false);
        
    }

  

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene("game_zumbi");
        Time.timeScale = 1;

    }
    void AjustarTempoMaximo(int min, int seg)
    {
        if (Time.timeSinceLevelLoad > tempoPontuacaoMaximaSalva)
        {
            tempoPontuacaoMaximaSalva = Time.timeSinceLevelLoad;
            textoTempoMaximo.text =
                string.Format("Novo Recorde!");
            PlayerPrefs.SetFloat("TempoMaximo", tempoPontuacaoMaximaSalva);
            
        }
        else
        {
            min = (int)(tempoPontuacaoMaximaSalva / 60);
            seg = (int)(tempoPontuacaoMaximaSalva % 60);
            textoTempoMaximo.text =
                string.Format("");
            
        }
    }
    void AjustarPontuacaoMaxima(int pontos)
    {
        if (pontos > pontosSalvos)
        {
            pontosSalvos = pontos;
            textoPontuacaoMaxima.text =
                string.Format("Novo Recorde!", pontosSalvos);
            PlayerPrefs.SetInt("PontuacaoMaxima", pontosSalvos);
            
        }
        else
        {
            textoPontuacaoMaxima.text = string.Format("");
            
        }
    }

    void AtualizarPontuacao()
    {
        scoreZumbis.text = string.Format("{0}", ContadorPontos.pontosScore);
    }

    public void AtualizarMunicao()
    {
        municaoArma.text = string.Format
            ("{0}", controlaArma.municaoPistola);
        municaoArma.color = coresArmas[numeroArmas[numeroTextoArma]][0];

    }
    public void AtualizarPentes()
    {
        municaoMochila.text = string.Format("{0}", (controlaArma.MunicaoMochila / controlaArma.MunicaoPistolaMax));
        municaoMochila.color = coresArmas[numeroArmas[numeroTextoArma]][1];
    }
    public void AtualizarSliderRecarregamento()
    {
        sliderRecarregamento.value = controlaArma.cronometroRecarregamento;
    }
    public void AtualizarArma()
    {
        controlaArma = GameObject.FindWithTag("Jogador").
            GetComponentInChildren<ControlaArma>();
        AtualizarCor();
        AtualizarMunicao();
        AtualizarPentes();
        sliderRecarregamento.maxValue = controlaArma.TempoRecarregar;
        AtualizarSliderRecarregamento();
        

    }
    void AtualizarCor()
    {
        numeroTextoArma++;
        if (numeroTextoArma <= numeroArmas.Count - 1)
        {
            MostrarArma(numeroTextoArma);
        }
        else
        {
            numeroTextoArma = 0;
            MostrarArma(numeroTextoArma);
        }

    }

    void MostrarArma(int numeroDaArma)
    {

        TextoArma.colorGradient = new VertexGradient
            (coresArmas[numeroArmas[numeroDaArma]][0], coresArmas[numeroArmas[numeroDaArma]][0], coresArmas[numeroArmas[numeroDaArma]][1], coresArmas[numeroArmas[numeroDaArma]][1]);
        TextoArma.fontStyle = FontStyles.Bold;
        TextoArma.text = numeroArmas[numeroDaArma];
    }
}
