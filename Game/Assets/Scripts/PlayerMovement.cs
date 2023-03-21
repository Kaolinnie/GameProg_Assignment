using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private readonly int WalkSpeed = 5;
    private readonly int RunSpeed = 8;
    private readonly float Gravity = -9.81f;
    private readonly float JumpHeight = 1.0f;
    private readonly float MouseSensitivity = 5.0f;
    
    private bool _groundedPlayer;
    private Vector3 _gravity;
    private Vector3 _playerVelocity;
    
    private CharacterController _controller;
    private Animator _animator;

    private static readonly int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    // private static readonly trigger Jump = Animator.StringToHash("Jump");
    // private static readonly trigger DoubleJump = Animator.StringToHash("DoubleJump");
    // private static readonly bool HasDoubleJumped = Animator.StringToHash("HasDoubleJumped");


    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
    }
    
    
    
    private void ProcessMovement()
    { 
        int speed = GetMovementSpeed();

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        _animator.SetInteger(PlayerSpeed,move.magnitude==0?0:speed);

        Debug.Log(_animator.GetInteger(PlayerSpeed));
        
        
        // Making sure we dont have a Y velocity if we are grounded
        // controller.isGrounded tells you if a character is grounded ( IE Touches the ground)
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer)
        {
            if (Input.GetButtonDown("Jump") )
            {
                _animator.SetTrigger("Jump");
                _gravity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
            }
            else 
            {
                // Dont apply gravity if grounded and not jumping
                _gravity.y = -1.0f;
            }
        }
        else 
        {
            // Since there is no physics applied on character controller we have this applies to reapply gravity
            _gravity.y += Gravity * Time.deltaTime;
        }  
        Vector3 movement = move.z *transform.forward  + move.x * transform.right;
                
        _playerVelocity = _gravity * Time.deltaTime + movement * (Time.deltaTime * speed);
        _controller.Move(_playerVelocity);
    }

    private int GetMovementSpeed() =>Input.GetButton("Fire3") ? // Left shift
            RunSpeed : WalkSpeed;
    
}
