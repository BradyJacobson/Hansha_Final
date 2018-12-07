using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [RequireComponent(typeof(CharacterController))]
    public class Enemy_Pattern_Move : MonoBehaviour
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

        [Header("Attack Properties")]
        public Weapon primaryAttack;
        public Transform attackPoint;
        public float speed = .05f,tempDirection,limit;
        public Vector3 tempVector;
        public GameObject Player;

        private bool _canAttack = true;

        private Plane _groundPlane;

        private FollowGuyEnemyAnimationScript animScript;

        private bool _canMove = true;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            _groundPlane = new Plane(Vector3.up, this.transform.position);
            animScript = GetComponent<FollowGuyEnemyAnimationScript>();
            Player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            //tempVector = transform.position - Player.transform.position;
            //tempDirection = Mathf.Sqrt(Mathf.Pow(tempVector.x, 2) + Mathf.Pow(tempVector.z, 2));
            //if (tempDirection <= limit)
            //{
            //    if (_canAttack) Attack();
            //}
            if(_canMove)
            {
                moveEnemy();
            }
            if (_canAttack)
            {
                Attack();
            }
        }  

        private void Attack()
        {
            if (primaryAttack == null)
                return;
            primaryAttack.Fire(attackPoint);

        }

        /// <summary>
        /// Freeze the character in place, store the current character velocity, or unfreeze the character and resume character velocity.
        /// </summary>
        /// <param name="value">If set to <c>true</c> value.</param>
        public void Freeze(bool value)
        {
            _canMove = !value;
            _canAttack = !value;

            if (value)
            {
                // _storedVelocity = _characterController.velocity;
                speed = 0f;
            }
            else
            {
                // _characterVelocity = _storedVelocity;

            }
        }

        private void moveEnemy()
        {
            tempVector = transform.position - Player.transform.position;
            tempDirection = Mathf.Sqrt(Mathf.Pow(tempVector.x, 2) + Mathf.Pow(tempVector.z, 2));
            if (tempDirection <= limit)
            {
                animScript.isRunning = true;
                if (tempVector.x < 1 && tempVector.x > -1)
                {
                    transform.position += new Vector3(0, 0, 0);
                }
               else if (tempVector.x > 0)
                {
                    transform.position -= new Vector3(speed, 0, 0);
                }
                else if (tempVector.x < 0)
                {
                    transform.position += new Vector3(speed, 0, 0);
                }
                if (tempVector.z < 1 && tempVector.z > -1)
                {
                    transform.position += new Vector3(0, 0, 0);
                }
                else if (tempVector.z > 0)
                {
                    transform.position -= new Vector3(0, 0, speed);
                }
                else if (tempVector.z < 0)
                {
                    transform.position += new Vector3(0, 0, speed);
                }
            }
            else
            {
               animScript.isRunning = false;
            }

        }
    }
}
