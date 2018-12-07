using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TopDown
{
    [RequireComponent(typeof(CharacterController))]
    public class BossMovement : MonoBehaviour
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
        public float speed = .05f, sum = 0.05f, DistanceTraveled, DistanceAvailable;
        public Vector3 tempVector;
        public GameObject Player;
        public bool _canAttack = true;

        private Plane _groundPlane;

        private Rigidbody rb;
        void Start()
        {
            sum = speed;
            rb = GetComponent<Rigidbody>();
            _groundPlane = new Plane(Vector3.up, this.transform.position);
            //DistanceTraveled = 10f;
            //DistanceAvailable = 20f;
            Player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (_canAttack)
                Attack();
            moveEnemy();
        }

        private void Attack()
        {
            if (primaryAttack == null) return;
            primaryAttack.Fire(attackPoint);
        }

        private void moveEnemy()
        {
            DistanceTraveled += sum;
            if (DistanceTraveled < DistanceAvailable)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
            else
            {
                DistanceTraveled = 0;
                speed *= -1;
            }
        }
    }
}
