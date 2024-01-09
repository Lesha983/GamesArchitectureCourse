using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Hero
{
	[RequireComponent(typeof(HeroAnimator), typeof(CharacterController))]
	public class HeroAttack : MonoBehaviour, ISavedProgressReader
	{
		public HeroAnimator HeroAnimator;
		public CharacterController CharacterController;

		private IInputService _inputService;
		private int _layerMask;
		private Collider[] _hits = new Collider[3];
		private Stats _stats;

		public void OnAttack()
		{
			var hitsCount = Hit();
			for (var i = 0; i < hitsCount; i++)
			{
				_hits[i].transform.parent.GetComponent<Logic.IHealth>().TakeDamage(_stats.Damage);
			}
		}

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_layerMask = 1 << LayerMask.NameToLayer("Hittable");
		}

		private void Update()
		{
			if ((_inputService.IsAttackButtonUp() && !HeroAnimator.IsAttacking) ||
				(Input.GetKeyDown("q") && !HeroAnimator.IsAttacking))
				HeroAnimator.PlayAttack();
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_stats = progress.HeroStats;
		}

		private int Hit() =>
			Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

		private Vector3 StartPoint() =>
			new Vector3(transform.position.x, CharacterController.center.y / 2, transform.position.z);
	}
}