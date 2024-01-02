using UnityEngine;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure;

namespace CodeBase.Logic
{
	public class SaveTrigger : MonoBehaviour
	{
		[SerializeField]
		private BoxCollider boxCollider;

		private ISaveLoadService _saveLoadService;

		private void Awake()
		{
			_saveLoadService = AllServices.Container.Single<ISaveLoadService>();
		}

		private void OnTriggerEnter(Collider other)
		{
			_saveLoadService.SaveProgress();
			Debug.Log("Progress Saved.");
			gameObject.SetActive(false);
		}

		private void OnDrawGizmos()
		{
			if (!boxCollider)
				return;

			Gizmos.color = new Color32(30, 200, 30, 130);
			Gizmos.DrawCube(transform.position + boxCollider.center, boxCollider.size);
		}
	}
}
