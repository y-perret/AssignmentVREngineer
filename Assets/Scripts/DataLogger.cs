using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static Assignment.Utils.TimeUtils;


namespace Assignment
{
	/// <summary>
	/// Handle the logging of the data collected during a session
	/// </summary>
	public class DataLogger
	{
		private List<TrialEvent> _events = new List<TrialEvent>();
		private List<SignalSample> _signalSamples = new List<SignalSample>();
		private long _sessionStartTimeMs;

		public bool IsRecording { get; private set; }
		public string SessionId { get; private set; }

		public void StartSession()
		{
			_events.Clear();
			_signalSamples.Clear();
			SessionId = System.Guid.NewGuid().ToString();
			_sessionStartTimeMs = UtcNowMs();
			IsRecording = true;
		}

		public void StopSession()
		{
			IsRecording = false;
			SaveSummary();
		}

		public void StartTrial()
		{
			_signalSamples.Clear();
		}

		public void LogTrialEvent(TrialEvent trialEvent)
		{
			if (!IsRecording)
			{
				return;
			}

			trialEvent.signalSamples = new List<SignalSample>(_signalSamples);
			_events.Add(trialEvent);
		}

		public void LogSignalSample(SignalSample signalSample)
		{
			if (!IsRecording)
			{
				return;
			}

			_signalSamples.Add(signalSample);
		}

		private void SaveSummary()
		{
			SessionSummary summary = new SessionSummary
			{
				sessionId = SessionId,
				sessionStartMs = _sessionStartTimeMs,
				sessionEndMs = UtcNowMs(),
				totalTrials = _events.Count,
				averageReactionTimeMs = (long) (_events.Count > 0 ? _events.Average(e => e.reactionTimeMs) : 0),
				pctMissedResponses = _events.Count > 0 ? (float) _events.Count(e => e.trialStatus != ReactionTrainingManager.TrialStatus.Reacted) / _events.Count : 0f,
				averageSignalValue = _events.SelectMany(e => e.signalSamples).DefaultIfEmpty().Average(s => s?.value ?? -1),
				trials = _events
			};

			string json = JsonUtility.ToJson(summary, true);

			// Write locally
			string path = Path.Combine(Application.dataPath, $"session_{SessionId}.json");
			File.WriteAllText(path, json);

			Debug.Log($"Session summary saved: {path}");
			Debug.Log(json);
		}
	}
}