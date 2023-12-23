using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Func_Boto : MonoBehaviour
{
    private AssetBundle Carpeta;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LoadScene");
        Carpeta = AssetBundle.LoadFromFile("Assets/Escenes");
    }

    public void LoadA(string scenename)
    {
        SceneManager.LoadScene(scenename);
        Debug.Log("sceneName to load: " + scenename);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
