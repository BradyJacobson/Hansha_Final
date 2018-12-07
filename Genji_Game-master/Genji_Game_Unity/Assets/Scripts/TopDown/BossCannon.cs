using UnityEngine;
using System.Collections;

namespace TopDown
{
    [RequireComponent(typeof(CharacterController))]
    public class BossCannon : MonoBehaviour
    {

        public enum AimType
        {
            Movement,
            Mouse,
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


        //Private Memeber Variables
        private CharacterController _characterController;
        private Vector3 _characterVelocity = Vector3.zero;
        private Vector3 _thumbstickVector = Vector3.zero;
        private Vector3 _force = Vector3.zero;
        private Vector3 tempVector;
        private float tempDirection;
        public float limit;
        private bool _canAim = true;
        public bool _canAttack = true;
        private bool _canMove = true;

        public GameObject Player;
        private Vector3 _storedVelocity = Vector3.zero;

        private CharacterState state = CharacterState.idle;

        private Follow_Script followScript;

        private Plane _groundPlane;

        void Start()
        {
            _characterController = this.GetComponent<CharacterController>();
            _groundPlane = new Plane(Vector3.up, this.transform.position);
            limit = 20f;
            Player = GameObject.FindWithTag("Player");
            followScript = GetComponent<Follow_Script>();
        }

        private void Update()
        {
            if (_canMove)
            {
                tempVector = transform.position - Player.transform.position;
                tempDirection = Mathf.Sqrt(Mathf.Pow(tempVector.x, 2) + Mathf.Pow(tempVector.z, 2));
                if (tempDirection <= limit)
                {
                    if (_canAttack)
                    {
                        Attack();
                    }
                }
            }
        }

        private void Attack()
        {
            if (primaryAttack == null) return;

            primaryAttack.Fire(attackPoint);
        }

        /// <summary>
        /// Freeze the character in place, store the current character velocity, or unfreeze the character and resume character velocity.
        /// </summary>
        /// <param name="value">If set to <c>true</c> value.</param>
        public void Freeze(bool value)
        {
            _canMove = !value;
            _canAim = !value;
            _canAttack = !value;

            if (value)
            {
                // _storedVelocity = _characterController.velocity;
                followScript.target = null;
                _characterVelocity = Vector3.zero;
            }
            else
            {
                // _characterVelocity = _storedVelocity;

            }
        }
    }
}

