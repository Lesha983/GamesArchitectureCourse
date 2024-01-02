using System;
using UnityEngine;

namespace CodeBase.Data
{
	[Serializable]
	public class PositionOnLevel
	{
		public string Level;
		public Vector3Data Position
		{
			get
			{
				if (X == 0f && Y == 0f && Z == 0f)
					return null;
				return new Vector3Data(X, Y, Z);
			}

			set
			{
				X = value.X;
				Y = value.Y;
				Z = value.Z;
			}
		}

		public float X;
		public float Y;
		public float Z;

		public PositionOnLevel(string initialLevel)
		{
			Level = initialLevel;
		}

		public PositionOnLevel(string level, Vector3Data position)
		{
			Level = level;
			Position = position;
		}
	}
}