using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Tooltip("The jump force to be applied to the object when 'space' is pressed")]
    public float jumpForce = 20.0f;
    public float movementSpeed= 20.0f;

    
    // SerializeField allows var to be visible in Unity's Inspector
    [SerializeField] private bool isGrounded;


    Rigidbody rb;


    // Use this for initialization  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    /// <summary>
    /// Translates object's transform based on wasd key press.
    /// </summary>
    void MoveObject()
    {
        float stepSize = 0.05f;
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("Move w");
            transform.Translate(stepSize * movementSpeed , 0f, 0f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Move s");
            transform.Translate(-stepSize * movementSpeed, 0f, 0f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Move d");
            transform.Translate(0.0f, 0f, -stepSize * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Move a");
            transform.Translate(0.0f, 0f, stepSize * movementSpeed);
        }
    }

    /// <summary>
    /// If Space pressed and object is on the floor, then allow the obj to jump
    /// </summary>
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("<color=red>Jump</color>");
            rb.AddForce(
                force: new Vector3(0.0f, 2.0f, 0.0f) * jumpForce,
                mode: ForceMode.Impulse
            );
            isGrounded = false;
        }
    }


    void Update()
    {
        MoveObject();
        Jump();
    }
}