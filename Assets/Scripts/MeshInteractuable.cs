using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MeshInteractuable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private GameObject gameObj1; 
    [SerializeField] private GameObject gameObj2; 

    public virtual void Interact() {
        gameObj1.SetActive(false);
        gameObj2.SetActive(true);
        cinemachineVirtualCamera.Priority = 9;
    }

    public string returnInteractText(){
        return interactText;
    }

}
