using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaSkin : MonoBehaviour
{
    public int NumeroSkin;
    public GameObject cameraDeSkin;
    public GameObject cameraMenu;
    private void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        NumeroSkin = PlayerPrefs.GetInt("Skin");
        transform.GetChild(NumeroSkin).gameObject.SetActive(true);
        if (NumeroSkin == 0)
        {
            NumeroSkin = 1;
            transform.GetChild(NumeroSkin).gameObject.SetActive(true);
        }
        PlayerPrefs.SetInt("Skin", NumeroSkin);
    }
    public void ProximaSkin()
    {
        if (NumeroSkin == transform.childCount-2)
        {
            NumeroSkin = transform.childCount - 2;
            return;
        }
        else
        {
            NumeroSkin++;
            transform.GetChild(NumeroSkin).gameObject.SetActive(true);
            transform.GetChild(NumeroSkin - 1).gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("Skin", NumeroSkin);
        
    }
    public void SkinAnterior()
    {
        if (NumeroSkin == 1)
        {
            NumeroSkin = 1;
            return;
        }
        else
        {
            NumeroSkin--;
            transform.GetChild(NumeroSkin).gameObject.SetActive(true);
            transform.GetChild(NumeroSkin + 1).gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("Skin", NumeroSkin);
    }
    public void VoltaAoMenu()
    {
        cameraDeSkin.SetActive(false);
        cameraMenu.SetActive(true);
    }
    public void IrAsSkins()
    {
        cameraDeSkin.SetActive(true);
        cameraMenu.SetActive(false);
    }

}
