using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assignment
{
	/// <summary>
	/// Gather input event from PlayerInput.
	/// </summary>
	[RequireComponent(typeof(PlayerInput))]
	public class InputManager : MonoBehaviour
	{
		public event Action Reacted;

		/// <summary>
		/// This method is called when one button assigned to the react action is called
		/// </summary>
		public void OnReact()
		{
			Reacted.Invoke();
		}
	}
}