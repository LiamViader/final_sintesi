using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MeshInteractuable : MonoBehaviour
{
    [SerializeField] private string interactText;
    public virtual void Interact() {
        SceneManager.LoadScene("Scenes/Minijocs/Minijoc 1");
    }

    public string returnInteractText(){
        return interactText;
    }
}
