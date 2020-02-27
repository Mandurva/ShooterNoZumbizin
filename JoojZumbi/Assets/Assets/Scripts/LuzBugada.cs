using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzBugada : MonoBehaviour
{
    // Start is called before the first frame update
    private float cronometro = 0;
    private ControlaInterface controlaInterface;
    [SerializeField]
    private Light luzPoste;
    private void Start()
    {
        controlaInterface = 
            GameObject.FindWithTag("Interface").GetComponent<ControlaInterface>();
        StartCoroutine((PiscarLuz()));
    }
 
    private IEnumerator PiscarLuz()
    {
        while (!controlaInterface.FimDeJogo)
        {
            cronometro += Time.deltaTime;
            if (cronometro >= 5)
            {
                luzPoste.intensity = 0;
                if (cronometro > 8)
                {
                    cronometro = 0;
                }
            }
            else
            {
                luzPoste.intensity = 3 + Random.Range(1, 10);
            }
            yield return null;
        }
    }
}
