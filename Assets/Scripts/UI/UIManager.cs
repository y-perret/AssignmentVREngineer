using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assignment.TrialManager;

namespace Assignment
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private GameObject _feedback;
		[SerializeField] private TMP_Text _feedbackText;
		[SerializeField] private Button  _startButton;

		private void Awake()
		{
			HideFeedback();
			ShowStartButton();
		}

		public void ShowTrialFeedback(TrialStatus trialStatus)
		{
			_feedback.SetActive(true);
			_feedbackText.text = trialStatus switch
			{
				TrialStatus.Incomplete => "Trial interrupted",
				TrialStatus.TooEarly => "Too early",
				TrialStatus.TooLate => "Too slow",
				TrialStatus.Reacted => "Nice!",
				_ => "",
			};
		}

		public void HideFeedback()
		{
			_feedback.SetActive(false);
		}

		public void SetStartButtonCallback(Action startAction)
		{
			_startButton.onClick.RemoveAllListeners();
			_startButton.onClick.AddListener(() => startAction());
		}

		public void ShowStartButton()
		{
			_startButton.gameObject.SetActive(true);
		}

		public void HideStartButton()
		{
			_startButton.gameObject.SetActive(false);
		}
	}
}