using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_andar : StateMachineBehaviour
{
    Transform jogador;
    Rigidbody rigiboss;
    public float velocidadeBoss = 6f;
    public float distanciaAtaqueBoss = 4f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jogador = GameObject.FindWithTag("Jogador").transform;
        rigiboss = animator.GetComponent<Rigidbody>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 irAte = new Vector3(jogador.position.x, rigiboss.position.y, jogador.position.z);
        Vector3 novaPosicao = Vector3.MoveTowards(rigiboss.position, irAte, velocidadeBoss * Time.fixedDeltaTime *Time.timeScale);
        rigiboss.MovePosition(novaPosicao);

        if (Vector3.Distance(jogador.position, rigiboss.transform.position) <= distanciaAtaqueBoss)
        {
            animator.SetTrigger("Atacar");
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Atacar");
    }
    

}
