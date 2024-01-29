using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoolProofMenu : MonoBehaviour
{

    

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

    }
    public void SortirMenu()
    {
        gameObject.SetActive(false);
        ControlMenu._instance.Tornar();

    }
}
