using UnityEngine;

namespace CodeBase.Services
{
	public class MobileInputService : InputService
	{
		public override Vector2 Axis => GetSimpleInputAxis();
	}
}

