using System;
using System.Collections.Generic;

namespace Assignment
{
	[Serializable]
	public class SessionSummary
	{
		public string sessionId;
		public long sessionStartMs;
		public long sessionEndMs;
		public int totalTrials;
		public long averageReactionTimeMs;
		public float pctMissedResponses;
		public float averageSignal;
		public List<TrialEvent> trials;
	}
}