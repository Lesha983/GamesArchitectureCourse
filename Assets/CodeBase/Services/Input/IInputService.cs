﻿using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Services
{
	public interface IInputService
	{
		Vector2 Axis { get; }

		bool IsAttackButtonUp();
	}
}