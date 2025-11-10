using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assignment.Utils.TimeUtils;

namespace Assignment
{
	public class TrialManager : MonoBehaviour
	{
		public enum TrialStatus
		{
			Incomplete,
			TooEarly,
			TooLate,
			Reacted
		}

		[Header("References")]
		[SerializeField] private SignalSimulator _signalSimulator;
		[SerializeField] private StimuliManager _stimuliManager;
		[SerializeField] private UIManager _uIManager;
		[SerializeField] private InputManager _inputManager;

		[Header("Parameters")]
		[SerializeField] private int _trialsCount = 10;
		[SerializeField] private float _minInterTrialInterval = 1f;
		[SerializeField] private float _maxInterTrialInterval = 1.5f;
		[SerializeField] private float _feedbackDuration = 1f;
		[SerializeField] private float _maxResponseTime = 2f;

		private int _trialIndex;
		private bool _isWaitingForResponse;
		private bool _hasReacted;
		private TrialStatus _currentTrialStatus;
		private DataLogger _dataLogger = new DataLogger();
		private Coroutine _sessionCoroutine;

		private void Start()
		{
			_uIManager.SetStartButtonCallback(StartSession);
		}

		private void OnEnable()
		{
			_inputManager.Reacted += OnReacted;
			_signalSimulator.SignalReceived += _dataLogger.LogSignalSample;
		}

		private void OnDisable()
		{
			_inputManager.Reacted -= OnReacted;
			_signalSimulator.SignalReceived += _dataLogger.LogSignalSample;
		}

		private void OnReacted()
		{
			_hasReacted = true;
		}

		private void StartSession()
		{
			_uIManager.HideStartButton();
			_dataLogger.StartSession();
			_signalSimulator.StartSimulation();
			_sessionCoroutine = StartCoroutine(RunSession());
		}

		private void StopSession()
		{
			if (_sessionCoroutine != null)
			{
				StopCoroutine(_sessionCoroutine);
			}

			_uIManager.ShowStartButton();
			_signalSimulator.StopSimulation();
			_dataLogger.StopSession();
		}

		private IEnumerator RunSession()
		{
			for (_trialIndex = 0; _trialIndex < _trialsCount; _trialIndex++)
			{
				yield return RunTrial();
			}

			StopSession();
		}

		private IEnumerator RunTrial()
		{
			long stimuliStartMs = -1;
			long reactionTimeMs = -1;
			long responseMs = -1;
			_hasReacted = false;

			_currentTrialStatus = TrialStatus.Incomplete;
			long trialStartMs = UtcNowMs();
			_dataLogger.StartTrial();

			float start = Time.time;
			float interTrialInterval = UnityEngine.Random.Range(_minInterTrialInterval, _maxInterTrialInterval);

			yield return new WaitUntil(() => Time.time - start >= interTrialInterval || _hasReacted);

			if (_hasReacted)
			{
				_currentTrialStatus = TrialStatus.TooEarly;
			}
			else
			{
				_stimuliManager.Show(StimuliManager.StimuliType.Mole);
				stimuliStartMs = UtcNowMs();
				_isWaitingForResponse = true;
				float startTime = Time.time;

				while (_isWaitingForResponse && Time.time - startTime < _maxResponseTime)
				{
					if (_hasReacted)
					{
						responseMs = UtcNowMs();
						reactionTimeMs = responseMs - stimuliStartMs;
						_currentTrialStatus = TrialStatus.Reacted;
						_isWaitingForResponse = false;
					}
					yield return null;
				}

				if (_currentTrialStatus != TrialStatus.Reacted)
				{
					_currentTrialStatus = TrialStatus.TooLate;
				}

				_stimuliManager.Hide();
			}

			TrialEvent trial = new TrialEvent
			{
				trialIndex = _trialIndex,
				trialStartMs = trialStartMs,
				stimuliStartMs = stimuliStartMs,
				stimulusType = StimuliManager.StimuliType.Mole.ToString(),
				trialStatus = _currentTrialStatus,
				responseMs = responseMs,
				reactionTimeMs = reactionTimeMs,
			};

			_uIManager.ShowTrialFeedback(_currentTrialStatus);

			yield return new WaitForSeconds(_feedbackDuration);

			_uIManager.HideFeedback();
			_dataLogger.LogTrialEvent(trial);
		}
	}
}
