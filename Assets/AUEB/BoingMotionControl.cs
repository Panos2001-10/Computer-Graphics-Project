using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoingMotionControl : MonoBehaviour
{
    float speed = 2.0f; 
    float turnSpeed = 100.0f;
    private Animator anim;
    private CapsuleCollider collider;
    
    private Vector3 prev_position;
    private Quaternion prev_rotation;

    private Vector2 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector2 v = new Vector2();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            v += new Vector2(0, 1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            v += new Vector2(0, -1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            v += new Vector2(-1, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v += new Vector2(1, 0);
        }
        return v;
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        collider = gameObject.GetComponent<CapsuleCollider>();
        if (collider == null)
            Debug.Log("Collider is NULL!");
    }

    void OnTriggerEnter(Collider collision)
    {
        transform.position = prev_position;
        transform.rotation = prev_rotation;
    }
    void OnTriggerStay(Collider collision)
    {
        transform.position = prev_position;
        transform.rotation = prev_rotation;
    }


    // Update is called once per frame
    void Update()
    {
        prev_position = transform.position;
        prev_rotation = transform.rotation;

        Vector2 velocity = GetBaseInput();

        anim.SetBool("MoveForward", velocity.y > 0.0f);
        anim.SetBool("MoveBackward", velocity.y < 0.0f);
        anim.SetBool("TurnLeft", velocity.x < 0.0f);
        anim.SetBool("TurnRight", velocity.x > 0.0f);


        transform.eulerAngles = new Vector3(transform.eulerAngles.x,  
                                            transform.eulerAngles.y + turnSpeed * Time.deltaTime * velocity.x, 
                                            transform.eulerAngles.z);
        
        Vector3 offset = speed * Time.deltaTime * (new Vector3(0,0,velocity.y));
        transform.Translate(offset);
        

        //Debug.Log("angle = " + Time.deltaTime * velocity.x);
    }
}
