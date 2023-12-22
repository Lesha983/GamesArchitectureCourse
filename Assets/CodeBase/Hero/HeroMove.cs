using CodeBase.Infrastructure;
using CodeBase.Services;
using UnityEngine;

using SF = UnityEngine.SerializeField;

namespace CodeBase.Hero
{
	public class HeroMove : MonoBehaviour
	{
		[SF] private CharacterController characterController;
		[SF] private float movmentSpeed;
		private IInputService _inputService;
		private Camera _camera;

		private void Awake()
		{
			_inputService = Game.inputService;
		}

		private void Start()
		{
			_camera = Camera.main;
		}

		private void Update()
		{
			var movmentVector = Vector3.zero;
			if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
			{
				movmentVector = _camera.transform.TransformDirection(_inputService.Axis);
				movmentVector.y = 0;
				movmentVector.Normalize();

				transform.forward = movmentVector;
			}

			movmentVector += Physics.gravity;
			characterController.Move(movmentSpeed * movmentVector * Time.deltaTime);
		}
	}
}
