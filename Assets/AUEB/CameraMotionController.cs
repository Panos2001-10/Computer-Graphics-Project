using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionController : MonoBehaviour
{
    float mainSpeed = 2.0f; //regular speed
    float camSens = 0.3f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.A)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // input from mouse
        Vector3 curpos = Input.mousePosition - lastMouse;
        curpos = new Vector3(-curpos.y * camSens, curpos.x * camSens, 0);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + curpos.x, transform.eulerAngles.y + curpos.y, 0);
        lastMouse = Input.mousePosition;
        
        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        p = p * mainSpeed * Time.deltaTime;
      
        transform.Translate(p);
        
    }
}
