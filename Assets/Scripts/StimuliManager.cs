using System.Collections.Generic;
using UnityEngine;

namespace Assignment
{
	public class StimuliManager : MonoBehaviour
	{
		[SerializeField] private List<Stimulus> _stimulis;

		private Stimulus _currentStimuli;

		public enum StimuliType
		{
			Mole
		}

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