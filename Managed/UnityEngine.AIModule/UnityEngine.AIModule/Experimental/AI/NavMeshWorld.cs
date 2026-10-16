using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x02000006 RID: 6
	[StaticAccessor("NavMeshWorldBindings", StaticAccessorType.DoubleColon)]
	[Obsolete("The experimental NavMeshWorld struct has been deprecated without replacement.")]
	public struct NavMeshWorld
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002150 File Offset: 0x00000350
		public bool IsValid()
		{
			return this.world != IntPtr.Zero;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002174 File Offset: 0x00000374
		public static NavMeshWorld GetDefaultWorld()
		{
			NavMeshWorld navMeshWorld;
			NavMeshWorld.GetDefaultWorld_Injected(out navMeshWorld);
			return navMeshWorld;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000218C File Offset: 0x0000038C
		private static void AddDependencyInternal(IntPtr navmesh, JobHandle handle)
		{
			NavMeshWorld.AddDependencyInternal_Injected(navmesh, ref handle);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021A1 File Offset: 0x000003A1
		public void AddDependency(JobHandle job)
		{
			NavMeshWorld.AddDependencyInternal(this.world, job);
		}

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDefaultWorld_Injected(out NavMeshWorld ret);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDependencyInternal_Injected(IntPtr navmesh, [In] ref JobHandle handle);

		// Token: 0x04000013 RID: 19
		internal IntPtr world;
	}
}
