using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

const vel_caminar;
const vel_correr;

//vec Direcció --> És un vector normalitzat que indica cap a on mira el personatge
struct Personatge{
    vector dir = new Vector3(0,1,0);
    double vel;
    bool moviment;
}
public class Caminar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DKey))
        {
            Personetge.dir = Personate.dir + [0, 1, 0];
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;
        }
        else if (Input.GetKey(KeyCode.AKey))
        {
            Personetge.dir = Personate.dir + [0, -1, 0];
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;
        }
        else if (Input.GetKey(KeyCode.Skey))
        {
            Personetge.dir = Personate.dir + [ -1,0, 0];
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;

        }
        else if (Input.GetKey(KeyCode.WKey))
        {
            Personetge.dir = Personate.dir + [1, 0, 0];
            Personatge.dir.normalize();
            Personatge.vel = vel_caminar;
            moviment = true;
        }
        if (Input.GetKey(KeyCode.Shiftkey))
        {
            Personatge.vel = vel_correr;
            moviment = true;
        }
        if (moviment)
        {
            Update_position(Personatge.dir * Personatge.vel);
        }
    }
}
