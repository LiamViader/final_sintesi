using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private playerInteract player_interact;
    [SerializeField] private TextMeshProUGUI interactTextMeshProGUI;

    private void Update(){
        if (player_interact.GetInteractuableObject() != null)
        {
            Show(player_interact.GetInteractuableObject());
        } else
        {
            Hide();
        }
    }
    private void Show(MeshInteractuable meshInteractuable){
        containerGameObject.SetActive(true);
        interactTextMeshProGUI.text = meshInteractuable.returnInteractText();
    }

    private void Hide(){
        containerGameObject.SetActive(false);
    }
}
