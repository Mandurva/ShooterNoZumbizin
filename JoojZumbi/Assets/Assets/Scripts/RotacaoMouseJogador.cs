using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoMouseJogador : MovimentoPersonagem
{
    public Camera MainCamerazinha;
    float impactoAntigo;
    private void Start()
    {
        impactoAntigo = Input.mousePosition.magnitude;
    }
    public void RotacaoJogador(LayerMask MascaraChao, Vector3 PosicaoMouse)
    {
        
        Ray raio = MainCamerazinha.ScreenPointToRay(PosicaoMouse);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - new Vector3(transform.position.x,0, transform.position.z);
            if (impactoAntigo != impacto.point.magnitude)
            {
                posicaoMiraJogador.y = 0; //transform.position.y

                Rotacionar(posicaoMiraJogador);
            }


            impactoAntigo = impacto.point.magnitude;
        }
    }
  

}
