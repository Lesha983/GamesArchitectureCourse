using UnityEngine;
using System.Collections;
using System;

namespace CodeBase.Enemy
{
	[RequireComponent(typeof(Collider))]
	public class TriggerObserver : MonoBehaviour
	{
		public event Action<Collider> TriggerEnter;
		public event Action<Collider> TriggerExit;

		private void OnTriggerEnter(Collider other) =>
			TriggerEnter?.Invoke(other);

		private void OnTriggerExit(Collider other) =>
			TriggerExit?.Invoke(other);
	}
}