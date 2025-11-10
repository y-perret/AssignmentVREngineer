using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assignment
{
	public class Stimulus : MonoBehaviour
	{
		[SerializeField] private Transform _movableStimuli;
		[SerializeField] private Transform _targetPosition;
		[SerializeField] private float _moveDuration = 0.5f;

		private Vector3 _startPosition;
		private Coroutine _moveCoroutine;

		private void Start()
		{
			_startPosition = _movableStimuli.position;
		}

		private void Update()
		{
			if (Keyboard.current.aKey.wasPressedThisFrame)
				Show();

			if (Keyboard.current.sKey.wasPressedThisFrame)
				Hide();
		}

		public void Show()
		{
			if (_moveCoroutine != null)
			{
				StopCoroutine(_moveCoroutine);
			}

			_moveCoroutine = StartCoroutine(Move(_movableStimuli.position, _targetPosition.position, _moveDuration));
		}

		public void Hide()
		{
			if (_moveCoroutine != null)
			{
				StopCoroutine(_moveCoroutine);
			}

			_moveCoroutine = StartCoroutine(Move(_movableStimuli.position, _startPosition, _moveDuration));
		}

		private IEnumerator Move(Vector3 from, Vector3 to, float duration)
		{
			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / duration;
				_movableStimuli.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
				yield return null;
			}
			_movableStimuli.position = to;
		}
	}
}

