using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoolProofMenu : MonoBehaviour
{
    [SerializeField]
    private Minijoc1 _minijocLlums;

    [SerializeField]
    private ComprovaButton _minijocRadio;

    [SerializeField]
    private PinPad _contrasenyaMiralls;

    [SerializeField]
    private Tecla _portaArmari;

    [SerializeField]
    private Tecla _portaFinal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObrirLlums()
    {
        _minijocLlums.Completat();
        SortirMenu();
    }
    public void SortirMenu()
    {
        gameObject.SetActive(false);
        ControlMenu._instance.Tornar();

    }

    public void EngegarRadio()
    {
        ObrirLlums();
        _minijocRadio.correcte.Invoke();
    }

    public void ObrirPortaMiralls()
    {
        EngegarRadio();
        _contrasenyaMiralls.correcte.Invoke();
    }

    public void AdquirirDispositiu()
    {
        ObrirPortaMiralls();
        ControlPersonatge._instance.AgafarDispositiu();
    }

    public void ObrirPortaArmari()
    {
        AdquirirDispositiu();
        _portaArmari.OnClic.Invoke();
    }

    public void ObrirPortaFinal()
    {
        ObrirPortaArmari();
        _portaFinal.OnClic.Invoke();
    }
}
