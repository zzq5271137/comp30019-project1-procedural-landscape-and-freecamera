using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunScript : MonoBehaviour
{
    public Color color;

    public int speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(1500f, 2000f, 1500f);
        this.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(1500f,0,1500f), Vector3.right,
            speed * Time.deltaTime);
    }

    public Vector3 getPosition()
    {
        return this.transform.position;
    }
}
