using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    private playerInteract player_interact;
    [SerializeField] private TextMeshProUGUI interactTextMeshProGUI;

    private void Start()
    {
        player_interact = ControlPersonatge._instance.GetComponent<playerInteract>();
    }

    private void Update(){
        if (player_interact.GetInteractuableObject() != null)
        {
            Show(player_interact.GetInteractuableObject());
        } else
        {
            Hide();
        }
    }
    private void Show(Interactuable meshInteractuable){
        containerGameObject.SetActive(true);
        interactTextMeshProGUI.text = meshInteractuable.returnInteractText();
    }

    private void Hide(){
        containerGameObject.SetActive(false);
    }
}
