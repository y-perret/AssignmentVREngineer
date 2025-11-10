using System;
using System.Collections.Generic;

namespace Assignment
{
	/// <summary>
	/// Summary of a completed session
	/// </summary>
	[Serializable]
	public class SessionSummary
	{
		public string sessionId;
		public long sessionStartMs; // Session start date in unix epoch time
		public long sessionEndMs; // Session end date in unix epoch time
		public int totalTrials;
		public long averageReactionTimeMs;
		public float pctMissedResponses; // Percentage of missed response (too early or too late)
		public float averageSignalValue;
		public List<TrialEvent> trials;
	}
}