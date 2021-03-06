﻿using UnityEngine;
using System.Collections;

namespace TopDown
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement_Direct : MonoBehaviour
    {

        public enum AimType
        {
            Movement,
            Mouse,
            Thumbstick,
            Vehicle,
        }

        public enum CharacterState
        {
            frozen,
            idle,
            moving
        }

        [Header("Input Axes")]
        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";
        public string horizontalThumbstick = "Horizontal_Thumbstick";
        public string verticalThumbstick = "Vertical_Thumbstick";
        public string attackAxis = "Fire1";

        [Header("Movment Properties")]
        public float maxSpeed = 10f;
        public float acceleration = 2f;
        [Range(0f, 1f)]
        public float frictionCoefficient = 0.85f;
        public float massCoeficcient = 0.85f;

        [Header("Aim Properties")]
        public AimType aimType = AimType.Movement;
        public float angularSpeed = 5f;
        public Camera MainCamera;

        [Header("Attack Properties")]
        public Weapon primaryAttack;
        public Transform attackPoint;


        //Private Memeber Variables
        private CharacterController _characterController;
        private Vector3 _characterVelocity = Vector3.zero;
        private Vector3 _thumbstickVector = Vector3.zero;
        private Vector3 _force = Vector3.zero;

        private bool _canMove = true;
        private bool _canAim = true;
        private bool _canAttack = true;

        private Vector3 _storedVelocity = Vector3.zero;

        private CharacterState state = CharacterState.idle;

        private Plane _groundPlane;

        private AnimationScript _animationScript;
        private PlayerHP _playerHPScript;
        private Vector3 lastPosition = Vector3.zero;


        void Start()
        {
            _animationScript = GetComponent<AnimationScript>();
            _playerHPScript = GetComponent<PlayerHP>();
            _animationScript.speed = 0;

            _characterController = GetComponent<CharacterController>();
            _groundPlane = new Plane(Vector3.up, this.transform.position);
        }

        private void Update()
        {
            if (_canAttack) Attack();
            if (_canMove) Move();
            if (_canAim) Aim();

            _characterVelocity *= frictionCoefficient;
            _force *= massCoeficcient;
            _characterController.Move((_characterVelocity + _force) * Time.deltaTime);

            _animationScript.speed = (transform.position - lastPosition).magnitude * 100;
            lastPosition = transform.position;

            _animationScript.currentHealth = _playerHPScript._currentHealth;

        }

        /// <summary>
        /// Handles the basic movement input, sets the character velocity local variable.
        /// </summary>
        private void Move()
        {
            if (_characterVelocity.magnitude < maxSpeed)
            {
                // Initialize a local force vector variable
                Vector3 forceVector = Vector3.zero;

                // Add the input from the Input Manager to the X & Z axes of the forceVector
                forceVector.x = Input.GetAxis(horizontalAxis) * acceleration;
                forceVector.z = Input.GetAxis(verticalAxis) * acceleration;

                // If the aim type has been set to vehicle movement add the forward vector of character times the force to the character velocity, 
                // otherwise just add the force vector
                if (aimType == AimType.Vehicle)
                {
                    _characterVelocity += (this.transform.forward * forceVector.z);
                }
                else
                {
                    _characterVelocity += forceVector;
                }

                if (!_characterController.isGrounded) _characterVelocity.y = Physics.gravity.y;
            }
        }

        /// <summary>
        /// Switch case to determine which type of aim to use to orient the character.
        /// </summary>
        private void Aim()
        {
            switch (aimType)
            {
                case AimType.Movement:
                    Vector3 lookDirection = _characterVelocity;
                    lookDirection.y = 0f;
                    this.transform.forward = lookDirection;
                    break;

                case AimType.Vehicle:
                    this.transform.Rotate(Vector3.up, Input.GetAxis(horizontalAxis) * angularSpeed);
                    break;

                case AimType.Mouse:
                    MouseAim();
                    break;

                case AimType.Thumbstick:
                    ThumbstickAim();
                    break;
            }
        }

        /// <summary>
        /// Uses a plane based raycast to orient the player toward the mouse position on screen.
        /// </summary>
        private void MouseAim()
        {
            Ray screenRay = MainCamera.ScreenPointToRay(Input.mousePosition);
            float intersection = 0.0f;

            // Set the raycast plane to the position of the player facing up
            _groundPlane.SetNormalAndPosition(Vector3.up, this.transform.position);

            // Perform a raycast to track the intersection distance of the ray
            if (_groundPlane.Raycast(screenRay, out intersection))
            {
                // Calculate the hit point on the plane and set the look at of the character transform
                Vector3 hitPoint = screenRay.GetPoint(intersection);
                this.transform.LookAt(hitPoint);
            }
        }

        /// <summary>
        /// Handles the aim from the thumbstick input & sets the forward transform vector of the character.
        /// </summary>
        private void ThumbstickAim()
        {
            // Set the thumbstick vector from input axes
            _thumbstickVector.x = Input.GetAxis(horizontalThumbstick);
            _thumbstickVector.z = Input.GetAxis(verticalThumbstick);

            // Set the aim only if the magnitude of the vector is significant, avoids thumbstick drift 
            if (_thumbstickVector.magnitude > 0.1f)
            {
                this.transform.forward = _thumbstickVector;
            }
        }


        private void Attack()
        {
            if (primaryAttack == null) return;
            if (Input.GetAxis(attackAxis) > 0.5f)
            {
                primaryAttack.Fire(attackPoint);
            }
        }

        /// <summary>
        /// Freeze the character in place, store the current character velocity, or unfreeze the character and resume character velocity.
        /// </summary>
        /// <param name="value">If set to <c>true</c> value.</param>
        public void Freeze(bool value)
        {
            _canMove = !value;
            _canAim = !value;

            if (value)
            {
               // _storedVelocity = _characterController.velocity;
                _characterVelocity = Vector3.zero;
            }
            else
            {
               // _characterVelocity = _storedVelocity;

            }
        }

    }
}

