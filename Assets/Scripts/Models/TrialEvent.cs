using System;
using System.Collections.Generic;

namespace Assignment
{
	/// <summary>
	/// Data corresponding to a trial event
	/// </summary>
	[Serializable]
	public class TrialEvent
	{
		public int trialIndex;
		public long trialStartMs; // Trial start date in unix epoch time
		public long stimuliStartMs; // Stimulus appearance date in unix epoch time. -1 if not not present.
		public string stimulusType;
		public ReactionTrainingManager.TrialStatus trialStatus;
		public long reactionTimeMs; // -1 if no response
		public List<SignalSample> signalSamples; // Signal samples during that trial
	}
}