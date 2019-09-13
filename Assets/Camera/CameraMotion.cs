using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    private float pitch;
    private float yaw;
    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        // mouse locked to the center of the view refer
        // https://docs.unity3d.com/ScriptReference/Cursor-lockState.html
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
        // this.transform.localPosition = new Vector3(12f, 35f, -150f); // should be all positive?
        // this.transform.localEulerAngles = new Vector3(9.0f, -4.0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // mouse look refer: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        pitch += Input.GetAxis("Mouse Y") * 2.0f;
        yaw +=  Input.GetAxis("Mouse X") * 2.0f;
        
        this.transform.eulerAngles = new Vector3(-pitch, yaw, 0);
        
        // WSAD keyboard control refer: workshop 02
        if(Input.GetKey(KeyCode.W)) {
            this.transform.localPosition += new Vector3(0.0f,0.0f,3f);
        }

        if(Input.GetKey(KeyCode.S)) {
            this.transform.localPosition += new Vector3(0.0f,0f,-3f);
        }

        if(Input.GetKey(KeyCode.D)) { 
            this.transform.localPosition += new Vector3(3f,0f,0f);
        }

        if(Input.GetKey(KeyCode.A)) {
            this.transform.localPosition += new Vector3(-3f,0f,0.0f);
        }

        // No react to collisions 
        _rigidbody.angularVelocity = Vector3.zero;     // The angular velocity vector of the rigidbody 
        _rigidbody.velocity = Vector3.zero;         // The velocity vector of the rigidbody.
        _rigidbody.freezeRotation = true;
        // boundaryCheck(this.transform.localPosition);
    }

    void boundaryCheck(Vector3 cameraPosition)
    {
    }
}