using System;
using UnityEngine.Bindings;

namespace UnityEngine.AI
{
	// Token: 0x02000022 RID: 34
	[NativeHeader("Modules/AI/Public/NavMeshBuildDebugSettings.h")]
	public struct NavMeshBuildDebugSettings
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004C3C File Offset: 0x00002E3C
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00004C54 File Offset: 0x00002E54
		public NavMeshBuildDebugFlags flags
		{
			get
			{
				return (NavMeshBuildDebugFlags)this.m_Flags;
			}
			set
			{
				this.m_Flags = (byte)value;
			}
		}

		// Token: 0x04000078 RID: 120
		private byte m_Flags;
	}
}
