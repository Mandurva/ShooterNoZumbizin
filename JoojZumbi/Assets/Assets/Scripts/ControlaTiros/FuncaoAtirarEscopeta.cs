using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncaoAtirarEscopeta : ControlaArma
{
    private ControlaArma controlaArma;
    [SerializeField]
    public Animator animacaoEsfera;
    private float cronometroAnimacaoEsfera;
    private bool soltaCronometro;
    public AudioClip SomRecarregamentoEscopeta;
    public AudioClip SomTiroEscopeta;
    void Start()
    {
        cronometroAnimacaoEsfera = 0.5f;
        controlaArma = GetComponent<ControlaArma>();
    }

    void Update()
    {
        if(soltaCronometro)
        {
            cronometroAnimacaoEsfera -= Time.deltaTime;
        }
        if(cronometroAnimacaoEsfera <= 0)
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
                    ControlaAudio.instancia.PlayOneShot(SomRecarregamentoEscopeta);
                    controlaArma.recarregando = true;
                }
            }
        }
    }
    public void Atirar()
    {
        
        Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
        Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation * Quaternion.Euler(new Vector3(0, 20, 0)));
        Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation * Quaternion.Euler(new Vector3(0, 10, 0)));
        Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation * Quaternion.Euler(new Vector3(0, -10, 0)));
        Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation * Quaternion.Euler(new Vector3(0, -20, 0)));
        ControlaAudio.instancia.PlayOneShot(SomTiroEscopeta);
        animacaoEsfera.SetBool("Atirou", true);
        soltaCronometro = true;
    }
    public override void AoColetarMunicao()
    {
        MunicaoMochila += (controlaColetaveis.ValorDaMunicao - controlaColetaveis.MunicaoRifle) * MunicaoPistolaMax;
        controlaInterface.AtualizarPentes();
    }
}
