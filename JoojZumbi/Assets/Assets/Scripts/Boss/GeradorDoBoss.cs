using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorDoBoss : MonoBehaviour
{
    public GameObject Boss;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
  

    public void ChamaBoss()
    {
        Instantiate(Boss, transform.position, transform.rotation);
    }
}
