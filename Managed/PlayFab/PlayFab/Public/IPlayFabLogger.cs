using System;
using System.Net;

namespace PlayFab.Public
{
	// Token: 0x0200015E RID: 350
	public interface IPlayFabLogger
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060004A5 RID: 1189
		// (set) Token: 0x060004A6 RID: 1190
		IPAddress ip { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060004A7 RID: 1191
		// (set) Token: 0x060004A8 RID: 1192
		int port { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060004A9 RID: 1193
		// (set) Token: 0x060004AA RID: 1194
		string url { get; set; }

		// Token: 0x060004AB RID: 1195
		void OnEnable();

		// Token: 0x060004AC RID: 1196
		void OnDisable();

		// Token: 0x060004AD RID: 1197
		void OnDestroy();
	}
}
