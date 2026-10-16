using System;

namespace Unity.Profiling
{
	// Token: 0x02000072 RID: 114
	internal sealed class ProfilerRecorderDebugView
	{
		// Token: 0x060001FA RID: 506 RVA: 0x0000476B File Offset: 0x0000296B
		public ProfilerRecorderDebugView(ProfilerRecorder r)
		{
			this.m_Recorder = r;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000477C File Offset: 0x0000297C
		public ProfilerRecorderSample[] Items
		{
			get
			{
				return this.m_Recorder.ToArray();
			}
		}

		// Token: 0x0400019D RID: 413
		private ProfilerRecorder m_Recorder;
	}
}
