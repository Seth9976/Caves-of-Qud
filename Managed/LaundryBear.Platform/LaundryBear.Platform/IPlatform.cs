using System;

namespace LaundryBear.PlatformServices
{
	// Token: 0x02000024 RID: 36
	public interface IPlatform : IService
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006F RID: 111
		bool SupportsAchievements { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000070 RID: 112
		bool SupportsRichPresence { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000071 RID: 113
		bool SupportsUsers { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000072 RID: 114
		bool AllowsUserWindowModification { get; }

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000073 RID: 115
		// (remove) Token: 0x06000074 RID: 116
		event PlatformSuspendEventHandler SuspendEvent;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000075 RID: 117
		// (remove) Token: 0x06000076 RID: 118
		event PlatformResumeEventHandler ResumeEvent;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000077 RID: 119
		// (remove) Token: 0x06000078 RID: 120
		event PlatformShutdownEventHandler ShutdownEvent;

		// Token: 0x06000079 RID: 121
		void SetupRequiredData(object data);

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007A RID: 122
		bool SupportsQuit { get; }

		// Token: 0x0600007B RID: 123
		string GetSystemLanguage();

		// Token: 0x0600007C RID: 124
		void Quit();
	}
}
