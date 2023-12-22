using UnityEngine;

namespace CodeBase.Services
{
	public class StandaloneInputService : InputService
	{
		public override Vector2 Axis
		{
			get
			{
				Vector2 axis = GetSimpleInputAxis();

				if (axis == Vector2.zero)
					axis = GetUnityAxis();
				return axis;
			}
		}

		private static Vector2 GetUnityAxis() =>
			new Vector2(UnityEngine.Input.GetAxis(_horizontal), Input.GetAxis(_vertical));
	}
}

