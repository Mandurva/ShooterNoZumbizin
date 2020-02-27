using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlaAchievments : MonoBehaviour
{
    [SerializeField]
    private Image imagem1Achiev;
    [SerializeField]
    private Image imagem2Achiev;

    private ControlaMenuPrincipal menuPrincipal;
    void Start()
    {
        menuPrincipal = GetComponentInParent<ControlaMenuPrincipal>();
        if(menuPrincipal.ZerouOGame == 1) 
        {
            Ativa1Achiev();
        }
        if(menuPrincipal.scoreRecorde >= 200)
        {
            Ativa2Achiev();
        }

    }
    private void Ativa1Achiev()
    {
        var Cor1 = imagem1Achiev.color;
        Cor1.a = 1f;
        imagem1Achiev.color = Cor1;
    }
    private void Ativa2Achiev()
    {
        var Cor2 = imagem2Achiev.color;
        Cor2.a = 1f;
        imagem2Achiev.color = Cor2;
    }

}
