using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField]
		private GameBootstrapper bootstrapperPrefab;

		private void Awake()
		{
			var bootstrapper = FindObjectOfType<GameBootstrapper>();
			if (!bootstrapper)
				Instantiate(bootstrapperPrefab);
		}

	}
}