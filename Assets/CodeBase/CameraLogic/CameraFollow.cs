using UnityEngine;
using System.Collections;

using SF = UnityEngine.SerializeField;

namespace CodeBase.CameraLogic
{
	public class CameraFollow : MonoBehaviour
	{
		[SF] private float RotationAngleX;
		[SF] private int Distance;
		[SF] private float OffsetY;

		[SF] private Transform _following;

		private void LateUpdate()
		{
			if (_following == null)
				return;

			var rotation = Quaternion.Euler(RotationAngleX, 0f, 0f);

			var position = rotation * new Vector3(0f, 0f, -Distance) + GetFollowingPointPosition();

			transform.position = position;
			transform.rotation = rotation;
		}

		private Vector3 GetFollowingPointPosition()
		{
			Vector3 followingPosition = _following.position;
			followingPosition.y += OffsetY;
			return followingPosition;
		}

		public void Follow(Transform following) => _following = following;
	}
}
