using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaSpawnDoBoss : MonoBehaviour
{
    private Transform jogador;
    private int i;
    public Transform[] localizacao;
    private float cronometroProvisorio = 5f;
    bool jaChamou;
    public bool ChamaOBossAiPo;
    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);

    }
    private void Update()
    {
        if (ChamaOBossAiPo)
        {
            cronometroProvisorio -= Time.deltaTime;
        }
        if (cronometroProvisorio <= 0 && !jaChamou)
        {
            GerarBoss();
            jaChamou = true;
            GetComponentInChildren<GeradorDoBoss>().ChamaBoss();
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    

    public void GerarBoss()
    {
        int j = 0;
        i = 0;
        float maiorDistancia = 0;
        foreach (Transform geradores in localizacao)
        {
            float DistanciaDoJogador = Vector3.Distance(localizacao[j].position, jogador.position);
            if (DistanciaDoJogador > maiorDistancia)
            {
                maiorDistancia = DistanciaDoJogador;
                i = j;
            }
            j++;

        }
        transform.GetChild(i).gameObject.SetActive(true);
    }
    public void ComecarGeracaoDoBoss()
    {
        ChamaOBossAiPo = true;
    }
}
