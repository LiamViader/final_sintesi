using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MeshInteractuable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera2;
    [SerializeField] private GameObject gameObj1; 
    [SerializeField] private GameObject gameObj2; 

    public virtual void Interact() {
        gameObj1.SetActive(false);
        gameObj2.SetActive(true);
        cinemachineVirtualCamera.gameObject.SetActive(true);
        cinemachineVirtualCamera2.gameObject.SetActive(false);
    }

    public string returnInteractText(){
        return interactText;
    }

}
