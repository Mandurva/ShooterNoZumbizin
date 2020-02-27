using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosaoCarro : MonoBehaviour
{
    public GameObject particulasExplosao;
    private float raioEsfera = 10f;
    public float forcaExplosao = 500f;
    public float danoExplosao = 15f;
    public Vector3 valorEscala = new Vector3(1f, 1f, 1f);
    public AudioClip SomDaExplosao;
    public void Explosao()
    {
        particulasExplosao.transform.localScale = valorEscala;
        Instantiate(particulasExplosao, transform.position, transform.rotation);
        Collider[] colisaoExplosao = Physics.OverlapSphere(transform.position, raioEsfera);
        foreach(Collider objetoProximo in colisaoExplosao)
        {
            Rigidbody rb = objetoProximo.GetComponent<Rigidbody>();
            if(rb != null && !objetoProximo.CompareTag("Boss"))
            {
                rb.AddExplosionForce(forcaExplosao, transform.position, raioEsfera);
            }
            ControlaJogador jogador = objetoProximo.GetComponent<ControlaJogador>();
            if (jogador != null)
            {
                jogador.TomarDano(danoExplosao);
            }
        }
        ControlaAudio.instancia.PlayOneShot(SomDaExplosao);
    }
}
