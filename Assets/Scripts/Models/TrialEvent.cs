using System;
using System.Collections.Generic;

namespace Assignment
{
	[Serializable]
	public class TrialEvent
	{
		public int trialIndex;
		public long trialStartMs;
		public long stimuliStartMs;
		public string stimulusType;
		public TrialManager.TrialStatus trialStatus;
		public long responseMs; // epoch ms, 0 if missing
		public long reactionTimeMs; // -1 if no response
		public List<SignalSample> signalSamples; // small window optionally attached
	}
}