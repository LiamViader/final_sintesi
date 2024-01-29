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
}
