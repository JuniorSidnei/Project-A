using System;
using System.Collections;
using System.Collections.Generic;
using ProjectA.Input;
using ProjectA.Movement;
using UnityEngine;

namespace ProjectA.Attack {
    
    public class PlayerAttack : MonoBehaviour {
        
        public InputManager InputManager;
        public PlayerMovement PlayerMovement;
        public float TimeToChargedAttack;
        
        [SerializeField]
        private float m_elapsedtimeCharged;
        private bool m_isChargingAttack;
        private float m_chargedAttackTreshold = .5f;
        
        private void Start() {
            InputManager.Attack.performed += ctx => StartAttack();
            InputManager.Attack.canceled += ctx => Attack();
        }

        private void StartAttack() {
            m_isChargingAttack = true;
        }

        private void Attack() {
            if (m_elapsedtimeCharged <= m_chargedAttackTreshold) {
                PlayerMovement.State = PlayerMovement.PlayerStates.ATTACK;
            } else if (m_elapsedtimeCharged > m_chargedAttackTreshold && m_elapsedtimeCharged > TimeToChargedAttack) {
                PlayerMovement.State = PlayerMovement.PlayerStates.CHARGEDATTACK;
            }

            m_elapsedtimeCharged = 0;
            m_isChargingAttack = false;
        }
        
        private void Update() {
            if (!m_isChargingAttack) return;

            m_elapsedtimeCharged += Time.deltaTime;
        }
    }
}