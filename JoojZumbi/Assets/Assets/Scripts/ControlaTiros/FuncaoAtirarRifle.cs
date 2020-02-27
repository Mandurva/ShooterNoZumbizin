using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncaoAtirarRifle : ControlaArma
{
    private ControlaArma controlaArma;
    public Animator animacaoEsfera;
    private bool soltaCronometro;
    private float cronometroAnimacaoEsfera = 0.5f;
    public AudioClip SomRecarregamentoRifle;
    public AudioClip SomTiroRifle;
    void Start()
    {
        controlaArma = GetComponent<ControlaArma>();
    }

    void Update()
    {
        if (soltaCronometro)
        {
            cronometroAnimacaoEsfera -= Time.deltaTime;
        }
        if (cronometroAnimacaoEsfera <= 0)
        {
            animacaoEsfera.SetBool("Atirou", false);
            soltaCronometro = false;
            cronometroAnimacaoEsfera = 0.5f;
        }
        if (!ControlaPause.JogoPausado)
        {
            if (Input.GetButtonDown("Fire1") && !controlaArma.recarregando)
            {
                if (controlaArma.municaoPistola > 0)
                {
                    Atirar();
                    controlaArma.municaoPistola -= 1;
                    controlaInterface.AtualizarMunicao();
                }
                else
                {
                    StartCoroutine(controlaArma.Recarregar());
                    ControlaAudio.instancia.PlayOneShot(SomRecarregamentoRifle);
                    controlaArma.recarregando = true;
                }
            }
        }
    }
    public void Atirar()
    {
        Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
        animacaoEsfera.SetBool("Atirou", true);
        soltaCronometro = true;
        ControlaAudio.instancia.PlayOneShot(SomTiroRifle);
    }
    public override void AoColetarMunicao()
    {
        MunicaoMochila += (controlaColetaveis.MunicaoRifle) * MunicaoPistolaMax;
        controlaInterface.AtualizarPentes();
    }
}
