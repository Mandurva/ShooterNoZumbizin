using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiraBoss : MonoBehaviour
{
    private Rigidbody meuRigidbody;
    private Transform jogador;
    public int danoBoss;
    public int VidaBoss;
    [SerializeField]
    public float vida;
    private Animator animacao;
    private GameObject carroDestruido;
    public bool indestrutivel;
    public Material bossCor;
    public float[] valoresEsperados;
    public ControlaGeradoresBoss GeracaoZumbis;
    public static int ControlaDificuldadeBoss;
    public ControlaInterfaceBoss interfaceBoss;
    private void Awake()
    {
        interfaceBoss = GameObject.FindWithTag("Interface").GetComponentInChildren<ControlaInterfaceBoss>();
        GeracaoZumbis = GameObject.FindWithTag("GeradorZumbisBoss").GetComponent<ControlaGeradoresBoss>();
        vida = VidaBoss;
    }
    void Start()
    {
        interfaceBoss.PegaElementos();
        GeracaoZumbis.PegaComponentesDoBoss();
        jogador = GameObject.FindWithTag("Jogador").transform;
        meuRigidbody = GetComponent<Rigidbody>();
        animacao = GetComponent<Animator>();
        Destrutivel();
        ControlaDificuldadeBoss = 0;
        interfaceBoss.AtivaInterfaceBoss();
    }

    void Update()
    {
        Vector3 direcao = jogador.position - transform.position;
        Rotacionar(direcao);

    }
    public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novaRotacao);
    }
    public void AtacaJogador()
    {
        jogador.GetComponent<ControlaJogador>().TomarDano(danoBoss);
    }
    public void TomarDano(int danoRecebido)
    {
        if (!indestrutivel)
        {
            vida -= danoRecebido;
            interfaceBoss.AjustaVidaBoss();
        }
        if(vida <= 0)
        {
            Destroy(gameObject);
        }
        else if(ControlaDificuldadeBoss < valoresEsperados.Length && !indestrutivel)
        {
            if (vida <= valoresEsperados[ControlaDificuldadeBoss])
            {
                Indestrutibilidade();
                
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carro"))
        {
            carroDestruido = other.gameObject;
            animacao.SetTrigger("AtacarCarro");
         

        }        
    }
    private void AtacaCarro()
    {
        if (carroDestruido.TryGetComponent<ExplosaoCarro>(out ExplosaoCarro explosao))
        {
            carroDestruido.GetComponent<ExplosaoCarro>().Explosao();
        }
        Destroy(carroDestruido);
        animacao.ResetTrigger("AtacarCarro");
    }
    private void Indestrutibilidade()
    {
        indestrutivel = true;
        bossCor.color = Color.blue;
        GeracaoZumbis.GerarZumbis();
        interfaceBoss.Invulneravel();
        interfaceBoss.MostraTextoAviso();
    }
    public void Destrutivel()
    {
        indestrutivel = false;
        bossCor.color = Color.red;
        GeracaoZumbis.RestaurarGeradores();
        ControlaDificuldadeBoss++;
        interfaceBoss.Invulneravel();
    }
    private void OnDestroy()
    {
        MostraVitoria();
        PlayerPrefs.SetInt("VirouOJogo", 1);
    }
    void MostraVitoria()
    {
        interfaceBoss.VitoriaUhu();
    }
}
