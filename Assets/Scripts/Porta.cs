using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    private bool oberta = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (oberta && transform.position.y < 100) transform.Translate(0, 1f * Time.fixedDeltaTime, 0);
        else if (transform.position.y >= 100) Destroy(gameObject);
    }

    public void Obrir()
    {
        oberta = true;
    }
}
