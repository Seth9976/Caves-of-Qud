using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.IO
{
	// Token: 0x02000440 RID: 1088
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	[NativeConditional("ENABLE_PROFILER")]
	[StaticAccessor("FileAccessor", StaticAccessorType.DoubleColon)]
	internal static class File
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x00050F98 File Offset: 0x0004F198
		internal static ulong totalOpenCalls
		{
			get
			{
				return File.GetTotalOpenCalls();
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060029F6 RID: 10742 RVA: 0x00050FB0 File Offset: 0x0004F1B0
		internal static ulong totalCloseCalls
		{
			get
			{
				return File.GetTotalCloseCalls();
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x00050FC8 File Offset: 0x0004F1C8
		internal static ulong totalReadCalls
		{
			get
			{
				return File.GetTotalReadCalls();
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060029F8 RID: 10744 RVA: 0x00050FE0 File Offset: 0x0004F1E0
		internal static ulong totalWriteCalls
		{
			get
			{
				return File.GetTotalWriteCalls();
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x00050FF8 File Offset: 0x0004F1F8
		internal static ulong totalSeekCalls
		{
			get
			{
				return File.GetTotalSeekCalls();
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x00051010 File Offset: 0x0004F210
		internal static ulong totalZeroSeekCalls
		{
			get
			{
				return File.GetTotalZeroSeekCalls();
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x00051028 File Offset: 0x0004F228
		internal static ulong totalFilesOpened
		{
			get
			{
				return File.GetTotalFilesOpened();
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060029FC RID: 10748 RVA: 0x00051040 File Offset: 0x0004F240
		internal static ulong totalFilesClosed
		{
			get
			{
				return File.GetTotalFilesClosed();
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060029FD RID: 10749 RVA: 0x00051058 File Offset: 0x0004F258
		internal static ulong totalBytesRead
		{
			get
			{
				return File.GetTotalBytesRead();
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x00051070 File Offset: 0x0004F270
		internal static ulong totalBytesWritten
		{
			get
			{
				return File.GetTotalBytesWritten();
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06002A00 RID: 10752 RVA: 0x00051094 File Offset: 0x0004F294
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x00051087 File Offset: 0x0004F287
		internal static bool recordZeroSeeks
		{
			get
			{
				return File.GetRecordZeroSeeks();
			}
			set
			{
				File.SetRecordZeroSeeks(value);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x000510AC File Offset: 0x0004F2AC
		// (set) Token: 0x06002A02 RID: 10754 RVA: 0x000510C3 File Offset: 0x0004F2C3
		internal static ThreadIORestrictionMode MainThreadIORestrictionMode
		{
			get
			{
				return File.GetMainThreadFileIORestriction();
			}
			set
			{
				File.SetMainThreadFileIORestriction(value);
			}
		}

		// Token: 0x06002A03 RID: 10755
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetRecordZeroSeeks(bool enable);

		// Token: 0x06002A04 RID: 10756
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetRecordZeroSeeks();

		// Token: 0x06002A05 RID: 10757
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalOpenCalls();

		// Token: 0x06002A06 RID: 10758
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalCloseCalls();

		// Token: 0x06002A07 RID: 10759
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalReadCalls();

		// Token: 0x06002A08 RID: 10760
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalWriteCalls();

		// Token: 0x06002A09 RID: 10761
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalSeekCalls();

		// Token: 0x06002A0A RID: 10762
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalZeroSeekCalls();

		// Token: 0x06002A0B RID: 10763
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalFilesOpened();

		// Token: 0x06002A0C RID: 10764
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalFilesClosed();

		// Token: 0x06002A0D RID: 10765
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalBytesRead();

		// Token: 0x06002A0E RID: 10766
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetTotalBytesWritten();

		// Token: 0x06002A0F RID: 10767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMainThreadFileIORestriction(ThreadIORestrictionMode mode);

		// Token: 0x06002A10 RID: 10768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ThreadIORestrictionMode GetMainThreadFileIORestriction();
	}
}
