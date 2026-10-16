using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x02000371 RID: 881
	[NativeHeader("Runtime/Scripting/GarbageCollector.h")]
	public static class GarbageCollector
	{
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060028D2 RID: 10450 RVA: 0x0004ED38 File Offset: 0x0004CF38
		// (remove) Token: 0x060028D3 RID: 10451 RVA: 0x0004ED6C File Offset: 0x0004CF6C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<GarbageCollector.Mode> GCModeChanged;

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x0004EDA0 File Offset: 0x0004CFA0
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x0004EDB8 File Offset: 0x0004CFB8
		public static GarbageCollector.Mode GCMode
		{
			get
			{
				return GarbageCollector.GetMode();
			}
			set
			{
				bool flag = value == GarbageCollector.GetMode();
				if (!flag)
				{
					GarbageCollector.SetMode(value);
					bool flag2 = GarbageCollector.GCModeChanged != null;
					if (flag2)
					{
						GarbageCollector.GCModeChanged(value);
					}
				}
			}
		}

		// Token: 0x060028D6 RID: 10454
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMode(GarbageCollector.Mode mode);

		// Token: 0x060028D7 RID: 10455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GarbageCollector.Mode GetMode();

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060028D8 RID: 10456
		public static extern bool isIncremental
		{
			[NativeMethod("GetIncrementalEnabled")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060028D9 RID: 10457
		// (set) Token: 0x060028DA RID: 10458
		public static extern ulong incrementalTimeSliceNanoseconds
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060028DB RID: 10459
		[NativeMethod("CollectIncrementalWrapper")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CollectIncremental(ulong nanoseconds = 0UL);

		// Token: 0x02000372 RID: 882
		public enum Mode
		{
			// Token: 0x04000B93 RID: 2963
			Disabled,
			// Token: 0x04000B94 RID: 2964
			Enabled,
			// Token: 0x04000B95 RID: 2965
			Manual
		}
	}
}
