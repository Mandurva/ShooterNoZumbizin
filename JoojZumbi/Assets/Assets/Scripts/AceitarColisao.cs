using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceitarColisao : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jogador"))
        {
            other.GetComponent<ControlaJogador>().ValorNecessario = true;
        }
    }
}
