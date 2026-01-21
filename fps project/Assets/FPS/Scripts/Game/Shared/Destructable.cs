﻿using UnityEngine;
using System.Collections;

namespace Unity.FPS.Game
{
    public class Destructable : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float destroyDelay = 10f;

        private Coroutine deathCo;
        private Health m_Health;
        private bool isDead;

        private void Awake()
        {
            m_Health = GetComponent<Health>();
            DebugUtility.HandleErrorIfNullGetComponent<Health, Destructable>(m_Health, this, gameObject);

            if (!animator) animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            if (!m_Health) return;
            m_Health.OnDie += OnDie;
            m_Health.OnDamaged += OnDamaged;
        }

        private void OnDisable()
        {
            if (m_Health == null) return;
            m_Health.OnDie -= OnDie;
            m_Health.OnDamaged -= OnDamaged;

            if (deathCo != null) StopCoroutine(deathCo);
            deathCo = null;
        }

        private void OnDamaged(float damage, GameObject damageSource)
        {
            // TODO: damage reaction
        }

        private void OnDie()
        {
            if (isDead) return;
            isDead = true;

            if (animator) animator.SetTrigger("Dead");

            // 즉시 파괴하지 말고, 지연 후 파괴
            if (deathCo != null) StopCoroutine(deathCo);
            deathCo = StartCoroutine(CoDestroyAfter(destroyDelay));
        }

        private IEnumerator CoDestroyAfter(float sec)
        {
            yield return new WaitForSeconds(sec);
            Destroy(gameObject);
        }
    }
}
