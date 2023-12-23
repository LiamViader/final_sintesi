using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MeshInteractuable : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    public virtual void Interact() {
        cinemachineVirtualCamera.m_Priority = 9;
    }

    public string returnInteractText(){
        return interactText;
    }

}
