using UnityEngine;

namespace Assignment
{
	/// <summary>
	/// Contains configuration to connect to the backend
	/// </summary>
	[CreateAssetMenu(fileName = "NetworkConfig", menuName = "Scriptable Objects/NetworkConfig")]
	public class NetworkConfig : ScriptableObject
	{
		public string host;
		public string port;
		public bool isSecureProtocol;

		public string BaseUrl => $"{(isSecureProtocol ? "https://" : "http://")}{host}:{port}";
	}
}
