using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[Obsolete("This type is deprecated and will be removed in a future release.", false)]
	[NativeHeader("Modules/ClusterRenderer/ClusterNetwork.h")]
	public class ClusterNetwork
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		public static extern bool isMasterOfCluster
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		public static extern bool isDisconnected
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		public static extern int nodeIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
