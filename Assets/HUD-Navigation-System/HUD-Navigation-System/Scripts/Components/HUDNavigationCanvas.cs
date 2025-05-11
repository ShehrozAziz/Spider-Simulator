using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SickscoreGames;

namespace SickscoreGames.HUDNavigationSystem
{
	[AddComponentMenu (HNS.Name + "/HUD Navigation Canvas"), DisallowMultipleComponent]
	public class HUDNavigationCanvas : MonoBehaviour
	{
		private static HUDNavigationCanvas _Instance;
		public static HUDNavigationCanvas Instance {
			get {
				if (_Instance == null) {
					_Instance = FindObjectOfType<HUDNavigationCanvas> ();
				}
				return _Instance;
			}
		}


		#region Variables
		public _RadarReferences Radar;
		public _CompassBarReferences CompassBar;
		public _IndicatorReferences Indicator;
		public _MinimapReferences Minimap;

		
		public float CompassBarCurrentDegrees { get; private set; }

		public bool isEnabled // use the EnableCanvas(bool) method to change this value.
		{
			get { return _isEnabled; }
			private set { _isEnabled = value; }
		}
		[SerializeField]private bool _isEnabled = true;

		private HUDNavigationSystem _HUDNavigationSystem;
		#endregion


		#region Main Methods
		void Awake ()
		{
			if (_Instance != null) {
				Destroy (this.gameObject);
				return;
			}

			_Instance = this;
		}


		void Start ()
		{
			// assign references
			if (_HUDNavigationSystem == null)
				_HUDNavigationSystem = HUDNavigationSystem.Instance;

			// dont destroy on load
			if (_HUDNavigationSystem != null && _HUDNavigationSystem.KeepAliveOnLoad)
				DontDestroyOnLoad (this.gameObject);
		}


		/// <summary>
		/// Enable / Disable the canvas at runtime.
		/// </summary>
		/// <param name="value">value</param>
		public void EnableCanvas (bool value)
		{
			if (value == isEnabled)
				return;

			// enable/disable canvas
			isEnabled = value;
			this.gameObject.SetActive (value);
		}
		#endregion



		#region Indicator Methods
		public void InitIndicators ()
		{
			// check references
			if (Indicator.Panel == null || Indicator.ElementContainer == null) {
				ReferencesMissing ("Indicator");
				return;
			}

			// show indicators
		//	ShowIndicators (true);
		}


		public void ShowIndicators (bool value)
		{
			if (Indicator.Panel != null)
				Indicator.Panel.gameObject.SetActive (value);
		}
		#endregion


		


		#region Utility Methods
		void ReferencesMissing (string feature)
		{
			Debug.LogErrorFormat ("{0} references are missing! Please assign them on the HUDNavigationCanvas component.", feature);
			this.enabled = false;
		}
		#endregion


		#region Subclasses
		[System.Serializable]
		public class _RadarReferences
		{
			public RectTransform Panel;
			public RectTransform Radar;
			public RectTransform PlayerIndicator;
			public RectTransform ElementContainer;
		}


		[System.Serializable]
		public class _CompassBarReferences
		{
			public RectTransform Panel;
			public RawImage Compass;
			public RectTransform ElementContainer;
		}


		[System.Serializable]
		public class _IndicatorReferences
		{
			public RectTransform Panel;
			public RectTransform ElementContainer;
		}


		[System.Serializable]
		public class _MinimapReferences
		{
			public RectTransform Panel;
			public Image MapMaskImage;
			public RectTransform MapContainer;
			public RectTransform PlayerIndicator;
			public RectTransform ElementContainer;
		}
		#endregion
	}
}
