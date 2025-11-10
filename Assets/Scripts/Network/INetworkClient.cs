using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment
{
	public interface INetworkCClient
	{
		/// <summary>
		/// Executes a HTTP GET request asynchronously.
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="cancellationToken">An optional cancellation token</param>
		/// <returns></returns>
		public Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default);

		/// <summary>
		/// Executes a HTTP POST request asynchronously.
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="content">The content of the request</param>
		/// <param name="cancellationToken">An optional cancellation token</param>
		/// <returns></returns>
		public Task<HttpResponseMessage> PostAsync(string url, HttpContent content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Executes a HTTP PUT request asynchronously.
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="content">The content of the request</param>
		/// <param name="cancellationToken">An optional cancellation token</param>
		/// <returns></returns>
		public Task<HttpResponseMessage> PutAsync(string url, StringContent content, CancellationToken cancellationToken = default);

		/// <summary>
		/// Executes a HTTP DELETE request asynchronously.
		/// </summary>
		/// <param name="url">The URL</param>
		/// <param name="cancellationToken">An optional cancellation token</param>
		/// <returns></returns>
		public Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken = default);
	}
}