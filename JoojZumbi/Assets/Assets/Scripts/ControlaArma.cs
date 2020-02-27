using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaArma : MonoBehaviour
{
    
    public int MunicaoPistolaMax;
    public int NumeroArma = 0;
    public int TempoRecarregar;
    public AudioClip SomDoTiro;
    public GameObject CanoDaArma;
    public int MunicaoMochila;
    public float cronometroRecarregamento = 0;
    public static int municaoRifle;
    public ItensColetaveis controlaColetaveis; 

    [HideInInspector]
    public int municaoPistola;

    [HideInInspector]

    public bool recarregando;
    
    [HideInInspector]
    public GameObject Bala;

    [HideInInspector]
    public ControlaInterface controlaInterface;

    
    public void Awake() 
    {
        cronometroRecarregamento = 0;
        municaoPistola = MunicaoPistolaMax;
        controlaInterface =
            GameObject.FindWithTag("Interface").GetComponent<ControlaInterface>();
    }
   

    public IEnumerator Recarregar()
    {
        if (MunicaoMochila > 0)
        {
            while (cronometroRecarregamento < TempoRecarregar)
            {
                cronometroRecarregamento += Time.deltaTime;
                controlaInterface.AtualizarSliderRecarregamento();
                yield return null;
            }
            if (cronometroRecarregamento >= TempoRecarregar)
            {

                cronometroRecarregamento = 0;
                recarregando = false;
                if (MunicaoMochila >= MunicaoPistolaMax)
                {
                    MunicaoMochila -= MunicaoPistolaMax;
                    municaoPistola = MunicaoPistolaMax;
                }
                else
                {
                    municaoPistola = MunicaoMochila;
                    MunicaoMochila -= MunicaoMochila;
                }
                controlaInterface.AtualizarPentes();
                controlaInterface.AtualizarMunicao();
                controlaInterface.AtualizarSliderRecarregamento();
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CanoDaArma.transform.position, 0.1f);
    }
   
    public virtual void AoColetarMunicao()
    {
       
    }
}
