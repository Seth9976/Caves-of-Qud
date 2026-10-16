using System;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000430 RID: 1072
	public interface IEditorPlayerConnection
	{
		// Token: 0x060029BD RID: 10685
		void Register(Guid messageId, UnityAction<MessageEventArgs> callback);

		// Token: 0x060029BE RID: 10686
		void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback);

		// Token: 0x060029BF RID: 10687
		void DisconnectAll();

		// Token: 0x060029C0 RID: 10688
		void RegisterConnection(UnityAction<int> callback);

		// Token: 0x060029C1 RID: 10689
		void RegisterDisconnection(UnityAction<int> callback);

		// Token: 0x060029C2 RID: 10690
		void UnregisterConnection(UnityAction<int> callback);

		// Token: 0x060029C3 RID: 10691
		void UnregisterDisconnection(UnityAction<int> callback);

		// Token: 0x060029C4 RID: 10692
		void Send(Guid messageId, byte[] data);

		// Token: 0x060029C5 RID: 10693
		bool TrySend(Guid messageId, byte[] data);
	}
}
