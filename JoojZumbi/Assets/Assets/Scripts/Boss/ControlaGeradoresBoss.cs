using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaGeradoresBoss : MonoBehaviour
{
    private GeradorBoss[] geradoresBoss;
    public GiraBoss controleBoss;
    private Transform jogador;
    private int i = 0;
    public Transform[] localizacao;
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        geradoresBoss = GetComponentsInChildren<GeradorBoss>();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        
    }
    void LateUpdate()
    {
        if(GeradorBoss.ZumbisMortos == geradoresBoss[i].LimiteZumbis)
        {
            controleBoss.Destrutivel();
            GeradorBoss.ZumbisMortos = 0;
        }

    }

    public void GerarZumbis()
    {
        int j = 0;
        i = 0;
        float maiorDistancia = 0;
        foreach (Transform geradores in localizacao)
        {
            float DistanciaDoJogador = Vector3.Distance(localizacao[j].position, jogador.position);
            if(DistanciaDoJogador> maiorDistancia)
            {
                maiorDistancia = DistanciaDoJogador;
                i = j;
            }
            j++;
            
        }
        transform.GetChild(i).gameObject.SetActive(true);
        geradoresBoss[i].zumbisGerados = 0;
       
    }
    public void RestaurarGeradores()
    {
        i = 0;
        foreach (Transform geradores in gameObject.transform)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            i++;
        }
        i = 0;
    }
    public void PegaComponentesDoBoss()
    {
        controleBoss = GameObject.FindWithTag("Boss").GetComponent<GiraBoss>();
    }
}
