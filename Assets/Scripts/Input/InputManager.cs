using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assignment
{
	/// <summary>
	/// Gather input event from PlayerInput
	/// </summary>
	[RequireComponent(typeof(PlayerInput))]
	public class InputManager : MonoBehaviour
	{
		public event Action Reacted;

		public void OnReact()
		{
			Reacted.Invoke();
		}
	}
}