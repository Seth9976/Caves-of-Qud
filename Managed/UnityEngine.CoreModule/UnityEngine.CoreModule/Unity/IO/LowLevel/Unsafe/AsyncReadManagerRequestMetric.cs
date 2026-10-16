using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200008C RID: 140
	[NativeConditional("ENABLE_PROFILER")]
	[RequiredByNativeCode]
	public struct AsyncReadManagerRequestMetric
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000058F0 File Offset: 0x00003AF0
		[NativeName("assetName")]
		public readonly string AssetName { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000294 RID: 660 RVA: 0x000058F8 File Offset: 0x00003AF8
		[NativeName("fileName")]
		public readonly string FileName { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00005900 File Offset: 0x00003B00
		[NativeName("offsetBytes")]
		public readonly ulong OffsetBytes { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00005908 File Offset: 0x00003B08
		[NativeName("sizeBytes")]
		public readonly ulong SizeBytes { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00005910 File Offset: 0x00003B10
		[NativeName("assetTypeId")]
		public readonly ulong AssetTypeId { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00005918 File Offset: 0x00003B18
		[NativeName("currentBytesRead")]
		public readonly ulong CurrentBytesRead { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00005920 File Offset: 0x00003B20
		[NativeName("batchReadCount")]
		public readonly uint BatchReadCount { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00005928 File Offset: 0x00003B28
		[NativeName("isBatchRead")]
		public readonly bool IsBatchRead { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00005930 File Offset: 0x00003B30
		[NativeName("state")]
		public readonly ProcessingState State { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00005938 File Offset: 0x00003B38
		[NativeName("readType")]
		public readonly FileReadType ReadType { get; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00005940 File Offset: 0x00003B40
		[NativeName("priorityLevel")]
		public readonly Priority PriorityLevel { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00005948 File Offset: 0x00003B48
		[NativeName("subsystem")]
		public readonly AssetLoadingSubsystem Subsystem { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00005950 File Offset: 0x00003B50
		[NativeName("requestTimeMicroseconds")]
		public readonly double RequestTimeMicroseconds { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00005958 File Offset: 0x00003B58
		[NativeName("timeInQueueMicroseconds")]
		public readonly double TimeInQueueMicroseconds { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00005960 File Offset: 0x00003B60
		[NativeName("totalTimeMicroseconds")]
		public readonly double TotalTimeMicroseconds { get; }
	}
}
