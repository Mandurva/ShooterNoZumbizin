using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator meuAnimator;
    private float cronometrozin;
    public static bool Morrendo;
    private bool iteradorCronometrozin = false;
    public ControlaInterface controlaInterface;
    private void Awake()
    {
        meuAnimator = GetComponent<Animator>();
        meuAnimator.SetBool("Morreu", false);
    }
    private void Update()
    {
        if (iteradorCronometrozin)
        {
            StartCoroutine(Morte());
               
        }
        
    }
    public void Atacar(bool estado)
    {
        meuAnimator.SetBool("Atacando", estado);
    }

    public void Movimentar(float valorDeMovimento)
    {
        meuAnimator.SetFloat("Movendo", valorDeMovimento);
    }
    public void Morreu()
    {
        Morrendo = true;
        meuAnimator.SetBool("Morreu", true);
        iteradorCronometrozin = true;
        
    }
    public IEnumerator Morte()
    {
        cronometrozin += Time.deltaTime;
        while (cronometrozin < 2)
        {
            Time.timeScale = 0.5f;
            yield return null;
        }
        iteradorCronometrozin = false;
        if (Morrendo)
        {
            controlaInterface.GameOver(true);
        }
        Morrendo = false;
        yield break;
    }
    

}
