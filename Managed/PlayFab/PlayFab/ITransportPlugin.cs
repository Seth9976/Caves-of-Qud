using System;

namespace PlayFab
{
	// Token: 0x02000021 RID: 33
	public interface ITransportPlugin : IPlayFabPlugin
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060002B4 RID: 692
		bool IsInitialized { get; }

		// Token: 0x060002B5 RID: 693
		void Initialize();

		// Token: 0x060002B6 RID: 694
		void Update();

		// Token: 0x060002B7 RID: 695
		void OnDestroy();

		// Token: 0x060002B8 RID: 696
		void SimpleGetCall(string fullUrl, Action<byte[]> successCallback, Action<string> errorCallback);

		// Token: 0x060002B9 RID: 697
		void SimplePutCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback);

		// Token: 0x060002BA RID: 698
		void SimplePostCall(string fullUrl, byte[] payload, Action<byte[]> successCallback, Action<string> errorCallback);

		// Token: 0x060002BB RID: 699
		void MakeApiCall(object reqContainer);

		// Token: 0x060002BC RID: 700
		int GetPendingMessages();
	}
}
