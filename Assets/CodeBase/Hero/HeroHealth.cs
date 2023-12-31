using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
	[RequireComponent(typeof(HeroAnimator))]
	public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
	{
		[SerializeField] private HeroAnimator animator;

		private State _state;

		public event Action HealthChanged;

		public float Current
		{
			get => _state.CurrentHP;
			private set
			{
				if (_state.CurrentHP != value)
				{
					_state.CurrentHP = value;
					HealthChanged?.Invoke();
				}
			}
		}

		public float Max
		{
			get => _state.MaxHP;
			private set => _state.MaxHP = value;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_state = progress.HeroState;
			HealthChanged?.Invoke();
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.HeroState.CurrentHP = Current;
		}

		public void TakeDamage(float damage)
		{
			if (Current <= 0f)
				return;

			Current -= damage;
			animator.PlayHit();
		}
	}
}