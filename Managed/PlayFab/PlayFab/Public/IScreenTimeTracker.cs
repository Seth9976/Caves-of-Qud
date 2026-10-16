using System;

namespace PlayFab.Public
{
	// Token: 0x0200015C RID: 348
	public interface IScreenTimeTracker
	{
		// Token: 0x06000494 RID: 1172
		void OnEnable();

		// Token: 0x06000495 RID: 1173
		void OnDisable();

		// Token: 0x06000496 RID: 1174
		void OnDestroy();

		// Token: 0x06000497 RID: 1175
		void OnApplicationQuit();

		// Token: 0x06000498 RID: 1176
		void OnApplicationFocus(bool isFocused);

		// Token: 0x06000499 RID: 1177
		void ClientSessionStart(string entityId, string entityType, string playFabUserId);

		// Token: 0x0600049A RID: 1178
		void Send();
	}
}
