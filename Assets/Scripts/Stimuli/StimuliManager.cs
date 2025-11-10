using System.Collections.Generic;
using UnityEngine;

namespace Assignment
{
	/// <summary>
	/// Manage all the visual side (stimuli) of the activing
	/// </summary>
	public class StimuliManager : MonoBehaviour
	{
		[SerializeField] private List<Stimulus> _stimulis;

		private Stimulus _currentStimuli;

		public enum StimuliType
		{
			Mole
		}

		/// <summary>
		/// Make a stimulus appear. Due to lack of time, the type of stimulus is is not supported as there is only one
		/// </summary>
		/// <param name="stimuliType">type of stimulus</param>
		public void Show(StimuliType stimuliType)
		{
			int randomIndex = Random.Range(0, _stimulis.Count);
			_currentStimuli = _stimulis[randomIndex];
			_currentStimuli.Show();
		}

		public void Hide()
		{
			_currentStimuli.Hide();
		}
	}
}