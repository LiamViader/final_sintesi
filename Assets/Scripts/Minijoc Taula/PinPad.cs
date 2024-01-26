using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class PinPad : MonoBehaviour
{

    [SerializeField] private TMP_Text _Text;
    [Space][Space]
    [SerializeField] private Button[] _Numeros = new Button[9];
    [SerializeField] private Button _Undo;
    [SerializeField] private Button _Ok;
    [SerializeField] int _Pin;
    [SerializeField] private bool _disableInput; 

    public UnityEvent correcte;
    public UnityEvent incorrecte;

    public UnityEvent clic;
    // Start is called before the first frame update
    void Start()
    {
        if (_Pin == 0){
            _Pin = 7772;
        }
        _disableInput = false;
        _Text.text = string.Empty;
        
    }


    public void OnOkPressed(){
        if (!_disableInput){
           if (Comprova()){
                Debug.Log("Pin Correcte");
                _disableInput = true;
                correcte.Invoke();
           } 
           else {
                Debug.Log("Incorrecte");
                incorrecte.Invoke();
                _Text.text = string.Empty;
                
           }
        }
    }

    public void OnUndoPressed(){
        if (!_disableInput){
            _Text.text = string.Empty;
            clic.Invoke();
        }
    }

    public void OnNumPressed(int num){
        string inputNum = num.ToString();
        if (!_disableInput){
            clic.Invoke();
            if (_Text.text.Length < 4)_Text.text += inputNum;
        }
    }

    private bool Comprova(){
        bool correcte;
        if (int.Parse(_Text.text)==_Pin){
            correcte = true;
            _Text.text = "OK";
        }
        else{
            correcte = false;
        }
        return correcte;
    }
}
