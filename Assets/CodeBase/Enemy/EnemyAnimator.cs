using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
	{
		private readonly int Attack = Animator.StringToHash("Attack_1");
		private readonly int Speed = Animator.StringToHash("Speed");
		private readonly int IsMoving = Animator.StringToHash("IsMoving");
		private readonly int Hit = Animator.StringToHash("Hit");
		private readonly int Die = Animator.StringToHash("Die");

		private readonly int _idleStateHash = Animator.StringToHash("idle");
		private readonly int _attackStateHash = Animator.StringToHash("attack01");
		private readonly int _walkingStateHash = Animator.StringToHash("Move");
		private readonly int _deathStateHash = Animator.StringToHash("die");

		private Animator _animator;

		public event Action<AnimatorState> StateEntered;
		public event Action<AnimatorState> StateExited;

		public AnimatorState State { get; private set; }

		public void PlayHit() => _animator.SetTrigger(Hit);
		public void PlayDeath() => _animator.SetTrigger(Die);

		public void Move(float speed)
		{
			_animator.SetBool(IsMoving, true);
			_animator.SetFloat(Speed, speed);
		}

		public void StopMoving() => _animator.SetBool(IsMoving, false);
		public void PlayAttack() => _animator.SetTrigger(Attack);

		public void EnteredState(int stateHash)
		{
			State = StateFor(stateHash);
			StateEntered?.Invoke(State);
		}

		public void ExitedState(int stateHash) =>
			StateExited?.Invoke(StateFor(stateHash));

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private AnimatorState StateFor(int stateHash)
		{
			if (stateHash == _idleStateHash)
				return AnimatorState.Idle;
			if (stateHash == _attackStateHash)
				return AnimatorState.Attack;
			if (stateHash == _walkingStateHash)
				return AnimatorState.Walking;
			if (stateHash == _deathStateHash)
				return AnimatorState.Died;

			return AnimatorState.Unknown;
		}
	}
}