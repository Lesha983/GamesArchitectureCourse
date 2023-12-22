using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Services
{
	public abstract class InputService : IInputService
	{
		protected const string _horizontal = "Horizontal";
		protected const string _vertical = "Vertical";
		private const string _button = "Fire";

		public abstract Vector2 Axis { get; }

		public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(_button);

		protected static Vector2 GetSimpleInputAxis() =>
			new Vector2(SimpleInput.GetAxis(_horizontal), SimpleInput.GetAxis(_vertical));
	}
}

