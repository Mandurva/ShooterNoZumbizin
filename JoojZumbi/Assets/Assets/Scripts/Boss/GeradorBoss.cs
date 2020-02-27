using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorBoss : MonoBehaviour
{
    private float contadorTempo = 0;
    private GameObject jogador;
    public float TempoGerarZumbis;
    public int LimiteZumbis;
    public int zumbisGerados;
    public int VidaZumbi;
    public float DistanciaPerseguirZumbi;
    public static int ZumbisMortos;

    [SerializeField]
    private LayerMask LayerZumbi;
    
    public GameObject Zumbi;
    

    [SerializeField]
    private float esferaCriacao;
    
    [SerializeField]
    private float DistanciaDoJogadorParaGeracao;

    private ControlaZumbiEspecial controlaZumbi;
    private StatusZumbiEspecial statusZumbiEspecial;

    // Start is called before the first frame update
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        controlaZumbi = Zumbi.GetComponent<ControlaZumbiEspecial>();
        statusZumbiEspecial = Zumbi.GetComponent<StatusZumbiEspecial>();
        zumbisGerados = 0;
        ZumbisMortos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        controlaZumbi.DistanciaPerseguicao = DistanciaPerseguirZumbi;
        statusZumbiEspecial.VidaInicial = VidaZumbi;

        if (Vector3.Distance
            (transform.position, jogador.transform.position)
            > DistanciaDoJogadorParaGeracao && zumbisGerados != LimiteZumbis)
        {
            contadorTempo += Time.deltaTime;
            if (contadorTempo >= TempoGerarZumbis)
            {
                StartCoroutine(GerarZumbi());
                contadorTempo = 0;
               
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, esferaCriacao);
    }
    IEnumerator GerarZumbi()
    {
        Vector3 posicaoCriacao = AleatorizarPosicao();
        // Criando uma esfera de Raio (1) verificando se há algo com colisão no local, retorna todos colisores na variável
        // "colisores"
        Collider[] colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoCriacao, 1, LayerZumbi);
            yield return null;
        }
        if (zumbisGerados < LimiteZumbis)
        {
            Instantiate(Zumbi, posicaoCriacao, transform.rotation);
            zumbisGerados++;
        }

        
    }
    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * esferaCriacao;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }
}
