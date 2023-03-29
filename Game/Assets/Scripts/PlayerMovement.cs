using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    private readonly int WalkSpeed = 5;
    private readonly int RunSpeed = 8;
    private readonly float Gravity = -9.81f;
    private readonly float JumpHeight = 2.0f;

    private bool canDoubleJump;
    
    private bool _groundedPlayer;
    private Vector3 _gravity;
    private Vector3 _playerVelocity;
    
    private CharacterController _controller;
    private Animator _animator;

    private GameManager _instance;
    
    private static readonly int PlayerSpeed = Animator.StringToHash("PlayerSpeed");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int DoubleJump = Animator.StringToHash("DoubleJump");

    public Text scoreText;

    public ParticleSystem activeJump;
    public ParticleSystem doubleJump;


    // Start is called before the first frame update
    void Start() {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _instance = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        _instance.DisplayUI(scoreText);
        ProcessMovement();
        DeathPlane();
        particles();
    }

    private void particles()
    {
        activeJump.gameObject.SetActive(_instance.hasDoubleJump);
    }
    
    private void ProcessMovement()
    { 
        int speed = GetMovementSpeed();

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        _animator.SetInteger(PlayerSpeed,move.magnitude==0?0:speed);
        
        // Making sure we dont have a Y velocity if we are grounded
        // controller.isGrounded tells you if a character is grounded ( IE Touches the ground)
        _groundedPlayer = _controller.isGrounded;
        _animator.SetBool(IsGrounded,_groundedPlayer);
        if (_groundedPlayer) canDoubleJump = true;
        
        JumpUp();

        Vector3 movement = move.z *transform.forward  + move.x * transform.right;
                
        _playerVelocity = _gravity * Time.deltaTime + movement * (Time.deltaTime * speed);
        _controller.Move(_playerVelocity);
    }

    private int GetMovementSpeed() =>Input.GetButton("Fire3") ? // Left shift
            RunSpeed : WalkSpeed;

    // ReSharper disable Unity.PerformanceAnalysis
    private void JumpUp()
    {
        _gravity.y += Gravity * Time.deltaTime;

        if (!Input.GetButtonDown("Jump"))
        {
            if(_groundedPlayer) _gravity.y = -1.0f;
            return;
        }

        if (!canDoubleJump) return;
        
        if (_groundedPlayer)
            _animator.SetTrigger(Jump);
        else
        {
            if (!_instance.hasDoubleJump) return;
            canDoubleJump = false;
            _instance.hasDoubleJump = false;
            _animator.SetTrigger(DoubleJump);
            doubleJump.Play();
        }

        _gravity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
    }
    private void DeathPlane()
    {
        if (transform.position.y < -5)
        {
            _instance.RestartLevel();
        }
    }
}