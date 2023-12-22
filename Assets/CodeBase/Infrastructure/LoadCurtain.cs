using System.Collections;
using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace CodeBase.Infrastructure
{
	public class LoadCurtain : MonoBehaviour
	{
		[SF] private CanvasGroup curtain;

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		public void Show()
		{
			gameObject.SetActive(true);
			curtain.alpha = 1;
		}

		public void Hide() =>
			StartCoroutine(nameof(FadeIn));

		private IEnumerator FadeIn()
		{
			var delay = new WaitForSeconds(0.03f);

			while (curtain.alpha > 0f)
			{
				curtain.alpha -= 0.03f;
				yield return delay;
			}

			gameObject.SetActive(false);
		}
	}
}