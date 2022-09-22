using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public EnemyHealth Health;
        public EnemyAnimator Animator;

        public GameObject DeathFX;

        public event Action Happened;

        private void Start() =>
            Health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (Health.Current <= 0)
                Die();
        }

        private void Die()
        {
            Health.HealthChanged -= HealthChanged;

            Animator.PlayDeath();

            SpawnDeathFX();
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

        private void SpawnDeathFX()
        {
            Instantiate(DeathFX, transform.position, Quaternion.identity);
        }
    }
}