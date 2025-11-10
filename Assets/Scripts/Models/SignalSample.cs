using System;

namespace Assignment
{
	/// <summary>
	/// Data received from the simulation
	/// </summary>
	[Serializable]
	public class SignalSample
	{
		public int seq; // sequential number
		public long timestampMs; // epoch ms UTC
		public float value; // [0;1]
	}
}