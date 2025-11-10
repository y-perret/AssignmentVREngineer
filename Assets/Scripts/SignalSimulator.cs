using System;
using System.Collections;
using UnityEngine;
using static Assignment.Utils.TimeUtils;

namespace Assignment
{
	/// <summary>
	/// Simulate a continuous signal receiving a SignalSample at a constant interval
	/// </summary>
	public class SignalSimulator : MonoBehaviour
	{
		[SerializeField] private int _sampleIntervalMs = 100;

		private int _seq;
		private Coroutine _recordingCoroutine;

		public event Action<SignalSample> SignalReceived;

		public void StartSimulation()
		{
			_seq = 0;
			_recordingCoroutine = StartCoroutine(SampleLoop());
		}

		public void StopSimulation()
		{
			if (_recordingCoroutine != null)
			{
				StopCoroutine(_recordingCoroutine);
			}
		}

		private IEnumerator SampleLoop()
		{
			while (true)
			{
				float value = 0.5f + 0.5f * Mathf.Sin(Time.time) + UnityEngine.Random.Range(-0.05f, 0.05f);

				SignalSample signalSample = new SignalSample
				{
					timestampMs = UtcNowMs(),
					value = Mathf.Clamp01(value),
					seq = _seq++
				};

				SignalReceived?.Invoke(signalSample);

				yield return new WaitForSeconds(_sampleIntervalMs / 1000f);
			}
		}
	}

}