using System;

namespace UnityEngine
{
	// Token: 0x02000228 RID: 552
	internal interface IPlayerEditorConnectionNative
	{
		// Token: 0x06001E15 RID: 7701
		void Initialize();

		// Token: 0x06001E16 RID: 7702
		void DisconnectAll();

		// Token: 0x06001E17 RID: 7703
		void SendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x06001E18 RID: 7704
		bool TrySendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x06001E19 RID: 7705
		void Poll();

		// Token: 0x06001E1A RID: 7706
		void RegisterInternal(Guid messageId);

		// Token: 0x06001E1B RID: 7707
		void UnregisterInternal(Guid messageId);

		// Token: 0x06001E1C RID: 7708
		bool IsConnected();
	}
}
