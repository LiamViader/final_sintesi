using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Referencies")]
    public Transform orientacio;
    public Transform jugador;
    public Transform mesh;
    public Rigidbody Rbody;

    public float velRotacio;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //girar orientacio
        Vector3 mirarDir = jugador.position = new Vector3(transform.position.x, jugador.position.y, transform.position.z);
        orientacio.forward = mirarDir.normalized;

        //girar jugador mesh
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientacio.forward * verticalInput + orientacio.right * horizontalInput;
        if (inputDir != Vector3.zero) mesh.forward = Vector3.Slerp(mesh.forward, inputDir.normalized, Time.deltaTime * velRotacio);

    }

}
