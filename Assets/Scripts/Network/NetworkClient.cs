using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment
{
	public class NetworkClient : INetworkCClient
	{
		private const float TimeoutDuration = 20f;

		private HttpClient _httpClient;

		public string HttpClientAddress { get; }


		public NetworkClient(string httpClientAddress)
		{
			HttpClientAddress = httpClientAddress;

			HttpClientHandler httpClientHandler = new HttpClientHandler
			{
				AllowAutoRedirect = false,
			};

			// HTTP client for http requests
			_httpClient = new HttpClient(httpClientHandler);
			_httpClient.Timeout = TimeSpan.FromSeconds(TimeoutDuration);
		}

		public async Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default)
		{
			using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			return await ExecuteAsync(request, cancellationToken);
		}

		public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, CancellationToken cancellationToken = default)
		{
			using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
			return await ExecuteAsync(request, cancellationToken);
		}

		public async Task<HttpResponseMessage> PutAsync(string url, StringContent content, CancellationToken cancellationToken = default)
		{
			using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url) { Content = content };
			return await ExecuteAsync(request, cancellationToken);
		}

		public async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken = default)
		{
			using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
			return await ExecuteAsync(request, cancellationToken);
		}

		protected async Task<HttpResponseMessage> ExecuteAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
		{
			try
			{
				// We compose the full URL
				request.RequestUri = new Uri(new Uri(HttpClientAddress), request.RequestUri);

				Task<HttpResponseMessage> responseTask = null;


				// Wait on the request to complete
				responseTask = _httpClient.SendAsync(request, cancellationToken);

				await responseTask;

				// Acquire the result
				return responseTask.Result;
			}
			catch (Exception exception)
			{
				throw; // Propagate exception further
			}
		}
	}
}
