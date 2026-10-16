using System;

namespace UnityEngine.InputForUI
{
	// Token: 0x02000024 RID: 36
	internal interface IEventProviderImpl
	{
		// Token: 0x0600009C RID: 156
		void Initialize();

		// Token: 0x0600009D RID: 157
		void Shutdown();

		// Token: 0x0600009E RID: 158
		void Update();

		// Token: 0x0600009F RID: 159
		void OnFocusChanged(bool focus);

		// Token: 0x060000A0 RID: 160
		bool RequestCurrentState(Event.Type type);

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A1 RID: 161
		uint playerCount { get; }
	}
}
