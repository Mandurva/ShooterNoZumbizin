using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapa : MonoBehaviour
{
    public Transform Jogador;
    
    void LateUpdate()
    {
        Vector3 posicao = Jogador.position;
        posicao.y = transform.position.y;
        transform.position = posicao;
    }
}
