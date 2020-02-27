using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ContadorPontos : MonoBehaviour
{
    public static int pontosScore = 0;
    private void OnDestroy()
    {
        pontosScore++;
    }

}
