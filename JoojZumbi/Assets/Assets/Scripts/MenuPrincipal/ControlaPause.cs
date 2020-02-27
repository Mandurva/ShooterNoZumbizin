using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlaPause : MonoBehaviour
{
    public static bool JogoPausado = false;
    public GameObject MenuPause;
    private void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (JogoPausado)
            {
                ResumeJogo();
            }
            else
            {
                PausaJogo();
            }
        }      
    }
    public void ResumeJogo()
    {

        Time.timeScale = 1;
        JogoPausado = false;
        MenuPause.SetActive(false);
    }
    void PausaJogo()
    {
        Time.timeScale = 0;
        JogoPausado = true;
        MenuPause.SetActive(true);
    }
    public void VoltaAoMenu()
    {
        Time.timeScale = 1;
        JogoPausado = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }

    public void SairDoJogo()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
