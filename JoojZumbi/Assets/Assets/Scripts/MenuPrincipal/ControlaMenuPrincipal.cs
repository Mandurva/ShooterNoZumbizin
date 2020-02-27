using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
public class ControlaMenuPrincipal : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioClip SomInicioJogo;
    public int ZerouOGame;
    public int scoreRecorde;
    public float tempoRecorde;
    public TextMeshProUGUI textoTempoRecorde;
    public TextMeshProUGUI textoScoreRecorde;
    public Slider sliderCarregamento;
    public Toggle ToggleTutorial;
    private bool TutorialLigado;
    private void Awake()
    {
        tempoRecorde = PlayerPrefs.GetFloat("TempoMaximo");
        ZerouOGame = PlayerPrefs.GetInt("VirouOJogo");
        scoreRecorde = PlayerPrefs.GetInt("PontuacaoMaxima");
        TutorialLigado = PlayerPrefs.GetInt("Tutorial") == 0;
        if (TutorialLigado)
        {
            ToggleTutorial.isOn = true;
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else if (!TutorialLigado)
        {
            ToggleTutorial.isOn = false;
            PlayerPrefs.SetInt("Tutorial", 0);
        }
        PontuacaoRecorde();
    }
    public void ComecaJogo()
    {
        StartCoroutine(CarregarNivelAssincrono(SceneManager.GetActiveScene().buildIndex + 1));
        ControlaAudio.instancia.PlayOneShot(SomInicioJogo);
       
    }
    public void SaiDoJogo()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void AlteraVolume(float volume)
    {
        audioMixer.SetFloat("VolumeJogo", volume);
    }

    public void AjustaQualidade(int qualidade)
    {
        QualitySettings.SetQualityLevel(qualidade);
    }
    public void TelaCheia(bool telaCheia)
    {
        Screen.fullScreen = telaCheia;
    }
    public void PontuacaoRecorde()
    {
        int minutosSobrevividos = (int)(tempoRecorde / 60);
        int segundosSobrevividos = (int)(tempoRecorde % 60);
        if (segundosSobrevividos < 10)
        {
            textoTempoRecorde.text =
                string.Format("Tempo: {0}:0{1}", minutosSobrevividos, segundosSobrevividos);
        }
        else
        {
            textoTempoRecorde.text =
                string.Format("Tempo: {0}:{1}", minutosSobrevividos, segundosSobrevividos);
        }
        textoScoreRecorde.text =
                string.Format("Score: {0}", scoreRecorde);
    }
    

    IEnumerator CarregarNivelAssincrono(int indexNivel)
    {
        AsyncOperation operacao = SceneManager.LoadSceneAsync(indexNivel);
        while (!operacao.isDone)
        {
            float progresso = Mathf.Clamp01(operacao.progress / .9f);
            sliderCarregamento.value = progresso;
            yield return null;
        }
    }

    public void DesativaTutorial()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }
    }

   
}
