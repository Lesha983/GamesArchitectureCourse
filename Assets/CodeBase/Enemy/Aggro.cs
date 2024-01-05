using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class Aggro : MonoBehaviour
	{
		[SerializeField] private TriggerObserver observer;
		[SerializeField] private Follow follow;
		[SerializeField] private float cooldown = 3f;
		private bool _hasAggroTarget;

		private void Start()
		{
			observer.TriggerEnter += TriggerEnter;
			observer.TriggerExit += TriggerExit;

			SwitchFollowOff();
		}

		private void OnDisable()
		{
			observer.TriggerEnter -= TriggerEnter;
			observer.TriggerExit -= TriggerExit;
		}

		private void TriggerEnter(Collider collider)
		{
			if (_hasAggroTarget)
				return;

			_hasAggroTarget = true;
			StopCoroutine(nameof(AutoSwitchFollow));
			SwitchFollowOn();
		}

		private void TriggerExit(Collider collider)
		{
			if (!_hasAggroTarget)
				return;

			_hasAggroTarget = false;
			StartCoroutine(nameof(AutoSwitchFollow));
		}

		private IEnumerator AutoSwitchFollow()
		{
			yield return new WaitForSeconds(cooldown);
			SwitchFollowOff();
		}

		private void SwitchFollowOn() =>
			follow.enabled = true;

		private void SwitchFollowOff() =>
			follow.enabled = false;
	}
}