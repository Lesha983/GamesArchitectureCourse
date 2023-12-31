using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI
{
	public class LookAtCamera : MonoBehaviour
	{
		private Camera _mainCamera;

		private void Start() =>
			_mainCamera = Camera.main;

		private void Update()
		{
			var rotation = _mainCamera.transform.rotation;
			transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
		}

	}
}