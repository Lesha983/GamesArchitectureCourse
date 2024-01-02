using CodeBase.Data;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using SF = UnityEngine.SerializeField;

namespace CodeBase.Hero
{
	public class HeroMove : MonoBehaviour, ISavedProgress
	{
		[SF] private CharacterController characterController;
		[SF] private float movmentSpeed;

		private IInputService _inputService;
		private Camera _camera;

		public void LoadProgress(PlayerProgress progress)
		{
			if (GetCurrentLevelName() != progress.WorldData.PositionOnLevel.Level)
				return;

			var savedPosition = progress.WorldData.PositionOnLevel.Position;
			if (savedPosition != null)
				Warp(savedPosition);
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.WorldData.PositionOnLevel = new PositionOnLevel(
													GetCurrentLevelName(),
													transform.position.AsVectorData());
		}

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
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

		private void Warp(Vector3Data to)
		{
			characterController.enabled = false;
			transform.position = to.AsUnityVector().AddY(characterController.height);
			characterController.enabled = true;
		}

		private string GetCurrentLevelName() =>
			SceneManager.GetActiveScene().name;
	}
}
