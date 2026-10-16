using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200008F RID: 143
	[NativeAsStruct]
	[NativeConditional("ENABLE_PROFILER")]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncReadManagerSummaryMetrics
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00005AF9 File Offset: 0x00003CF9
		[NativeName("totalBytesRead")]
		public ulong TotalBytesRead { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00005B01 File Offset: 0x00003D01
		[NativeName("averageBandwidthMBPerSecond")]
		public float AverageBandwidthMBPerSecond { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00005B09 File Offset: 0x00003D09
		[NativeName("averageReadSizeInBytes")]
		public float AverageReadSizeInBytes { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00005B11 File Offset: 0x00003D11
		[NativeName("averageWaitTimeMicroseconds")]
		public float AverageWaitTimeMicroseconds { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00005B19 File Offset: 0x00003D19
		[NativeName("averageReadTimeMicroseconds")]
		public float AverageReadTimeMicroseconds { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00005B21 File Offset: 0x00003D21
		[NativeName("averageTotalRequestTimeMicroseconds")]
		public float AverageTotalRequestTimeMicroseconds { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00005B29 File Offset: 0x00003D29
		[NativeName("averageThroughputMBPerSecond")]
		public float AverageThroughputMBPerSecond { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00005B31 File Offset: 0x00003D31
		[NativeName("longestWaitTimeMicroseconds")]
		public float LongestWaitTimeMicroseconds { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00005B39 File Offset: 0x00003D39
		[NativeName("longestReadTimeMicroseconds")]
		public float LongestReadTimeMicroseconds { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00005B41 File Offset: 0x00003D41
		[NativeName("longestReadAssetType")]
		public ulong LongestReadAssetType { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00005B49 File Offset: 0x00003D49
		[NativeName("longestWaitAssetType")]
		public ulong LongestWaitAssetType { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00005B51 File Offset: 0x00003D51
		[NativeName("longestReadSubsystem")]
		public AssetLoadingSubsystem LongestReadSubsystem { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00005B59 File Offset: 0x00003D59
		[NativeName("longestWaitSubsystem")]
		public AssetLoadingSubsystem LongestWaitSubsystem { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00005B61 File Offset: 0x00003D61
		[NativeName("numberOfInProgressRequests")]
		public int NumberOfInProgressRequests { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00005B69 File Offset: 0x00003D69
		[NativeName("numberOfCompletedRequests")]
		public int NumberOfCompletedRequests { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00005B71 File Offset: 0x00003D71
		[NativeName("numberOfFailedRequests")]
		public int NumberOfFailedRequests { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00005B79 File Offset: 0x00003D79
		[NativeName("numberOfWaitingRequests")]
		public int NumberOfWaitingRequests { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00005B81 File Offset: 0x00003D81
		[NativeName("numberOfCanceledRequests")]
		public int NumberOfCanceledRequests { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00005B89 File Offset: 0x00003D89
		[NativeName("totalNumberOfRequests")]
		public int TotalNumberOfRequests { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00005B91 File Offset: 0x00003D91
		[NativeName("numberOfCachedReads")]
		public int NumberOfCachedReads { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00005B99 File Offset: 0x00003D99
		[NativeName("numberOfAsyncReads")]
		public int NumberOfAsyncReads { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00005BA1 File Offset: 0x00003DA1
		[NativeName("numberOfSyncReads")]
		public int NumberOfSyncReads { get; }
	}
}
