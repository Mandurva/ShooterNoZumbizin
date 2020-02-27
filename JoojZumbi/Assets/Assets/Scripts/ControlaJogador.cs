using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : MonoBehaviour, IMatavel
{
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public StatusPersonagens meuStatusJogador;
    private RotacaoMouseJogador meuRotacaoJogador;
    private AnimacaoPersonagem meuAnimacaoJogador;
    public ControlaArma[] controlaArma;
    public ControlaInterface controlaInterface;
    public bool ValorNecessario;
    private Vector3 posicaoMouse;
    [SerializeField]
    private AudioClip SomDeDano;
    private bool mortin;

    void Start()
    {
        meuRotacaoJogador = GetComponent<RotacaoMouseJogador>();
        meuAnimacaoJogador = GetComponent<AnimacaoPersonagem>();
        int NumeroSkin = PlayerPrefs.GetInt("Skin") + 3;
        transform.GetChild(NumeroSkin).gameObject.SetActive(true);
    }
    void Awake()
    {
        meuStatusJogador = GetComponent<StatusPersonagens>();
        controlaArma = GetComponentsInChildren<ControlaArma>();
    }
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoZ);
        meuAnimacaoJogador.Movimentar(direcao.magnitude);
        posicaoMouse = Input.mousePosition;
        
    }

    

    void FixedUpdate()
    {
        if (!AnimacaoPersonagem.Morrendo)
        {
            meuRotacaoJogador.Movimentar(direcao, meuStatusJogador.Velocidade);
            meuRotacaoJogador.RotacaoJogador(MascaraChao, posicaoMouse);
        }
    }


    //Encapsulando Variáveis
    public void TomarDano(float danoZumbi)
    {
        meuStatusJogador.Vida -= danoZumbi;
        meuStatusJogador.Escudo = meuStatusJogador.Vida - meuStatusJogador.VidaInicial;
        if(meuStatusJogador.Escudo < 0)
        {
            meuStatusJogador.Escudo = 0;
        }
        controlaInterface.AtualizarSliderVida();
        if (!mortin)
        {
            ControlaAudio.instancia.PlayOneShot(SomDeDano);
        }
        if (meuStatusJogador.Vida <= 0 && !mortin)
        {
            Morrer();
            mortin = true;
        }
    }
    public void Morrer()
    {
        meuAnimacaoJogador.Morreu();
    }
   
    void OnTriggerEnter(Collider other)
    {
        if (ValorNecessario)
        {
            if (other.gameObject.CompareTag("ColetavelVida"))
            {
                int vidaEntregue = other.GetComponentInParent<ItensColetaveis>().ValorDaVida;
                meuStatusJogador.AumentaVida(vidaEntregue);
                controlaInterface.AtualizarSliderVida();
                other.GetComponentInParent<ItensColetaveis>().AoColetarItem(other.transform.parent.GetComponentInParent<Transform>().GetSiblingIndex());
                other.GetComponentInParent<ItensColetaveis>().numeroDaVez = Random.Range(0, other.GetComponentInParent<ItensColetaveis>().PackDeItem.Length);
            }
            if (other.gameObject.CompareTag("ColetavelMunicao"))
            {
                foreach (ControlaArma controlaArma in controlaArma)
                {
                    controlaArma.AoColetarMunicao();
                }
                other.GetComponentInParent<ItensColetaveis>().AoColetarItem(other.transform.parent.GetComponentInParent<Transform>().GetSiblingIndex());
                other.GetComponentInParent<ItensColetaveis>().numeroDaVez = Random.Range(0, other.GetComponentInParent<ItensColetaveis>().PackDeItem.Length);
            }
        }
        ValorNecessario = false;
    }

    

}
