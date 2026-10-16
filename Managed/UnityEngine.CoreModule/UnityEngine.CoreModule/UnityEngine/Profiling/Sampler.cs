using System;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x02000300 RID: 768
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	[UsedByNativeCode]
	public class Sampler
	{
		// Token: 0x06002644 RID: 9796 RVA: 0x00030B93 File Offset: 0x0002ED93
		internal Sampler()
		{
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00049E0B File Offset: 0x0004800B
		internal Sampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x00049E1C File Offset: 0x0004801C
		public bool isValid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00049E40 File Offset: 0x00048040
		public Recorder GetRecorder()
		{
			ProfilerRecorderHandle profilerRecorderHandle = new ProfilerRecorderHandle((ulong)this.m_Ptr.ToInt64());
			return new Recorder(profilerRecorderHandle);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00049E6C File Offset: 0x0004806C
		public static Sampler Get(string name)
		{
			IntPtr marker = ProfilerUnsafeUtility.GetMarker(name);
			bool flag = marker == IntPtr.Zero;
			Sampler sampler;
			if (flag)
			{
				sampler = Sampler.s_InvalidSampler;
			}
			else
			{
				sampler = new Sampler(marker);
			}
			return sampler;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00049EA4 File Offset: 0x000480A4
		public static int GetNames(List<string> names)
		{
			List<ProfilerRecorderHandle> list = new List<ProfilerRecorderHandle>();
			ProfilerRecorderHandle.GetAvailable(list);
			bool flag = names != null;
			if (flag)
			{
				bool flag2 = names.Count < list.Count;
				if (flag2)
				{
					names.Capacity = list.Count;
					for (int i = names.Count; i < list.Count; i++)
					{
						names.Add(null);
					}
				}
				int num = 0;
				foreach (ProfilerRecorderHandle profilerRecorderHandle in list)
				{
					names[num] = ProfilerRecorderHandle.GetDescription(profilerRecorderHandle).Name;
					num++;
				}
			}
			return list.Count;
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x00049F80 File Offset: 0x00048180
		public string name
		{
			get
			{
				return ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
			}
		}

		// Token: 0x04000ABC RID: 2748
		internal IntPtr m_Ptr;

		// Token: 0x04000ABD RID: 2749
		internal static Sampler s_InvalidSampler = new Sampler();
	}
}
