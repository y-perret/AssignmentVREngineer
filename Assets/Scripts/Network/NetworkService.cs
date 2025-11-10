using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assignment
{
	/// <summary>
	/// Class providing a service using a network client
	/// </summary>
	public class NetworkService
	{
		private const string TrialEventEndPointURL = "/api/reaction/events";
		private const string SummaryEndPointURL = "/api/reaction/summaries";
		private const string JsonContentType = "application/json";

		private readonly NetworkClient _client;

		public NetworkService(NetworkClient networkClient)
		{
			_client = networkClient;
		}

		/// <summary>
		/// Post trial event data
		/// </summary>
		/// <param name="trialEvent"></param>
		/// <returns></returns>
		public async Task<bool> PostTrialEvent(TrialEvent trialEvent)
		{
			string json = JsonUtility.ToJson(trialEvent);
			using StringContent content = new StringContent(json, Encoding.UTF8, JsonContentType);

			HttpResponseMessage response = await _client.PostAsync(TrialEventEndPointURL, content);
			return response.IsSuccessStatusCode;
		}

		/// <summary>
		/// Post session summary data
		/// </summary>
		/// <param name="sessionSummary"></param>
		/// <returns></returns>
		public async Task<bool> PostSessionSummary(SessionSummary sessionSummary)
		{
			string json = JsonUtility.ToJson(sessionSummary);
			using StringContent content = new StringContent(json, Encoding.UTF8, JsonContentType);

			HttpResponseMessage response = await _client.PostAsync(SummaryEndPointURL, content);
			return response.IsSuccessStatusCode;
		}
	}
}