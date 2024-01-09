using CodeBase.Hero;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI
{
	public class ActorUI : MonoBehaviour
	{
		public HPBar hpBar;

		private IHealth _health;

		private void OnDestroy() =>
			_health.HealthChanged -= UpdateHpBar;

		public void Constract(IHealth health)
		{
			_health = health;
			_health.HealthChanged += UpdateHpBar;
		}

		private void UpdateHpBar()
		{
			hpBar.SetValue(_health.Current, _health.Max);
		}
	}
}