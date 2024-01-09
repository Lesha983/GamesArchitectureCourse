using System;
using UnityEngine;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(Attack))]
	public class CheckAttackRange : MonoBehaviour
	{
		[SerializeField] private Attack attack;
		[SerializeField] private TriggerObserver observer;

		private void Start()
		{
			observer.TriggerEnter += TriggerEnter;
			observer.TriggerExit += TriggerExit;

			attack.DisableAttack();
		}

		private void TriggerEnter(Collider collider)
		{
			attack.EnableAttack();
		}

		private void TriggerExit(Collider collider)
		{
			attack.DisableAttack();
		}
	}
}