using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterfaceBoss : MonoBehaviour
{
    public Slider VidaBoss;
    public Image CorVida;
    public Text TextoZumbisRestantes;
    public Text TextoAvisoMatarZumbis;
    public GiraBoss statusBoss;
    public GeradorBoss[] statusZumbis;
    private float cronometroTexto = 3f;
    public GameObject telaVitoria;
    public ControlaInterface interfaceJogador;
    private void Start()
    {
        
        TextoZumbisRestantes.text = string.Format("{0}", 0);
    }
    private void Update()
    {
        AlteraNumeroDeZumbisRestantes();
        if (TextoAvisoMatarZumbis.IsActive())
        {
            cronometroTexto -= Time.deltaTime;
            if (cronometroTexto <= 0)
            {
                TextoAvisoMatarZumbis.gameObject.SetActive(false);
                cronometroTexto = 3f;
            }
        }

    }
    public void AjustaVidaBoss()
    {
        VidaBoss.value = statusBoss.vida;

    }
    public void MostraTextoAviso()
    {
        TextoAvisoMatarZumbis.gameObject.SetActive(true);
    }
    public void Invulneravel()
    {
        if (statusBoss.indestrutivel)
        {
            CorVida.color = Color.blue;
        }
        else
        {
            CorVida.color = Color.red;
        }
    }

    public void AlteraNumeroDeZumbisRestantes()
    {
        int i = 0;
        foreach (GeradorBoss geradores in statusZumbis)
        {
            if (geradores.isActiveAndEnabled)
            {
                TextoZumbisRestantes.text = string.Format("{0}", statusZumbis[i].zumbisGerados - GeradorBoss.ZumbisMortos);
            }
            i++;
        }
    }
    public void PegaElementos()
    {
        statusBoss = GameObject.FindWithTag("Boss").GetComponent<GiraBoss>();
        VidaBoss.maxValue = statusBoss.vida;
        AjustaVidaBoss();
    }
    public void AtivaInterfaceBoss()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void VitoriaUhu()
    {
        telaVitoria.SetActive(true);
        interfaceJogador.GameOver(false);
    }
}
