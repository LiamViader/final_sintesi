using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiMissatge : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private TextMeshProUGUI interactTextMeshProGUI;
    private float _timeLeft = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeLeft <= 0)
        {
            containerGameObject.SetActive(false);
        }
        else
        {
            _timeLeft -= Time.deltaTime;
        }
    }

    public void Mostrar(string mis, float segons)
    {
        _timeLeft = segons;
        containerGameObject.SetActive(true);
        interactTextMeshProGUI.text = mis;
    }
}
