using System;
using AiUnity.NLog.Core.Targets;

namespace AiUnity.NLog.Core.Internal.FileAppenders
{
	// Token: 0x0200027E RID: 638
	internal interface ICreateFileParameters
	{
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060016DC RID: 5852
		int ConcurrentWriteAttemptDelay { get; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060016DD RID: 5853
		int ConcurrentWriteAttempts { get; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060016DE RID: 5854
		bool ConcurrentWrites { get; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060016DF RID: 5855
		bool CreateDirs { get; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060016E0 RID: 5856
		bool EnableFileDelete { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060016E1 RID: 5857
		int BufferSize { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060016E2 RID: 5858
		bool ForceManaged { get; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060016E3 RID: 5859
		Win32FileAttributes FileAttributes { get; }
	}
}
