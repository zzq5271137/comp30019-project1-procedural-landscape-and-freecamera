using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMove : MonoBehaviour
{
    public ProceduralTerrain Landscape;
    public int mouseSens;

    // height by which the camera is above the terrain in this point

    private int speed = 150;
    private float accelerationDefault = 1;
    private float acceleration;
    private float pitch;
    private float yaw;

    private Rigidbody _Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        WASD();
        MouseControl();
        // force no reaction regarding to colliders
        _Rigidbody.velocity = Vector3.zero;
        _Rigidbody.angularVelocity = Vector3.zero;
        _Rigidbody.freezeRotation = true;
    }

    // update with pressing WASD from keyboards
    void WASD()
    {
        // get the current position of the capsule
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        acceleration = accelerationDefault;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            acceleration = 2f * accelerationDefault;
        }

        if (Input.GetKey(KeyCode.W))
        {
            position += transform.forward * Time.deltaTime * speed * acceleration;
        }

        if (Input.GetKey(KeyCode.S))
        {
            position -= transform.forward * Time.deltaTime * speed * acceleration;
        }

        if (Input.GetKey(KeyCode.A))
        {
            position -= transform.right * Time.deltaTime * speed * acceleration;
        }

        if (Input.GetKey(KeyCode.D))
        {
            position += transform.right * Time.deltaTime * speed * acceleration;
        }


        this.transform.position = position;
    }

    // mouse look refer: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
    void MouseControl()
    {
        Vector3 mouseRotate = transform.rotation.eulerAngles;

        yaw = Input.GetAxis("Mouse X") * mouseSens;
        pitch = Input.GetAxis("Mouse Y") * mouseSens;

        mouseRotate.x -= pitch;
        mouseRotate.y += yaw;
        mouseRotate.z = 0;

        // transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        transform.rotation = Quaternion.Euler(mouseRotate); // deal with >360 degree rotation
    }
}
    
 

    
  
