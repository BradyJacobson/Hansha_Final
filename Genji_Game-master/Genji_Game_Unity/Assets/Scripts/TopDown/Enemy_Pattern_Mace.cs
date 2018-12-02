using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    [RequireComponent(typeof(CharacterController))]
    public class Enemy_Pattern_Mace : MonoBehaviour
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

        private bool _canAim = true;
        private bool _canAttack = true;

        private Vector3 _storedVelocity = Vector3.zero;

        private CharacterState state = CharacterState.idle;

        private Plane _groundPlane;

        void Start()
        {
            _characterController = this.GetComponent<CharacterController>();
            _groundPlane = new Plane(Vector3.up, this.transform.position);
        }

        private void Update()
        {
            if (_canAttack) Attack();
        }

        private void Attack()
        {
            if (primaryAttack == null)
                return;
            primaryAttack.Fire(attackPoint);

        }
    }
}
