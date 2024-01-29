using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{

    public static ControlMenu _instance = null;
    private bool playerEnabled = true;
    [SerializeField]
    private GameObject _menuInGame;

    [SerializeField]
    private GameObject _menuFoolProof;

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausar();
        }
        else if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.K))
        {
            FoolProofMenu();
        }
    }

    private void Pausar()
    {
        _menuInGame.SetActive(true);
        playerEnabled = ControlPersonatge._instance.enabled;
        ControlPersonatge._instance.enabled = false;
    }

    private void FoolProofMenu()
    {
        _menuFoolProof.SetActive(true);
        playerEnabled = ControlPersonatge._instance.enabled;
        ControlPersonatge._instance.enabled = false;
    }

    private void OnEnable()
    {
        ControlPersonatge._instance.enabled = playerEnabled;
    }
}
