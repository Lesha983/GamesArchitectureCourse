using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth))]
	public class EnemyDeath : MonoBehaviour
	{
		public EnemyAnimator Animator;
		public EnemyHealth Health;

		public GameObject DeathFxPrefab;

		public event Action Happened;

		private void Start()
		{
			Health.HealthChanged += HealthChanged;
		}

		private void OnDestroy()
		{
			Health.HealthChanged -= HealthChanged;
		}

		private void HealthChanged()
		{
			if (Health.Current <= 0f)
				Die();
		}

		private void Die()
		{
			Health.HealthChanged -= HealthChanged;
			Animator.PlayDeath();
			SpawnDeathFx();
			Happened?.Invoke();
			StartCoroutine(nameof(DestroyTimer));
		}

		private void SpawnDeathFx()
		{
			Instantiate(DeathFxPrefab, transform.position, Quaternion.identity);
		}

		private IEnumerator DestroyTimer()
		{
			yield return new WaitForSeconds(3f);
			Destroy(gameObject);
		}
	}
}