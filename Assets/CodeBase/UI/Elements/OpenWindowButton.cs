using System;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class OpenWindowButton : MonoBehaviour
	{
		public Button button;
		public WindowId windowId;
		private IWindowService _windowService;

		public void Construct(IWindowService windowService) =>
			_windowService = windowService;

		private void Awake() =>
			button.onClick.AddListener(Open);

		private void Open() =>
			_windowService.Open(windowId);
	}
}