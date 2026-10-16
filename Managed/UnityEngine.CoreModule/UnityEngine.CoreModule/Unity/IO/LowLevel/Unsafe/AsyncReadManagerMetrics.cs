using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x0200008D RID: 141
	[NativeConditional("ENABLE_PROFILER")]
	public static class AsyncReadManagerMetrics
	{
		// Token: 0x060002A2 RID: 674
		[FreeFunction("AreMetricsEnabled_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsEnabled();

		// Token: 0x060002A3 RID: 675
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->ClearMetrics")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearMetrics_Internal();

		// Token: 0x060002A4 RID: 676 RVA: 0x00005968 File Offset: 0x00003B68
		public static void ClearCompletedMetrics()
		{
			AsyncReadManagerMetrics.ClearMetrics_Internal();
		}

		// Token: 0x060002A5 RID: 677
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Internal(bool clear);

		// Token: 0x060002A6 RID: 678 RVA: 0x00005974 File Offset: 0x00003B74
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc")]
		[ThreadSafe]
		internal static void GetMetrics_NoAlloc_Internal([NotNull] List<AsyncReadManagerRequestMetric> metrics, bool clear)
		{
			if (metrics == null)
			{
				ThrowHelper.ThrowArgumentNullException(metrics, "metrics");
			}
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Internal_Injected(metrics, clear);
		}

		// Token: 0x060002A7 RID: 679
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMarshalledMetrics_Filtered_Managed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerRequestMetric[] GetMetrics_Filtered_Internal(AsyncReadManagerMetricsFilters filters, bool clear);

		// Token: 0x060002A8 RID: 680 RVA: 0x00005998 File Offset: 0x00003B98
		[FreeFunction("GetAsyncReadManagerMetrics()->GetMetrics_NoAlloc_Filtered_Managed")]
		[ThreadSafe]
		internal static void GetMetrics_NoAlloc_Filtered_Internal([NotNull] List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters filters, bool clear)
		{
			if (metrics == null)
			{
				ThrowHelper.ThrowArgumentNullException(metrics, "metrics");
			}
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Filtered_Internal_Injected(metrics, filters, clear);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000059BC File Offset: 0x00003BBC
		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Filtered_Internal(filters, flag);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000059E0 File Offset: 0x00003BE0
		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetricsFilters filters, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Filtered_Internal(outMetrics, filters, flag);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00005A04 File Offset: 0x00003C04
		public static AsyncReadManagerRequestMetric[] GetMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetMetrics_Internal(flag);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00005A28 File Offset: 0x00003C28
		public static void GetMetrics(List<AsyncReadManagerRequestMetric> outMetrics, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			AsyncReadManagerMetrics.GetMetrics_NoAlloc_Internal(outMetrics, flag);
		}

		// Token: 0x060002AD RID: 685
		[FreeFunction("GetAsyncReadManagerMetrics()->StartCollecting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StartCollectingMetrics();

		// Token: 0x060002AE RID: 686
		[FreeFunction("GetAsyncReadManagerMetrics()->StopCollecting")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StopCollectingMetrics();

		// Token: 0x060002AF RID: 687
		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetrics")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetrics_Internal(bool clear);

		// Token: 0x060002B0 RID: 688 RVA: 0x00005A4C File Offset: 0x00003C4C
		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetrics_Internal(flag);
		}

		// Token: 0x060002B1 RID: 689
		[FreeFunction("GetAsyncReadManagerMetrics()->GetCurrentSummaryMetricsWithFilters")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryMetricsWithFilters_Internal(AsyncReadManagerMetricsFilters metricsFilters, bool clear);

		// Token: 0x060002B2 RID: 690 RVA: 0x00005A70 File Offset: 0x00003C70
		public static AsyncReadManagerSummaryMetrics GetCurrentSummaryMetrics(AsyncReadManagerMetricsFilters metricsFilters, AsyncReadManagerMetrics.Flags flags)
		{
			bool flag = (flags & AsyncReadManagerMetrics.Flags.ClearOnRead) == AsyncReadManagerMetrics.Flags.ClearOnRead;
			return AsyncReadManagerMetrics.GetSummaryMetricsWithFilters_Internal(metricsFilters, flag);
		}

		// Token: 0x060002B3 RID: 691
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_Managed")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_Internal(AsyncReadManagerRequestMetric[] metrics);

		// Token: 0x060002B4 RID: 692 RVA: 0x00005A94 File Offset: 0x00003C94
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_Internal(metrics);
		}

		// Token: 0x060002B5 RID: 693
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetrics_FromContainer_Managed", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetrics_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics);

		// Token: 0x060002B6 RID: 694 RVA: 0x00005AAC File Offset: 0x00003CAC
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetrics_FromContainer_Internal(metrics);
		}

		// Token: 0x060002B7 RID: 695
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_Managed")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_Internal(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters);

		// Token: 0x060002B8 RID: 696 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(AsyncReadManagerRequestMetric[] metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_Internal(metrics, metricsFilters);
		}

		// Token: 0x060002B9 RID: 697
		[FreeFunction("GetAsyncReadManagerMetrics()->GetSummaryOfMetricsWithFilters_FromContainer_Managed", ThrowsException = true)]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AsyncReadManagerSummaryMetrics GetSummaryOfMetricsWithFilters_FromContainer_Internal(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters);

		// Token: 0x060002BA RID: 698 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public static AsyncReadManagerSummaryMetrics GetSummaryOfMetrics(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters metricsFilters)
		{
			return AsyncReadManagerMetrics.GetSummaryOfMetricsWithFilters_FromContainer_Internal(metrics, metricsFilters);
		}

		// Token: 0x060002BB RID: 699
		[ThreadSafe]
		[FreeFunction("GetAsyncReadManagerMetrics()->GetTotalSizeNonASRMReadsBytes")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong GetTotalSizeOfNonASRMReadsBytes(bool emptyAfterRead);

		// Token: 0x060002BC RID: 700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMetrics_NoAlloc_Internal_Injected(List<AsyncReadManagerRequestMetric> metrics, bool clear);

		// Token: 0x060002BD RID: 701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMetrics_NoAlloc_Filtered_Internal_Injected(List<AsyncReadManagerRequestMetric> metrics, AsyncReadManagerMetricsFilters filters, bool clear);

		// Token: 0x0200008E RID: 142
		[Flags]
		public enum Flags
		{
			// Token: 0x04000230 RID: 560
			None = 0,
			// Token: 0x04000231 RID: 561
			ClearOnRead = 1
		}
	}
}
