﻿using System;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class RotateToHero : Follow
	{
		[SerializeField] private float speed;

		private Transform _heroTransform;
		private Vector3 _positionToLook;
		
		public void Constract(Transform heroTransform) => 
			_heroTransform = heroTransform;

		private void Update()
		{
			if (!_heroTransform)
				return;

			RotateTowardsHero();
		}

		private void RotateTowardsHero()
		{
			UpdatePositionToLookAt();

			transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
		}

		private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
			Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

		private Quaternion TargetRotation(Vector3 positionToLook) =>
			Quaternion.LookRotation(positionToLook);

		private float SpeedFactor() =>
			speed * Time.deltaTime;

		private void UpdatePositionToLookAt()
		{
			var positionDiff = _heroTransform.position - transform.position;
			_positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
		}
	}
}