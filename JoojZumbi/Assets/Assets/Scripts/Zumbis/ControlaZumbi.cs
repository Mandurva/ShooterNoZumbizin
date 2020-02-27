
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ControlaZumbi : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    public float DistanciaAtaque;
    
    private float danoZumbi;
    private Vector3 direcao;
    private Vector3 posicaoAleatoria;
    private MovimentoPersonagem movimentaInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private StatusPersonagens meuStatusInimigo;
    private float contadorVagar;
    public ParticleSystem particulasMorte;
    public float DistanciaPerseguicao;
    private bool Ouviu;
    private Vector3 CentroEsfera;
    private bool perseguindo;

    [SerializeField]
    private AudioClip SomDaMorteDoZumbi;

    [SerializeField]
    private float raioEsferaAleatoria;
   

    [SerializeField]
    private float erroPosicao = 0.5f;
    
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentaInimigo = GetComponent<MovimentoPersonagem>();
        meuStatusInimigo = GetComponent<StatusPersonagens>();
        AleatorizarZumbi();
        meuStatusInimigo.EscolheTamanho();
        DistanciaAtaque *= meuStatusInimigo.Tamanho;
    }

  

    void FixedUpdate()
    {
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        

        if (distancia > DistanciaPerseguicao && !Ouviu && !perseguindo)
        {
            Vagar();

        }
        else if (distancia > DistanciaPerseguicao && Ouviu && !perseguindo)
        {
            IrAteEsfera(CentroEsfera);
        }

        else if (distancia > DistanciaAtaque && distancia < DistanciaPerseguicao || perseguindo)
        {
            Ouviu = false;
            perseguindo = true;
            if(distancia <= DistanciaAtaque)
            {
                perseguindo = false;
            }
            Perseguir();
        }
        else if(distancia <= DistanciaAtaque)
        {
            perseguindo = false;
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
        movimentaInimigo.Rotacionar(direcao);
        animacaoInimigo.Movimentar(direcao.magnitude);
    }

    private void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if (contadorVagar <= 0)
        {
            posicaoAleatoria = GerarPosicaoAleatoria();
            contadorVagar = meuStatusInimigo.tempoProximaPosicao;
        }
        if (Vector3.Distance(transform.position, posicaoAleatoria) >= erroPosicao)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentaInimigo.Movimentar(direcao, meuStatusInimigo.Velocidade);
        }

    }
    private void Perseguir()
    {
        direcao = Jogador.transform.position - transform.position;
        movimentaInimigo.Movimentar(direcao, meuStatusInimigo.Velocidade);
        animacaoInimigo.Atacar(false);
        if(Vector3.Distance(transform.position, Jogador.transform.position) > DistanciaPerseguicao)
        {
            animacaoInimigo.Atacar(false);
            contadorVagar -= Time.deltaTime;
            if (contadorVagar <= 0)
            {
                animacaoInimigo.Atacar(false);
                perseguindo = false;
                contadorVagar = meuStatusInimigo.tempoProximaPosicao;
            }            
        }
    }
    private void IrAteEsfera(Vector3 posicaoCentral)
    {
        contadorVagar -= Time.deltaTime;
        direcao = posicaoCentral - transform.position;
        posicaoCentral.y = 0;
        if (Vector3.Distance(transform.position, posicaoCentral) >= erroPosicao && contadorVagar > 0)
        {
            direcao = posicaoCentral - transform.position;
            movimentaInimigo.Movimentar(direcao, meuStatusInimigo.Velocidade);
        }
        else
        {
            contadorVagar = meuStatusInimigo.tempoProximaPosicao;
            Ouviu = false;
        }
    }

    private Vector3 GerarPosicaoAleatoria()
    {
        Vector3 posicao = Random.insideUnitSphere * raioEsferaAleatoria;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    void AtacaJogador()
    {
        danoZumbi = meuStatusInimigo.DanoZumbi;
        Jogador.GetComponent<ControlaJogador>().TomarDano(danoZumbi);
    }
    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    public void TomarDano(float Dano)
    {
        meuStatusInimigo.VidaZumbi -= (int)Dano;
        if(meuStatusInimigo.VidaZumbi <= 0)
        {
            particulasMorte.Play();
            Morrer();
        }
    }

    public void Morrer()
    {
        particulasMorte.transform.parent = null;
        particulasMorte.Play();
        Destroy(gameObject);
        ControlaAudio.instancia.PlayOneShot(SomDaMorteDoZumbi);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SomTiro")) 
        {
            contadorVagar = meuStatusInimigo.tempoProximaPosicao;
            Ouviu = true;
            CentroEsfera = other.transform.position;
        }
    }

}
