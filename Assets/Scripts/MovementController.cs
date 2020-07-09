using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

internal class KeyMapper
{
    private readonly Dictionary<KeyCode, Direction> _keys;
    public List<KeyCode> movementKeys;

    public KeyMapper()
    {
        _keys = new Dictionary<KeyCode, Direction>
        {
            [KeyCode.UpArrow] = Direction.Up, [KeyCode.W] = Direction.Up,
            [KeyCode.DownArrow] = Direction.Down, [KeyCode.S] = Direction.Down,
            [KeyCode.LeftArrow] = Direction.Left, [KeyCode.A] = Direction.Left,
            [KeyCode.RightArrow] = Direction.Right, [KeyCode.D] = Direction.Right,
        };

        movementKeys = new List<KeyCode>(_keys.Keys);
    }

    public Direction GetDirection(KeyCode k)
    {
        return _keys[k];
    }
}


public class MovementController : MonoBehaviour
{
    [Tooltip("The jump force to be applied to the object when 'space' is pressed")]
    public float jumpForce = 15.0f;

    [Tooltip("The movement speed of the object")]
    public float movementSpeed = 1.0f;


    // SerializeField allows var to be visible in Unity's Inspector
    [SerializeField] private bool isGrounded;

    private KeyMapper keyMapper;
    Rigidbody rb;


    // Use this for initialization  
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        keyMapper = new KeyMapper();
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        ParseInput();
    }

    /// <summary>
    ///  called once per frame for every collider/rigidbody that is touching rigidbody/collider.
    /// </summary>
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    /// <summary>
    /// called when this collider/rigidbody has stopped touching another rigidbody/collider.
    /// </summary>
    void OnCollisionExit()
    {
        isGrounded = false;
    }

    /// <summary>
    /// Translates object's transform based on wasd key press.
    /// </summary>
    void ParseInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        foreach (KeyCode k in keyMapper.movementKeys)
        {
            if (Input.GetKey(k))
                Move(keyMapper.GetDirection(k));
        }
    }


    public void Move(Direction d)
    {
        float stepSize = 0.05f;

        if (d is Direction.Up)
        {
            Debug.Log("Move +x");
            transform.Translate(stepSize * movementSpeed, 0f, 0f);
        }

        if (d is Direction.Down)
        {
            Debug.Log("Move -x");
            transform.Translate(-stepSize * movementSpeed, 0f, 0f);
        }

        if (d is Direction.Left)
        {
            Debug.Log("Move +z");
            transform.Translate(0.0f, 0f, +stepSize * movementSpeed);
        }

        if (d is Direction.Right)
        {
            Debug.Log("Move -z");
            transform.Translate(0.0f, 0f, -stepSize * movementSpeed);
        }
    }


    /// <summary>
    /// If object is on the floor, then allow the obj to jump
    /// </summary>
    public void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("<color=red>Jump</color>");
            rb.AddForce(
                force: new Vector3(0.0f, 2.0f, 0.0f) * jumpForce,
                mode: ForceMode.Impulse
            );
            isGrounded = false;
        }
    }
}