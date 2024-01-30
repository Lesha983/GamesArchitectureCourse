using System.Linq;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(EnemyAnimator))]
	public class Attack : MonoBehaviour
	{
		[SerializeField] private EnemyAnimator animator;
		[SerializeField] private float attackCooldown = 3f;
		[SerializeField] private float cleavage = 0.5f;
		[SerializeField] private float effectiveDistance = 0.5f;

		public float Damage { get; set; }
		public float Cleavage { get; set; }
		public float EffectiveDistance { get; set; }

		private Transform _heroTransform;
		private float _attackCooldown;
		private bool _isAttacking;
		private int _layerMask;
		private Collider[] _hits = new Collider[1];
		private Vector3 _offset = new Vector3(0f, 0.5f, 0f);
		private bool _attackIsActive;

		public void DisableAttack() =>
			_attackIsActive = false;

		public void EnableAttack() =>
			_attackIsActive = true;

		public void Constract(Transform heroTransform) => 
			_heroTransform = heroTransform;

		private void Awake()
		{
			_layerMask = 1 << LayerMask.NameToLayer("Player");
		}

		private void Update()
		{
			UpdateCooldown();

			if (CanAttack())
				StartAttack();
		}

		private void OnAttack()
		{
			if (Hit(out Collider hit))
			{
				PhysicsDebug.DrawDebug(StartPoint(), cleavage, 1f);
				hit.transform.GetComponent<Logic.IHealth>().TakeDamage(Damage);
			}
		}

		private void OnAttackEnded()
		{
			_attackCooldown = attackCooldown;
			_isAttacking = false;
		}

		private bool Hit(out Collider hit)
		{
			var hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), cleavage, _hits, _layerMask);
			hit = _hits.FirstOrDefault();

			return hitsCount > 0;
		}

		private Vector3 StartPoint() =>
			transform.position + _offset + transform.forward * effectiveDistance;

		private void UpdateCooldown()
		{
			if (!CooldownIsUp())
				_attackCooldown -= Time.deltaTime;
		}

		private void StartAttack()
		{
			transform.LookAt(_heroTransform);
			animator.PlayAttack();

			_isAttacking = true;
		}

		private bool CanAttack() =>
			_attackIsActive && !_isAttacking && CooldownIsUp();

		private bool CooldownIsUp() =>
			_attackCooldown <= 0f;

	}
}