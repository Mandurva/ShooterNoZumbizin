using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade;
    private Rigidbody rigidbodyBala;
   
    private int i;
    private int reflexao = 0;
    private float anguloReflexao;
   
    [SerializeField]
    
    private ParticleSystem explosaoAoBater;

    [SerializeField]
    
    private int danoArma;

    [SerializeField]
    
    private int numeroDeBatidas;
    private void Start()
    {
        i = numeroDeBatidas;
        rigidbodyBala = GetComponent<Rigidbody>();
        
    }
    void FixedUpdate()
    {
        if (reflexao % 2 == 0)
        {
            Tiro();
        }
        else
        {
            Reflete();
        }
               
    }

    void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.CompareTag("Boss"))
        {
            objetoDeColisao.GetComponent<GiraBoss>().TomarDano(danoArma);
            if (explosaoAoBater != null)
            {
                explosaoAoBater.Play();
                explosaoAoBater.transform.parent = null;
            }
            i--;
        }
        
        if (objetoDeColisao.CompareTag("Inimigo"))
        {
            
            ControlaZumbi Cz = objetoDeColisao.GetComponent<ControlaZumbi>();
            if(Cz == null)
            {
                ControlaZumbiEspecial Czs = objetoDeColisao.GetComponent<ControlaZumbiEspecial>();
                Czs.TomarDano(danoArma);
            }
            else if(Cz != null)
            {
                Cz.TomarDano(danoArma);
            }
            if (explosaoAoBater != null)
            {
                explosaoAoBater.Play();
                explosaoAoBater.transform.parent = null;
            }
            i--;
        }
        else if (!objetoDeColisao.CompareTag("SomTiro"))
        {
            i--;
        }

        if (i <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (!objetoDeColisao.CompareTag("SomTiro"))
            {
                anguloReflexao = Random.Range(0, 30);
                rigidbodyBala.rotation *= Quaternion.Euler(0, anguloReflexao, 0);
                reflexao++;
            }
        }
    }
    void Tiro()
    {
            rigidbodyBala.MovePosition
            (rigidbodyBala.position
            + transform.forward * Velocidade * Time.deltaTime);
    }
    void Reflete()
    {
       
        rigidbodyBala.MovePosition
            (rigidbodyBala.position -
             transform.forward * Velocidade * Time.deltaTime);
        
    }
}
