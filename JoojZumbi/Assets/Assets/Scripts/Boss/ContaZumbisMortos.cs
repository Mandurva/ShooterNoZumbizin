using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContaZumbisMortos : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDestroy()
    {
        GeradorBoss.ZumbisMortos++;
    }
}
