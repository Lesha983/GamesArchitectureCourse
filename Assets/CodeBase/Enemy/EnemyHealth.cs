using System;
using CodeBase.Logic;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class EnemyHealth : MonoBehaviour, IHealth
	{
		public EnemyAnimator Animator;

		[SerializeField]
		private float current;
		[SerializeField]
		private float max;

		public float Current { get => current; set => current = value; }
		public float Max { get => max; set => max = value; }

		public event Action HealthChanged;

		public void TakeDamage(float damage)
		{
			Current -= damage;

			Animator.PlayHit();

			HealthChanged?.Invoke();
		}

		private void Awake()
		{
			GetComponentInChildren<ActorUI>().Constract(this);
		}
	}
}